namespace BinarySerializer.Compression
{
    using System.IO;
    using System.IO.Compression;
    
    /// <summary>
    /// A <see cref="ICompressor"/> implementation that uses the <see cref="DeflateStream"/> class
    /// to compress and uncompress a <see cref="byte"/> array.
    /// </summary>
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