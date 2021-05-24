// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

using Hertzole.ALE;
using UnityEngine;

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Resolvers
{
    using System;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(2)
            {
                { typeof(global::MsgPackTest), 0 },
                { typeof(global::MyCustomStruct), 1 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new MessagePack.Formatters.MsgPackTestFormatter();
                case 1: return new MessagePack.Formatters.MyCustomStructFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1649 // File name should match first type name




// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters
{
    using System;
    using System.Buffers;
    using MessagePack;

    public sealed class MsgPackTestFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MsgPackTest>
    {

        public void Serialize(ref MessagePackWriter writer, global::MsgPackTest value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(101);
            writer.WriteNil();
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.stringTest, options);
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            writer.WriteNil();
            formatterResolver.GetFormatterWithVerify<Vector3>().Serialize(ref writer, value.vector3TesT, options);
        }

        public global::MsgPackTest Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var __intTest__ = default(int);
            var __stringTest__ = default(string);
            var __vector3TesT__ = default(Vector3);

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case -10:
                        __intTest__ = reader.ReadInt32();
                        break;
                    case 1:
                        __stringTest__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 100:
                        __vector3TesT__ = formatterResolver.GetFormatterWithVerify<Vector3>().Deserialize(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::MsgPackTest();
            ____result.intTest = __intTest__;
            ____result.stringTest = __stringTest__;
            ____result.vector3TesT = __vector3TesT__;
            reader.Depth--;
            return ____result;
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name

// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters
{
    using System;
    using System.Buffers;
    using System.Runtime.InteropServices;
    using MessagePack;

    public sealed class MyCustomStructFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MyCustomStruct>
    {
        // str
        private static global::System.ReadOnlySpan<byte> GetSpan_str() => new byte[1 + 3] { 163, 115, 116, 114 };
        // vec
        private static global::System.ReadOnlySpan<byte> GetSpan_vec() => new byte[1 + 3] { 163, 118, 101, 99 };
        // intValue
        private static global::System.ReadOnlySpan<byte> GetSpan_intValue() => new byte[1 + 8] { 168, 105, 110, 116, 86, 97, 108, 117, 101 };
        // veryLongNameThatIsLong
        private static global::System.ReadOnlySpan<byte> GetSpan_veryLongNameThatIsLong() => new byte[1 + 22] { 182, 118, 101, 114, 121, 76, 111, 110, 103, 78, 97, 109, 101, 84, 104, 97, 116, 73, 115, 76, 111, 110, 103 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::MyCustomStruct value, global::MessagePack.MessagePackSerializerOptions options)
        {
            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(4);
            writer.WriteRaw(GetSpan_str());
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.str, options);
            writer.WriteRaw(GetSpan_vec());
            formatterResolver.GetFormatterWithVerify<Vector3>().Serialize(ref writer, value.vec, options);
            writer.WriteRaw(GetSpan_intValue());
            writer.Write(value.intValue);
            writer.WriteRaw(GetSpan_veryLongNameThatIsLong());
            writer.Write(value.veryLongNameThatIsLong);
        }

        public global::MyCustomStruct Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                throw new global::System.InvalidOperationException("typecode is null, struct not supported");
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __str__ = default(string);
            var __vec__ = default(Vector3);
            var __intValue__ = default(int);
            var __veryLongNameThatIsLong__ = default(long);

            for (int i = 0; i < length; i++)
            {
                // ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                // switch (stringKey.Length)
                // {
                //     default:
                //     FAIL:
                //       reader.Skip();
                //       continue;
                //     case 3:
                //         switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                //         {
                //             default: goto FAIL;
                //             case 7500915UL:
                //                 __str__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                //                 continue;
                //             case 6514038UL:
                //                 __vec__ = formatterResolver.GetFormatterWithVerify<Vector3>().Deserialize(ref reader, options);
                //                 continue;
                //         }
                //     case 8:
                //         if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 7310868735423966825UL) { goto FAIL; }
                //
                //         __intValue__ = reader.ReadInt32();
                //         continue;
                //     case 22:
                //         if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_veryLongNameThatIsLong().Slice(1))) { goto FAIL; }
                //
                //         __veryLongNameThatIsLong__ = reader.ReadInt64();
                //         continue;
                //
                // }
                
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                LevelEditorLogger.Log($"Reading loop {i} {stringKey.ToDebugString()}");
                int keyLength = stringKey.Length;
                ulong key = 0;
                if (keyLength <= 8)
                {
                    key = global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey);
                }
                if (keyLength == 3 && key == 7500915UL)
                {
                    __str__ = reader.ReadString();
                }
                else if (keyLength == 3 && key == 6514038UL)
                {
                    __vec__ = formatterResolver.GetFormatterWithVerify<Vector3>().Deserialize(ref reader, options);
                }
                else if (keyLength == 8 && key == 7310868735423966825UL)
                {
                    __intValue__ = reader.ReadInt32();
                }
                else if (keyLength == 22 && global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_veryLongNameThatIsLong().Slice(1)))
                {
                    __veryLongNameThatIsLong__ = reader.ReadInt64();
                }
                else
                {
                    LevelEditorLogger.Log($"Skipping {stringKey.Length} {key}");
                    reader.Skip();
                }
            }

            var ____result = new global::MyCustomStruct()
            {
                str = __str__,
                vec = __vec__,
                intValue = __intValue__,
                veryLongNameThatIsLong = __veryLongNameThatIsLong__,
            };

            reader.Depth--;
            return ____result;
        }
    }
}

