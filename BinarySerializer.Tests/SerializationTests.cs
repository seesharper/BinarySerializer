using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Tests
{
    using System.IO;

    [TestClass]
    public class SerializationTests
    {
        private MemoryStream stream = new MemoryStream();
        
        [TestMethod]
        public void Serialize_Byte_CanBeDeserialized()
        {
            GetWriter().Write((byte)42);
            Assert.AreEqual((byte)42, GetReader().ReadByte());
        }
        
        [TestMethod]
        public void Serialize_Int32WithZeroValue_CanBeDeserialized()
        {
            GetWriter().Write(0);
            Assert.AreEqual(0, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Serialize_Int32WithMaxValue_CanBeDeserialized()
        {
            GetWriter().Write(int.MaxValue);
            Assert.AreEqual(int.MaxValue, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Serialize_Int32WithMinValuePlusOne_CanBeDeserialized()
        {
            GetWriter().Write(int.MinValue + 1);
            Assert.AreEqual(int.MinValue + 1, GetReader().ReadInt32());
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
