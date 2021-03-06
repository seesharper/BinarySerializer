﻿namespace BinarySerializer
{
    /// <summary>
    /// Allows an object to control its own optimized serialization and deserialization.    
    /// </summary>
    public interface IBinarySerializable
    {
        /// <summary>
        /// Allows an object to serialize its own data using the <paramref name="writer"/>
        /// </summary>
        /// <param name="writer">The <see cref="ISerializer"/> that is used to serialize the data.</param>
        void Serialize(ISerializer writer);

        /// <summary>
        /// Allows an object to deserialize its own data using the <paramref name="reader"/>
        /// </summary>
        /// <param name="reader">The <see cref="IDeserializer"/> that is used to deserialize the object graph.</param>
        void Deserialize(IDeserializer reader);
    }
}
