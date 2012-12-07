namespace BinarySerializer
{
    using System;

    /// <summary>
    /// Represents a class that is capable of deserializing an object graph from a byte stream. 
    /// </summary>    
    public interface IBinarySerializationReader
    {
        /// <summary>
        /// Reads an <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="int"/> read from the current stream.</returns>
        int ReadInt32();
        
        /// <summary>
        /// Reads the next <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> read from the current stream.</returns>
        byte ReadByte();
    }
}