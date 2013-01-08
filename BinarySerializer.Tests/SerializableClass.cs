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
        
        public void Serialize(IBinarySerializationWriter writer)
        {
            writer.Write(Value);
        }

        public void Deserialize(IBinarySerializationReader reader)
        {
            Value = reader.ReadString();
        }
    }
}