namespace BinarySerializer.Tests
{
    using System;

    [Serializable]
    public class SerializableClass
    {
        public string Value { get; set; }
    }

    [Serializable]
    public sealed class SealedSerializableClass
    {
        public string Value { get; set; }
    }

    public class BinarySerializableClass : IBinarySerializable
    {
        public string Value { get; set; }
        
        public void Serialize(IWriter writer)
        {
            writer.Write(Value);
        }

        public void Deserialize(IReader reader)
        {
            Value = reader.ReadString();
        }
    }

    public enum ByteEnum : byte
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum SByteEnum : sbyte
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum Int16Enum : short
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum UInt16Enum : ushort
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum Int32Enum 
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum UInt32Enum : uint
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum Int64Enum : long
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }

    public enum UInt64Enum : ulong
    {
        Sat, Sun, Mon, Tue, Wed, Thu, Fri
    }
}