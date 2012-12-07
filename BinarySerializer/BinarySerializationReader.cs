namespace BinarySerializer
{
    using System.IO;

    /// <summary>
    /// A class that is capable of deserializing an object graph from a byte stream. 
    /// </summary>        
    public class BinarySerializationReader : IBinarySerializationReader
    {
        private readonly Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySerializationReader"/> class.
        /// </summary>
        /// <param name="stream">The target <see cref="Stream"/></param>        
        public BinarySerializationReader(Stream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// Reads an <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="int"/> read from the current stream.</returns>
        public int ReadInt32()
        {            
            byte firstByte = this.ReadByte();

            // Retrieve the first six bits
            int result = firstByte & 0x3f;
            int bitShift = 6;
            byte nextByte = firstByte;
            
            // Check the eight bit to see if we must read another byte. 
            while ((nextByte & 0x80) != 0)
            {
                nextByte = this.ReadByte();
                result |= (nextByte & 0x7f) << bitShift;
                bitShift += 7;                    
            }
            
            // Check the seventh bit of the first byte to see if we must negate the result.
            return (firstByte & 0x40) == 0x40 ? -result : result;
        }

        /// <summary>
        /// Reads the next <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> read from the current stream.</returns>
        public byte ReadByte()
        {
            return (byte)this.stream.ReadByte();
        }
    }
}