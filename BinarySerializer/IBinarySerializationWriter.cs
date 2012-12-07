namespace BinarySerializer
{
    using System.IO;

    /// <summary>
    /// Represents a class that is capable of serializing an object graph to a byte stream. 
    /// </summary>    
    public interface IBinarySerializationWriter
    {
        /// <summary>
        /// Writes a <see cref="int"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to write.</param>
        void Write(int value);

        /// <summary>
        /// Writes an <see cref="byte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to write.</param>
        void Write(byte value);
    }

    public class BinarySerializationWriter : IBinarySerializationWriter
    {
        private readonly Stream stream;

        public BinarySerializationWriter(Stream stream)
        {
            this.stream = stream;
        }

        public void Write(int value)
        {
            Write7BitEncodedSigned32BitValue(value);
        }

        public void Write(byte value)
        {
            stream.WriteByte(value);
        }

        private void Write7BitEncodedSigned32BitValue(int value)
        {
            // Same as Math.Abs(value), only faster
            int absoluteValue = (value ^ (value >> 31)) - (value >> 31);

            // Get the first six bits from the first byte
            var firstByte = (byte)(absoluteValue & 0x3f);

            // Use the seventh bit to indicate a negative number
            if (value < 0)
            {
                firstByte = (byte)(firstByte | 0x40);
            }

            if (absoluteValue >= 0x40)
            {
                // Indicate that the are a byte following this one when reading.
                firstByte = (byte)(firstByte | 0x80);
            }

            stream.WriteByte(firstByte);
            absoluteValue >>= 6;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (absoluteValue >= 0x80)
            {
                var bytetowrite = (byte)(absoluteValue | 0x80);
                stream.WriteByte(bytetowrite);
                absoluteValue >>= 7;
            }

            // write the last byte            
            if (absoluteValue > 0)
            {
                stream.WriteByte((byte)absoluteValue);
            }
        }
    }
}