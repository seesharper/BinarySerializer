namespace BinarySerializer.Tests
{
    using System.Collections.ObjectModel;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectSerializationTests
    {
        private readonly MemoryStream stream = new MemoryStream();

        [TestMethod]
        public void WriteObject_BinarySerializable_CanBeRead()
        {
            GetWriter().Write(new BinarySerializableClass());
            this.GetReader().Read<BinarySerializableClass>();
        }

        [TestMethod]
        public void WriteObject_BinarySerializableAsObject_CanBeRead()
        {
            object value = new BinarySerializableClass();            
            GetWriter().Write<object>(value);
            this.GetReader().Read<object>();
        }
        
        [TestMethod]
        public void WriteObject_Collection_CanBeRead()
        {
            Collection<int> collection = new Collection<int>();
            collection.Add(42);
            collection.Add(84);
            GetWriter().Write(collection);
            this.GetReader().Read<Collection<int>>();        
        }


        [TestMethod]
        public void WriteObject_Byte_CanBeRead()
        {
            GetWriter().Write<byte>(42);
            Assert.AreEqual((byte)42, GetReader().Read<byte>());
        }
       
        private IBinarySerializationWriter GetWriter()
        {
            stream.SetLength(0);
            return new BinarySerializationWriter(stream);
        }

        private BinarySerializationReader GetReader()
        {
            stream.Position = 0;
            return new BinarySerializationReader(stream);
        } 
    }
}