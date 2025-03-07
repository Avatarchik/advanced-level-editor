﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Hertzole.ALE
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
    [AddComponentMenu("ALE/Object Manager", 2)]
#endif
    //TODO: Make pooling toggleable.
    public class LevelEditorObjectManager : MonoBehaviour, ILevelEditorObjectManager
    {
        [SerializeField] 
        private bool poolObjects = true;
        [SerializeField]
        private ScriptableObject resources = null;
        [SerializeField, RequireType(typeof(ILevelEditorUndo))]
        private GameObject undo = null;

        private uint instanceID = 0;

        private ILevelEditorResources realResources;
        private ILevelEditorUndo undoComp;

        private List<ILevelEditorObject> allObjects = new List<ILevelEditorObject>();

        private Dictionary<string, Stack<ILevelEditorObject>> pooledObjects = new Dictionary<string, Stack<ILevelEditorObject>>();
        private Dictionary<string, List<ILevelEditorObject>> activeObjects = new Dictionary<string, List<ILevelEditorObject>>();
        private Dictionary<string, int> objectCount = new Dictionary<string, int>();
        private Dictionary<uint, ILevelEditorObject> objectsWithId = new Dictionary<uint, ILevelEditorObject>();

        public bool PoolObjects { get { return poolObjects; } set { poolObjects = value; } }
        
        public ScriptableObject ResourcesObject
        {
            get { return resources; }
            set
            {
                if (value is ILevelEditorResources resources)
                {
                    this.resources = value;
                    realResources = resources;
                }
                else
                {
                    Debug.LogError($"{value} does not implement ILevelEditorResources interface.");
                }
            }
        }

        public ILevelEditorResources Resources
        {
            get
            {
                if (realResources == null) { realResources = resources as ILevelEditorResources; }
                return realResources;
            }
            set { realResources = value; }
        }

        public event EventHandler<LevelEditorObjectEventSpawningEvent> OnCreatingObject;
        public event EventHandler<LevelEditorObjectEventDeletingEvent> OnDeletingObject;

        public event EventHandler<LevelEditorObjectEvent> OnCreatedObject;
        public event EventHandler<LevelEditorObjectEvent> OnCreatedObjectFromSaveData;
        public event EventHandler<LevelEditorObjectEvent> OnDeletedObject;

        private void Awake()
        {
            if (undo != null)
            {
                undoComp = undo.GetComponent<ILevelEditorUndo>();
            }
        }

#if !ALE_STRIP_SAFETY || UNITY_EDITOR
        private void Start()
        {
            if (realResources == null && !resources.ExistsAndImplements<ILevelEditorResources>(nameof(resources), this))
            {
                return;
            }
        }
#endif

        public ILevelEditorObject CreateObject(ILevelEditorResource resource, Vector3 position, Quaternion rotation, Transform parent, uint instanceID, bool registerUndo = true)
        {
            if (resource.Asset is GameObject go)
            {
                if (objectsWithId.ContainsKey(instanceID))
                {
                    throw new DuplicateIDException($"There already is an object with the instance ID {instanceID}.");
                }

                LevelEditorObjectEventSpawningEvent args = new LevelEditorObjectEventSpawningEvent(resource);

                OnCreatingObject?.Invoke(this, args);

                if (args.Cancel)
                {
                    LevelEditorLogger.Log("CreateObject canceled. Returning null.");
                    return null;
                }

                ILevelEditorObject obj;

                if (poolObjects && pooledObjects.ContainsKey(resource.ID) && pooledObjects[resource.ID].Count > 0)
                {
                    obj = pooledObjects[resource.ID].Pop();
                    obj.MyGameObject.transform.SetPositionAndRotation(position, rotation);
                    obj.MyGameObject.transform.SetParent(parent);
                }
                else
                {
                    GameObject objGo = Instantiate(go, position, rotation, parent);
                    obj = objGo.GetOrAddComponent<LevelEditorObject>();
                    obj.ID = resource.ID;

                    LevelEditorComponentWrapper.AddWrappers(objGo);
                }

                obj.MyGameObject.name = resource.Name;
                obj.InstanceID = instanceID;
                obj.MyGameObject.SetActive(true);

                if (instanceID >= this.instanceID)
                {
                    this.instanceID = instanceID;
                }

                if (!activeObjects.ContainsKey(resource.ID))
                {
                    activeObjects.Add(resource.ID, new List<ILevelEditorObject>());
                }

                if (!objectCount.ContainsKey(resource.ID))
                {
                    objectCount.Add(resource.ID, 0);
                }

                activeObjects[resource.ID].Add(obj);
                objectCount[resource.ID]++;
                allObjects.Add(obj);

                obj.OnUnPooled();

                objectsWithId[obj.InstanceID] = obj;

                OnCreatedObject?.Invoke(this, new LevelEditorObjectEvent(obj));

                if (registerUndo && undoComp != null)
                {
                    undoComp.AddAction(new CreateObjectUndoAction(resource, position, rotation, parent, instanceID, obj), false);
                }

                return obj;
            }
            else
            {
                Debug.LogError($"Tried to create {resource.ID} but the asset is not a prefab.");
                return null;
            }
        }

        public void CreateObjectsFromSaveData(LevelEditorSaveData data)
        {
            if (data.objects != null && data.objects.Count > 0)
            {
                ILevelEditorObject[] allObjects = new ILevelEditorObject[data.objects.Count];

                for (int i = 0; i < data.objects.Count; i++)
                {
                    ILevelEditorObject obj = this.CreateObject(data.objects[i]);
                    allObjects[i] = obj;
                }

                Assert.AreEqual(data.objects.Count, allObjects.Length);

                for (int i = 0; i < allObjects.Length; i++)
                {
                    LevelEditorComponentData[] components = data.objects[i].components;
                    for (int j = 0; j < components.Length; j++)
                    {
                        // LevelEditorPropertyData[] properties = components[j].properties;
                        // for (int k = 0; k < properties.Length; k++)
                        // {
                        //     if (properties[k].typeName == "UnityEngine.Component")
                        //     {
                        //         ILevelEditorObject targetObj = null;
                        //         if (properties[k].value != null)
                        //         {
                        //             // targetObj = GetObject((uint)properties[k].value);
                        //         }
                        //         properties[k].value = targetObj ?? null;
                        //     }
                        // }
                    }

                    allObjects[i].ApplyExposedData(components);
                    OnCreatedObjectFromSaveData?.Invoke(this, new LevelEditorObjectEvent(allObjects[i]));
                }
            }
        }

        public bool DeleteObject(ILevelEditorObject target, bool registerUndo = true)
        {
            if (target == null)
            {
                return false;
            }

            LevelEditorObjectEventDeletingEvent args = new LevelEditorObjectEventDeletingEvent(target);

            OnDeletingObject?.Invoke(this, args);

            if (args.Cancel)
            {
                LevelEditorLogger.Log("DeleteObject canceled.");
                return false;
            }

            DeleteObjectInternal(target, registerUndo);

            allObjects.Remove(target);

            return true;
        }

        public void DeleteAllObjects(bool registerUndo = true)
        {
            for (int i = 0; i < allObjects.Count; i++)
            {
                DeleteObjectInternal(allObjects[i], false);
            }

            allObjects.Clear();
        }

        private void DeleteObjectInternal(ILevelEditorObject target, bool registerUndo)
        {
            if (!pooledObjects.ContainsKey(target.ID))
            {
                pooledObjects.Add(target.ID, new Stack<ILevelEditorObject>());
            }

            objectsWithId.Remove(target.InstanceID);

            pooledObjects[target.ID].Push(target);
            activeObjects[target.ID].Remove(target);
            objectCount[target.ID]--;

            target.OnPooled();

            if (target.MyGameObject.TryGetComponent(out ILevelEditorGizmos gizmos))
            {
                LevelEditorGLRenderer.Remove(gizmos);
            }

            if (target.Parent != null)
            {
                target.Parent.RemoveChild(target);
            }

            if (target.HasChildren())
            {
                //TODO: If deleting children, register delete multiple undo.
                for (int i = 0; i < target.Children.Count; i++)
                {
                    DeleteObject(target.Children[i], false);
                }

                target.Children.Clear();
            }

            if (poolObjects)
            {
                target.MyGameObject.SetActive(false);
            }
            else
            {
                Destroy(target.MyGameObject);
            }

            //TODO: Apply undo.

            OnDeletedObject?.Invoke(this, new LevelEditorObjectEvent(target));
        }

        public void ResetInstanceID()
        {
            instanceID = 0;
        }

        public List<ILevelEditorObject> GetAllObjects()
        {
            return allObjects;
        }

        public ILevelEditorObject GetObject(uint instanceID)
        {
            return objectsWithId.TryGetValue(instanceID, out ILevelEditorObject obj) ? obj : null;
        }

        public int GetObjectCount(string id)
        {
            if (objectCount.TryGetValue(id, out int count))
            {
                return count;
            }

            Debug.LogError("No object with the ID " + id + ".");
            return 0;
        }

        public int GetTotalObjectCount()
        {
            return allObjects.Count;
        }

        public uint GetNextInstanceID()
        {
            return instanceID + 1;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GetStandardComponents();
        }

        private void Reset()
        {
            GetStandardComponents();
        }

        private void GetStandardComponents()
        {
            if (!resources.ExistsAndImplements<ILevelEditorResources>(nameof(resources), this, false))
            {
                resources = null;
            }
        }
#endif
    }
}
