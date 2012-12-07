namespace BinarySerializer
{
    /// <summary>
    /// Allows an object to control its own optimized serialization and deserialization.    
    /// </summary>
    public interface IBinarySerializable
    {
        /// <summary>
        /// Allows an object to serialize its own data using the <paramref name="binarySerializationWriter"/>
        /// </summary>
        /// <param name="binarySerializationWriter">The <see cref="IBinarySerializationWriter"/> that is used to serialize the data.</param>
        void Serialize(IBinarySerializationWriter binarySerializationWriter);

        /// <summary>
        /// Allows an object to deserialize its own data using the <paramref name="binarySerializationReader"/>
        /// </summary>
        /// <param name="binarySerializationReader">The <see cref="IBinarySerializationReader"/> that is used to deserialize the object graph.</param>
        void Deserialize(IBinarySerializationReader binarySerializationReader);
    }
}
