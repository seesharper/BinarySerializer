namespace BinarySerializer
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Represents a class that is capable of compressing and decompressing a byte array.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compresses a byte array.
        /// </summary>
        /// <param name="bytes">The <see cref="byte"/> array.</param>
        /// <returns>The compressed <see cref="byte"/> array.</returns>
        byte[] Compress(byte[] bytes);

        byte[] Decompress(byte[] bytes, uint length);
    }


    public class DeflateCompressor : ICompressor
    {
        public byte[] Compress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);                    
                }
                return memoryStream.ToArray();
            }
        }
        
        public byte[] Decompress(byte[] bytes, uint length)
        {
            var buffer = new byte[length];
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))
                {
                    deflateStream.Read(buffer, 0, (int)length);
                }           

                return buffer;
            }
        }
    }

}