namespace BinarySerializer
{
    using System;

    /// <summary>
    /// Represents a class that is capable of deserializing an object graph from a byte stream. 
    /// </summary>    
    public interface IBinarySerializationReader
    {
        /// <summary>
        /// Reads an <see cref="ushort"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="ushort"/> read from the current stream.</returns>
        ushort ReadUInt16();

        /// <summary>
        /// Reads a nullable <see cref="ushort"/> from the current stream.
        /// </summary>        
        /// <returns>A nullable <see cref="ushort"/> read from the current stream.</returns>
        ushort? ReadNullableUInt16();

        /// <summary>
        /// Reads an <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="short"/> read from the current stream.</returns>
        short ReadInt16();

        /// <summary>
        /// Reads a nullable <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="short"/> read from the current stream.</returns>
        short? ReadNullableInt16();

        /// <summary>
        /// Reads an <see cref="uint"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="uint"/> read from the current stream.</returns>
        uint ReadUInt32();

        /// <summary>
        /// Reads a nullable <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="int"/> read from the current stream.</returns>
        uint? ReadNullableUInt32();

        /// <summary>
        /// Reads an <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="int"/> read from the current stream.</returns>
        int ReadInt32();

        /// <summary>
        /// Reads a nullable <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="int"/> read from the current stream.</returns>
        int? ReadNullableInt32();

        /// <summary>
        /// Reads an <see cref="ulong"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="ulong"/> read from the current stream.</returns>
        ulong ReadUInt64();

        /// <summary>
        /// Reads a nullable <see cref="ulong"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="ulong"/> read from the current stream.</returns>
        ulong? ReadNullableUInt64();

        /// <summary>
        /// Reads an <see cref="long"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="long"/> read from the current stream.</returns>
        long ReadInt64();

        /// <summary>
        /// Reads a nullable <see cref="long"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="long"/> read from the current stream.</returns>
        long? ReadNullableInt64();
        
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
        /// Reads the next nullable <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="byte"/> read from the current stream.</returns>
        byte? ReadNullableByte();
        
        /// <summary>
        /// Reads the next <see cref="decimal"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="decimal"/> read from the current stream.</returns>
        decimal ReadDecimal();

        /// <summary>
        /// Reads the next nullable <see cref="decimal"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="decimal"/> read from the current stream.</returns>
        decimal? ReadNullableDecimal();

        /// <summary>
        /// Reads the next <see cref="float"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="float"/> read from the current stream.</returns>
        float ReadSingle();

        /// <summary>
        /// Reads the next nullable <see cref="float"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="float"/> read from the current stream.</returns>
        float? ReadNullableSingle();

        /// <summary>
        /// Reads the next <see cref="double"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="double"/> read from the current stream.</returns>
        double ReadDouble();

        /// <summary>
        /// Reads the next nullable <see cref="double"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="double"/> read from the current stream.</returns>
        double? ReadNullableDouble();

        /// <summary>
        /// Reads the next <see cref="Guid"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="Guid"/> read from the current stream.</returns>
        Guid ReadGuid();

        /// <summary>
        /// Reads the next nullable <see cref="Guid"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="Guid"/> read from the current stream.</returns>
        Guid? ReadNullableGuid();

        /// <summary>
        /// Reads the next <see cref="DateTime"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="DateTime"/> read from the current stream.</returns>
        DateTime ReadDateTime();

        /// <summary>
        /// Reads the next nullable <see cref="DateTime"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="DateTime"/> read from the current stream.</returns>
        DateTime? ReadNullableDateTime();

        /// <summary>
        /// Reads the next <see cref="bool"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="bool"/> read from the current stream.</returns>
        bool ReadBoolean();

        /// <summary>
        /// Reads the next nullable <see cref="bool"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="bool"/> read from the current stream.</returns>
        bool? ReadNullableBoolean();

        /// <summary>
        /// Reads the next <see cref="string"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="string"/> read from the current stream.</returns>
        string ReadString();

        /// <summary>
        /// Reads the next object value from the current stream.
        /// </summary>        
        /// <returns>The next object read from the current stream.</returns>
        T ReadObject<T>();

        /// <summary>
        /// Reads the next <see cref="IBinarySerializable"/> object from the current stream.
        /// </summary>
        /// <typeparam name="T">The type of object to be returned from the stream.</typeparam>
        /// <returns>The next <see cref="IBinarySerializable"/> object read from the current stream.</returns>
        T ReadBinarySerializeableObject<T>() where T : IBinarySerializable;

    }
}