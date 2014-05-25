namespace BinarySerializer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a class that is capable of deserializing an object graph from a byte stream. 
    /// </summary>    
    public interface IReader
    {
        /// <summary>
        /// Reads an <see cref="ushort"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="ushort"/> read from the current stream.</returns>
        ushort ReadUInt16();
        
        /// <summary>
        /// Reads an <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="short"/> read from the current stream.</returns>
        short ReadInt16();
       
        /// <summary>
        /// Reads an <see cref="uint"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="uint"/> read from the current stream.</returns>
        uint ReadUInt32();
       
        /// <summary>
        /// Reads an <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="int"/> read from the current stream.</returns>
        int ReadInt32();
       
        /// <summary>
        /// Reads an <see cref="ulong"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="ulong"/> read from the current stream.</returns>
        ulong ReadUInt64();
        
        /// <summary>
        /// Reads an <see cref="long"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="long"/> read from the current stream.</returns>
        long ReadInt64();
       
        /// <summary>
        /// Reads the next <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> read from the current stream.</returns>
        byte ReadByte();

        /// <summary>
        /// Reads the next <see cref="byte"/> array from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> array read from the current stream.</returns>
        byte[] ReadBytes();
      
        /// <summary>
        /// Reads the next <see cref="sbyte"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="sbyte"/> read from the current stream.</returns>
        sbyte ReadSByte();
       
        /// <summary>
        /// Reads the next <see cref="decimal"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="decimal"/> read from the current stream.</returns>
        decimal ReadDecimal();
       
        /// <summary>
        /// Reads the next <see cref="float"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="float"/> read from the current stream.</returns>
        float ReadSingle();
        
        /// <summary>
        /// Reads the next <see cref="double"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="double"/> read from the current stream.</returns>
        double ReadDouble();
      
        /// <summary>
        /// Reads the next <see cref="Guid"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="Guid"/> read from the current stream.</returns>
        Guid ReadGuid();
        
        /// <summary>
        /// Reads the next <see cref="DateTime"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="DateTime"/> read from the current stream.</returns>
        DateTime ReadDateTime();
       
        /// <summary>
        /// Reads the next <see cref="bool"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="bool"/> read from the current stream.</returns>
        bool ReadBoolean();
       
        /// <summary>
        /// Reads the next <see cref="string"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="string"/> read from the current stream.</returns>
        string ReadString();

        /// <summary>
        /// Reads the next object value from the current stream.
        /// </summary>
        /// <returns>The next <see cref="object"/> read from the current stream.</returns>
        object ReadObject();

        /// <summary>
        /// Reads the next <see cref="Type"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="Type"/> read from the current stream.</returns>
        Type ReadType();

        /// <summary>
        /// Reads the next <see cref="IBinarySerializable"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="IBinarySerializable"/> read from the current stream.</returns>
        IBinarySerializable ReadBinarySerializable();

        /// <summary>
        /// Reads the next <see cref="Enum"/> from the current stream.
        /// </summary>
        /// <typeparam name="TValue">The type of <see cref="Enum"/> to read.</typeparam>
        /// <returns>The next <see cref="Enum"/> from the current stream.</returns>
        TValue ReadEnum<TValue>();

        /// <summary>
        /// Reads the next <see cref="ICollection{T}"/> from the current stream.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements in the collection.</typeparam>
        /// <returns>The next <see cref="ICollection{T}"/> from the current stream.</returns>
        ICollection<TValue> ReadCollection<TValue>();
    }
}