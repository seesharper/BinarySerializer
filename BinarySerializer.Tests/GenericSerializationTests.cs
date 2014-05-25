//namespace BinarySerializer.Tests
//{
//    using System.Collections.ObjectModel;
//    using System.IO;

//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    [TestClass]
//    public class ObjectSerializationTests : TestBase
//    {       
//        [TestMethod]
//        public void Write_Byte_CanBeRead()
//        {
//            GetWriter().Write<byte>(42);
//            Assert.AreEqual((byte)42,GetReader().Read<byte>());
//        }

//        [TestMethod]
//        public void Write_NullableByte_CanBeRead()
//        {
//            GetWriter().Write<byte?>(42);
//            Assert.AreEqual((byte?)42, GetReader().Read<byte?>());
//        }
//    }
//}