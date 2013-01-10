namespace BinarySerializer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a class that is capable of serializing an object graph to a byte stream. 
    /// </summary>    
    public interface IBinarySerializationWriter
    {
        /// <summary>
        /// Writes a <see cref="ushort"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="ushort"/> value to write.</param>
        void Write(ushort value);

        /// <summary>
        /// Writes a nullable <see cref="ushort"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="ushort"/> value to write.</param>
        void Write(ushort? value);

        /// <summary>
        /// Writes a <see cref="short"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="short"/> value to write.</param>
        void Write(short value);

        /// <summary>
        /// Writes a nullable <see cref="short"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="short"/> value to write.</param>
        void Write(short? value);

        /// <summary>
        /// Writes a <see cref="uint"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="uint"/> value to write.</param>
        void Write(uint value);

        /// <summary>
        /// Writes a nullable <see cref="uint"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="uint"/> value to write.</param>
        void Write(uint? value);

        /// <summary>
        /// Writes a <see cref="int"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to write.</param>
        void Write(int value);

        /// <summary>
        /// Writes a nullable <see cref="int"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="int"/> value to write.</param>
        void Write(int? value);

        /// <summary>
        /// Writes a <see cref="ulong"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value to write.</param>
        void Write(ulong value);

        /// <summary>
        /// Writes a nullable <see cref="ulong"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="ulong"/> value to write.</param>
        void Write(ulong? value);

        /// <summary>
        /// Writes a <see cref="long"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value to write.</param>
        void Write(long value);

        /// <summary>
        /// Writes a nullable <see cref="long"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="long"/> value to write.</param>
        void Write(long? value);

        /// <summary>
        /// Writes an <see cref="byte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to write.</param>
        void Write(byte value);

        /// <summary>
        /// Writes an <see cref="byte"/> array to the current stream.
        /// </summary>
        /// <param name="bytes">The <see cref="byte"/> array to write.</param>
        void Write(byte[] bytes);

        /// <summary>
        /// Writes a nullable <see cref="byte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to write.</param>
        void Write(byte? value);
     
        /// <summary>
        /// Writes an <see cref="decimal"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="decimal"/> value to write.</param>
        void Write(decimal value);

        /// <summary>
        /// Writes a nullable <see cref="decimal"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="decimal"/> value to write.</param>
        void Write(decimal? value);

        /// <summary>
        /// Writes an <see cref="float"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to write.</param>
        void Write(float value);

        /// <summary>
        /// Writes a nullable <see cref="float"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="float"/> value to write.</param>
        void Write(float? value);

        /// <summary>
        /// Writes an <see cref="double"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to write.</param>
        void Write(double value);

        /// <summary>
        /// Writes a nullable <see cref="double"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="double"/> value to write.</param>
        void Write(double? value);

        /// <summary>
        /// Writes an <see cref="Guid"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="Guid"/> value to write.</param>
        void Write(Guid value);

        /// <summary>
        /// Writes a nullable <see cref="Guid"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="Guid"/> value to write.</param>
        void Write(Guid? value);

        /// <summary>
        /// Writes an <see cref="DateTime"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        void Write(DateTime value);

        /// <summary>
        /// Writes a nullable <see cref="DateTime"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="DateTime"/> value to write.</param>
        void Write(DateTime? value);

        /// <summary>
        /// Writes a <see cref="bool"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to write.</param>
        void Write(bool value);

        /// <summary>
        /// Writes a nullable <see cref="bool"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="bool"/> value to write.</param>
        void Write(bool? value);

        /// <summary>
        /// Writes a <see cref="string"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to write.</param>
        void Write(string value);

        /// <summary>
        /// Writes an object to the current stream.
        /// </summary>        
        /// <param name="value">The object to write.</param>
        void Write<T>(T value);

        /// <summary>
        /// Writes an object that implements the <see cref="IBinarySerializable"/> interface to the current stream.
        /// </summary>
        /// <param name="value">The object to write.</param>
        void WriteBinarySerializableObject(IBinarySerializable value);

        /// <summary>
        /// Writes a <see cref="Type"/> object to the current stream.
        /// </summary>
        /// <param name="type"></param>
        void Write(Type type);

        ///// <summary>
        ///// Writes an <see cref="ICollection{T}"/> to the current stream.
        ///// </summary>
        ///// <typeparam name="T">The type of the elements in the collection.</typeparam>
        ///// <param name="collection">The <see cref="ICollection{T}"/> to write.</param>
        //void Write<T>(ICollection<T> collection);
    }
}