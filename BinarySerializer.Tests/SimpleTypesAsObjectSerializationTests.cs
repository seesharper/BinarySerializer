//namespace BinarySerializer.Tests
//{
//    using System;
//    using System.Linq;

//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    [TestClass]
//    public class SimpleTypesAsObjectSerializationTests : TestBase
//    {
//        [TestMethod]
//        public void Write_Byte_CanBeRead()
//        {
//            GetWriter().Write<object>((byte)42);
//            Assert.AreEqual((byte)42, GetReader().Read<object>());           
//        }

//        [TestMethod]
//        public void Write_ByteWithMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(byte.MinValue);
//            Assert.AreEqual(byte.MinValue, (byte)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_ByteWithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(byte.MaxValue);
//            Assert.AreEqual(byte.MaxValue, (byte)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_ByteArray_CanBeRead()
//        {
//            var bytesToWrite = new byte[] { 0xf, 0xff };
//            GetWriter().Write<object>(bytesToWrite);
//            var bytesRead = (byte[])GetReader().Read<object>();
//            Assert.IsTrue(bytesRead.Intersect(bytesToWrite).Count() == 2);
//        }

//        [TestMethod]
//        public void Write_Null_CanBeRead()
//        {
//            GetWriter().Write<object>(null);
//            Assert.IsNull(GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableByte_CanBeRead()
//        {
//            GetWriter().Write<object>((byte?)42);
//            Assert.AreEqual((byte?)42, (byte?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt16_CanBeRead()
//        {
//            GetWriter().Write<object>((ushort)42);
//            Assert.AreEqual((ushort)42, (ushort)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt16WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(ushort.MaxValue);
//            Assert.AreEqual(ushort.MaxValue, (ushort)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt16WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>((ushort)0);
//            Assert.AreEqual(0, (ushort)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableUInt16_CanBeRead()
//        {
//            GetWriter().Write<object>((ushort?)42);
//            Assert.AreEqual((ushort?)42, (ushort?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int16_CanBeRead()
//        {
//            GetWriter().Write<object>((short)42);
//            Assert.AreEqual((short)42, (short)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int16WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(short.MaxValue);
//            Assert.AreEqual(short.MaxValue, (short)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int16WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>((short)0);
//            Assert.AreEqual(0, (short)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int16WithMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(short.MinValue);
//            Assert.AreEqual(short.MinValue, (short)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableInt16_CanBeRead()
//        {
//            GetWriter().Write<object>((short?)42);
//            Assert.AreEqual((short?)42, (short)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt32_CanBeRead()
//        {
//            GetWriter().Write<object>((uint)42);
//            Assert.AreEqual((uint)42, (uint)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt32WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(uint.MaxValue);
//            Assert.AreEqual(uint.MaxValue, (uint)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt32WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>((uint)0);
//            Assert.AreEqual((uint)0, (uint)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableUInt32_CanBeRead()
//        {
//            GetWriter().Write<object>((uint?)42);
//            Assert.AreEqual((uint?)42, (uint)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int32_CanBeRead()
//        {
//            GetWriter().Write<object>(42);
//            Assert.AreEqual(42, (int)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int32WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>(0);
//            Assert.AreEqual(0, (int)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int32WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(int.MaxValue);
//            Assert.AreEqual(int.MaxValue, (int)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int32WithMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(int.MinValue);
//            Assert.AreEqual(int.MinValue, (int)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableInt32_CanBeRead()
//        {
//            GetWriter().Write<object>((int?)42);
//            Assert.AreEqual(42, (int?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt64_CanBeRead()
//        {
//            GetWriter().Write<object>((ulong)42);
//            Assert.AreEqual((ulong)42, (ulong)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt64WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(ulong.MaxValue);
//            Assert.AreEqual(ulong.MaxValue, (ulong)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UInt64WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>((ulong)0);
//            Assert.AreEqual((ulong)0, (ulong)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableUInt64_CanBeRead()
//        {
//            GetWriter().Write<object>((ulong?)42);
//            Assert.AreEqual((ulong?)42, (ulong?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int64_CanBeRead()
//        {
//            GetWriter().Write<object>((long)42);
//            Assert.AreEqual(42, (long)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int64WithZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>((long)0);
//            Assert.AreEqual(0, (long)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int64WithMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(long.MaxValue);
//            Assert.AreEqual(long.MaxValue, (long)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Int64WithMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(long.MinValue);
//            Assert.AreEqual(long.MinValue, (long)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableInt64_CanBeRead()
//        {
//            GetWriter().Write<object>((long?)42);
//            Assert.AreEqual(42, (long?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Decimal_CanBeRead()
//        {
//            GetWriter().Write<object>(42m);
//            Assert.AreEqual(42m, (decimal)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DecimalMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(decimal.MaxValue);
//            Assert.AreEqual(decimal.MaxValue, (decimal)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DecimalMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(decimal.MinValue);
//            Assert.AreEqual(decimal.MinValue, (decimal)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableDecimal_CanBeRead()
//        {
//            GetWriter().Write<object>((decimal?)42);
//            Assert.AreEqual(42, (decimal?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_Double_CanBeRead()
//        {
//            GetWriter().Write<object>(42d);
//            Assert.AreEqual(42d, (double)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DoubleMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(double.MaxValue);
//            Assert.AreEqual(double.MaxValue, (double)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DoubleZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>(0d);
//            Assert.AreEqual(0d, (double)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DoubleMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(double.MinValue);
//            Assert.AreEqual(double.MinValue, (double)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableDouble_CanBeRead()
//        {
//            GetWriter().Write<object>((double?)42);
//            Assert.AreEqual(42, (double?)GetReader().Read<object>());
//        }
        
//        [TestMethod]
//        public void Write_Single_CanBeRead()
//        {
//            GetWriter().Write<object>(42F);
//            Assert.AreEqual(42F, (float)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_SingleMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(float.MaxValue);
//            Assert.AreEqual(float.MaxValue, (float)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_SingleZeroValue_CanBeRead()
//        {
//            GetWriter().Write<object>(0f);
//            Assert.AreEqual(0f, (float)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_SingleMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(float.MinValue);
//            Assert.AreEqual(float.MinValue, (float)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableSingle_CanBeRead()
//        {
//            GetWriter().Write<object>((float?)42);
//            Assert.AreEqual(42, (float?)GetReader().Read<object>());
//        }
        
//        [TestMethod]
//        public void Write_Guid_CanBeRead()
//        {
//            Guid guid = Guid.NewGuid();
//            GetWriter().Write<object>(guid);
//            Assert.AreEqual(guid, (Guid)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableGuid_CanBeRead()
//        {
//            Guid? guid = Guid.NewGuid();
//            GetWriter().Write<object>(guid);
//            Assert.AreEqual(guid, (Guid)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DateTimeMaxValue_CanBeRead()
//        {
//            GetWriter().Write<object>(DateTime.MaxValue);
//            CompareDateTime(DateTime.MaxValue, (DateTime)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_DateTimeMinValue_CanBeRead()
//        {
//            GetWriter().Write<object>(DateTime.MinValue);
//            CompareDateTime(DateTime.MinValue, (DateTime)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableDateTime_CanBeRead()
//        {
//            DateTime? dateTime = DateTime.Now;
//            GetWriter().Write<object>(dateTime);
//            CompareDateTime(dateTime.Value, ((DateTime?)GetReader().Read<object>()).Value);
//        }
                
//        [TestMethod]
//        public void Write_BooleanFalse_CanBeRead()
//        {
//            GetWriter().Write<object>(false);
//            Assert.AreEqual(false, (bool)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_BooleanTrue_CanBeRead()
//        {
//            GetWriter().Write<object>(true);
//            Assert.AreEqual(true, (bool)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableBooleanTrue_CanBeRead()
//        {
//            GetWriter().Write<object>((bool?)true);
//            Assert.AreEqual(true, (bool?)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_NullableBooleanFalse_CanBeRead()
//        {
//            GetWriter().Write<object>((bool?)false);
//            Assert.AreEqual(false, (bool?)GetReader().Read<object>());
//        }
        
//        [TestMethod]
//        public void Write_String_CanBeRead()
//        {
//            GetWriter().Write<string>("SomeValue");
//            Assert.AreEqual("SomeValue", (string)GetReader().Read<object>());
//        }
        
//        [TestMethod]
//        public void Write_EmptyString_CanBeRead()
//        {
//            GetWriter().Write<object>(string.Empty);
//            Assert.AreEqual(string.Empty, (string)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_CompressedString_CanBeRead()
//        {
//            GetWriter().Write<object>(Text.LoremIpsum);
//            Assert.AreEqual(Text.LoremIpsum, (string)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_KnownType_CanBeRead()
//        {
//            GetWriter().Write<object>(typeof(string));
//            Assert.AreEqual(typeof(string), (Type)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_UnKnownType_CanBeRead()
//        {
//            GetWriter().Write<object>(typeof(SerializableClass));
//            Assert.AreEqual(typeof(SerializableClass), (Type)GetReader().Read<object>());
//        }

//        [TestMethod]
//        public void Write_ByteEnum_CanBeRead()
//        {
//            GetWriter().Write(ByteEnum.Fri);
//            Assert.AreEqual(ByteEnum.Fri, GetReader().Read<ByteEnum>());
//        }

//        [TestMethod]
//        public void Write_NullableByteEnum_CanBeRead()
//        {
//            GetWriter().Write((ByteEnum?)ByteEnum.Fri);
//            Assert.AreEqual(ByteEnum.Fri, GetReader().Read<ByteEnum?>());
//        }
//    }
//}