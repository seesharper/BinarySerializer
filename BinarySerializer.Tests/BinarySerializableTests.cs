namespace BinarySerializer.Tests
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinarySerializableTests
    {
        [TestMethod]
        public void Write_BinarySerializableClass_CanBeRead()
        {
            GetWriter().Write(new BinarySerializableClass());
            var instance = GetReader().ReadBinarySerializable();
            Assert.IsInstanceOfType(instance, typeof(BinarySerializableClass));
        }
        
        
        
        
        private readonly MemoryStream stream = new MemoryStream();
        
        private IWriter GetWriter()
        {
            stream.SetLength(0);
            return new Writer(stream);
        }

        private IReader GetReader()
        {
            stream.Position = 0;
            return new Reader(stream);
        }
    }
}