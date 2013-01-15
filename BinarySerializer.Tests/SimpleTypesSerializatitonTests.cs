using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Tests
{    
    using System.IO;
   
    [TestClass]
    public class SimpleTypesSerializatitonTests
    {
        private readonly MemoryStream stream = new MemoryStream();
        
        [TestMethod]
        public void Write_Byte_CanBeRead()
        {
            GetWriter().Write((byte)42);
            Assert.AreEqual((byte)42, GetReader().ReadByte());            
        }

        [TestMethod]
        public void Write_ByteWithMinValue_CanBeRead()
        {
            GetWriter().Write(byte.MinValue);
            Assert.AreEqual(byte.MinValue, GetReader().ReadByte());
        }

        [TestMethod]
        public void Write_ByteWithMaxValue_CanBeRead()
        {
            GetWriter().Write(byte.MaxValue);
            Assert.AreEqual(byte.MaxValue, GetReader().ReadByte());
        }

        [TestMethod]
        public void Write_ByteArray_CanBeRead()
        {
            var bytesToWrite = new byte[] { 0xf, 0xff };
            GetWriter().Write(bytesToWrite);
            var bytesRead = GetReader().ReadBytes();
            Assert.IsTrue(bytesRead.Intersect(bytesToWrite).Count() == 2);
        }

        [TestMethod]
        public void Write_NullByteArray_CanBeRead()
        {            
            GetWriter().Write((byte[])null);            
            Assert.IsNull(GetReader().ReadBytes());
        }


        [TestMethod]
        public void Write_NullableByte_CanBeRead()
        {
            GetWriter().Write((byte?)42);
            Assert.AreEqual((byte?)42, GetReader().ReadNullableByte());
        }

        [TestMethod]
        public void Write_NullableByteWithNullValue_CanBeRead()
        {
            GetWriter().Write((byte?)null);
            Assert.AreEqual(null, GetReader().ReadNullableByte());
        }

        [TestMethod]
        public void Write_UInt16_CanBeRead()
        {
            GetWriter().Write((ushort)42);
            Assert.AreEqual((ushort)42, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_UInt16WithMaxValue_CanBeRead()
        {
            GetWriter().Write(ushort.MaxValue);
            Assert.AreEqual(ushort.MaxValue, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_UInt16WithZeroValue_CanBeRead()
        {
            GetWriter().Write((ushort)0);
            Assert.AreEqual(0, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_NullableUInt16_CanBeRead()
        {
            GetWriter().Write((ushort?)42);
            Assert.AreEqual((ushort?)42, GetReader().ReadNullableUInt16());
        }

        [TestMethod]
        public void Write_NullableUInt16WithNullValue_CanBeRead()
        {
            GetWriter().Write((ushort?)null);
            Assert.AreEqual(null, GetReader().ReadNullableUInt16());
        }

        [TestMethod]
        public void Write_Int16_CanBeRead()
        {
            GetWriter().Write((short)42);
            Assert.AreEqual((short)42, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_Int16WithMaxValue_CanBeRead()
        {
            GetWriter().Write(short.MaxValue);
            Assert.AreEqual(short.MaxValue, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_Int16WithZeroValue_CanBeRead()
        {
            GetWriter().Write((short)0);
            Assert.AreEqual(0, GetReader().ReadInt16());
        }
                
        [TestMethod]
        public void Write_Int16WithMinValue_CanBeRead()
        {
            GetWriter().Write(short.MinValue);
            Assert.AreEqual(short.MinValue, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_NullableInt16_CanBeRead()
        {
            GetWriter().Write((short?)42);
            Assert.AreEqual((short?)42, GetReader().ReadNullableInt16());
        }

        [TestMethod]
        public void Write_NullableInt16WithNullValue_CanBeRead()
        {
            GetWriter().Write((short?)null);
            Assert.AreEqual(null, GetReader().ReadNullableInt16());
        }

        [TestMethod]
        public void Write_UInt32_CanBeRead()
        {
            GetWriter().Write((uint)42);
            Assert.AreEqual((uint)42, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_UInt32WithMaxValue_CanBeRead()
        {
            GetWriter().Write(uint.MaxValue);
            Assert.AreEqual(uint.MaxValue, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_UInt32WithZeroValue_CanBeRead()
        {
            GetWriter().Write((uint)0);
            Assert.AreEqual((uint)0, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_NullableUInt32_CanBeRead()
        {
            GetWriter().Write((uint?)42);
            Assert.AreEqual((uint?)42, GetReader().ReadNullableUInt32());
        }

        [TestMethod]
        public void Write_NullableUInt32WithNullValue_CanBeRead()
        {
            GetWriter().Write((uint?)null);
            Assert.AreEqual(null, GetReader().ReadNullableUInt32());
        }

        [TestMethod]
        public void Write_Int32_CanBeRead()
        {
            GetWriter().Write(42);
            Assert.AreEqual(42, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_Int32WithZeroValue_CanBeRead()
        {
            GetWriter().Write(0);
            Assert.AreEqual(0, GetReader().ReadInt32());
        }
        
        [TestMethod]
        public void Write_Int32WithMaxValue_CanBeRead()
        {
            GetWriter().Write(int.MaxValue);
            Assert.AreEqual(int.MaxValue, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_Int32WithMinValue_CanBeRead()
        {
            GetWriter().Write(int.MinValue);
            Assert.AreEqual(int.MinValue, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_NullableInt32_CanBeRead()
        {
            GetWriter().Write((int?)42);
            Assert.AreEqual(42, GetReader().ReadNullableInt32());
        }

        [TestMethod]
        public void Write_NullableInt32WithNullValue_CanBeRead()
        {
            GetWriter().Write((int?)null);
            Assert.AreEqual(null, GetReader().ReadNullableInt32());
        }

        [TestMethod]
        public void Write_UInt64_CanBeRead()
        {
            GetWriter().Write((ulong)42);
            Assert.AreEqual((ulong)42, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_UInt64WithMaxValue_CanBeRead()
        {
            GetWriter().Write(ulong.MaxValue);
            Assert.AreEqual(ulong.MaxValue, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_UInt64WithZeroValue_CanBeRead()
        {
            GetWriter().Write((ulong)0);
            Assert.AreEqual((ulong)0, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_NullableUInt64_CanBeRead()
        {
            GetWriter().Write((ulong?)42);
            Assert.AreEqual((ulong?)42, GetReader().ReadNullableUInt64());
        }

        [TestMethod]
        public void Write_NullableUInt64WithNullValue_CanBeRead()
        {
            GetWriter().Write((ulong?)null);
            Assert.AreEqual(null, GetReader().ReadNullableUInt64());
        }

        [TestMethod]
        public void Write_Int64_CanBeRead()
        {
            GetWriter().Write((long)42);
            Assert.AreEqual(42, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithZeroValue_CanBeRead()
        {
            GetWriter().Write((long)0);
            Assert.AreEqual(0, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithMaxValue_CanBeRead()
        {
            GetWriter().Write(long.MaxValue);
            Assert.AreEqual(long.MaxValue, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithMinValue_CanBeRead()
        {
            GetWriter().Write(long.MinValue);
            Assert.AreEqual(long.MinValue, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_NullableInt64_CanBeRead()
        {
            GetWriter().Write((long?)42);
            Assert.AreEqual(42, GetReader().ReadNullableInt64());
        }

        [TestMethod]
        public void Write_NullableInt64WithNullValue_CanBeRead()
        {
            GetWriter().Write((long?)null);
            Assert.AreEqual(null, GetReader().ReadNullableInt64());
        }

        [TestMethod]
        public void Write_Decimal_CanBeRead()
        {
            GetWriter().Write(42m);
            Assert.AreEqual(42m, GetReader().ReadDecimal());
        }

        [TestMethod]
        public void Write_DecimalMaxValue_CanBeRead()
        {
            GetWriter().Write(decimal.MaxValue);
            Assert.AreEqual(decimal.MaxValue, GetReader().ReadDecimal());
        }

        [TestMethod]
        public void Write_DecimalMinValue_CanBeRead()
        {
            GetWriter().Write(decimal.MinValue);
            Assert.AreEqual(decimal.MinValue, GetReader().ReadDecimal());           
        }

        [TestMethod]
        public void Write_NullableDecimal_CanBeRead()
        {
            GetWriter().Write((decimal?)42);
            Assert.AreEqual(42, GetReader().ReadNullableDecimal());
        }

        [TestMethod]
        public void Write_NullableDecimalWithNullValue_CanBeRead()
        {
            GetWriter().Write((decimal?)null);
            Assert.AreEqual(null, GetReader().ReadNullableDecimal());
        }

        [TestMethod]
        public void Write_Double_CanBeRead()
        {
            GetWriter().Write(42d);
            Assert.AreEqual(42d, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_DoubleMaxValue_CanBeRead()
        {
            GetWriter().Write(double.MaxValue);
            Assert.AreEqual(double.MaxValue, GetReader().ReadDouble());           
        }

        [TestMethod]
        public void Write_DoubleZeroValue_CanBeRead()
        {
            GetWriter().Write(0d);
            Assert.AreEqual(0d, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_DoubleMinValue_CanBeRead()
        {        
            GetWriter().Write(double.MinValue);
            Assert.AreEqual(double.MinValue, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_NullableDouble_CanBeRead()
        {
            GetWriter().Write((double?)42);
            Assert.AreEqual(42, GetReader().ReadNullableDouble());
        }

        [TestMethod]
        public void Write_NullableDoubleWithNullValue_CanBeRead()
        {
            GetWriter().Write((double?)null);
            Assert.AreEqual(null, GetReader().ReadNullableDouble());
        }

        [TestMethod]
        public void Write_Single_CanBeRead()
        {
            GetWriter().Write(42F);
            Assert.AreEqual(42F, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleMaxValue_CanBeRead()
        {
            GetWriter().Write(float.MaxValue);
            Assert.AreEqual(float.MaxValue, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleZeroValue_CanBeRead()
        {
            GetWriter().Write(0f);
            Assert.AreEqual(0f, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleMinValue_CanBeRead()
        {
            GetWriter().Write(float.MinValue);
            Assert.AreEqual(float.MinValue, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_NullableSingle_CanBeRead()
        {
            GetWriter().Write((float?)42);
            Assert.AreEqual(42, GetReader().ReadNullableSingle());
        }

        [TestMethod]
        public void Write_NullableSingleWithNullValue_CanBeRead()
        {
            GetWriter().Write((float?)null);
            Assert.AreEqual(null, GetReader().ReadNullableSingle());
        }

        [TestMethod]
        public void Write_Guid_CanBeRead()
        {
            Guid guid = Guid.NewGuid();
            GetWriter().Write(guid);
            Assert.AreEqual(guid, GetReader().ReadGuid());
        }

        [TestMethod]
        public void Write_NullableGuid_CanBeRead()
        {
            Guid? guid = Guid.NewGuid();
            GetWriter().Write(guid);
            Assert.AreEqual(guid, GetReader().ReadNullableGuid());
        }

        [TestMethod]
        public void Write_NullableGuidWithNullValue_CanBeRead()
        {
            GetWriter().Write((Guid?)null);
            Assert.AreEqual(null, GetReader().ReadNullableGuid());
        }

        [TestMethod]
        public void Write_DateTimeMaxValue_CanBeRead()
        {            
            GetWriter().Write(DateTime.MaxValue);
            CompareDateTime(DateTime.MaxValue, GetReader().ReadDateTime());
        }

        [TestMethod]
        public void Write_DateTimeMinValue_CanBeRead()
        {
            GetWriter().Write(DateTime.MinValue);
            CompareDateTime(DateTime.MinValue, GetReader().ReadDateTime());
        }

        [TestMethod]
        public void Write_NullableDateTime_CanBeRead()
        {
            DateTime? dateTime = DateTime.Now;
            GetWriter().Write(dateTime);
            CompareDateTime(dateTime.Value, GetReader().ReadNullableDateTime().Value);            
        }

        [TestMethod]
        public void Write_NullableDateTimeWithNullValue_CanBeRead()
        {
            GetWriter().Write((DateTime?)null);
            Assert.AreEqual(null, GetReader().ReadNullableDateTime());
        }

        [TestMethod]
        public void Write_BooleanFalse_CanBeRead()
        {
            GetWriter().Write(false);
            Assert.AreEqual(false, GetReader().ReadBoolean());
        }

        [TestMethod]
        public void Write_BooleanTrue_CanBeRead()
        {
            GetWriter().Write(true);
            Assert.AreEqual(true, GetReader().ReadBoolean());
        }

        [TestMethod]
        public void Write_NullableBooleanTrue_CanBeRead()
        {            
            GetWriter().Write((bool?)true);
            Assert.AreEqual(true, GetReader().ReadNullableBoolean());            
        }

        [TestMethod]
        public void Write_NullableBooleanFalse_CanBeRead()
        {
            GetWriter().Write((bool?)false);
            Assert.AreEqual(false, GetReader().ReadNullableBoolean());
        }

        [TestMethod]
        public void Write_NullableBooleanWithNullValue_CanBeRead()
        {
            GetWriter().Write((bool?)null);
            Assert.AreEqual(null, GetReader().ReadNullableBoolean());
        }

        [TestMethod]
        public void Write_String_CanBeRead()
        {
            GetWriter().Write("SomeValue");
            Assert.AreEqual("SomeValue", GetReader().ReadString());
        }

        [TestMethod]
        public void Write_StringWithNullValue_CanBeRead()
        {
            GetWriter().Write((string)null);
            Assert.AreEqual(null, GetReader().ReadString());
        }

        [TestMethod]
        public void Write_EmptyString_CanBeRead()
        {
            GetWriter().Write(string.Empty);
            Assert.AreEqual(string.Empty, GetReader().ReadString());
        }

        [TestMethod]
        public void Write_CompressedString_CanBeRead()
        {            
            this.GetWriter().Write(Text.LoremIpsum);
            Assert.AreEqual(Text.LoremIpsum, GetReader().ReadString());        
        }
              
        [TestMethod]
        public void Write_KnownType_CanBeRead()
        {
            GetWriter().Write(typeof(string));
            Assert.AreEqual(typeof(string), GetReader().ReadType());
        }

        [TestMethod]
        public void Write_UnKnownType_CanBeRead()
        {
            GetWriter().Write(typeof(SerializableClass));
            Assert.AreEqual(typeof(SerializableClass), GetReader().ReadType());
        }

        [TestMethod]
        public void Write_NullType_CanBeRead()
        {
            GetWriter().Write((Type)null);
            Assert.IsNull(GetReader().ReadType());
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
        
        private void CompareDateTime(DateTime exptectedDateTime, DateTime actualDateTime)
        {
            Assert.AreEqual(exptectedDateTime.Year, actualDateTime.Year);
            Assert.AreEqual(exptectedDateTime.Month, actualDateTime.Month);
            Assert.AreEqual(exptectedDateTime.Day, actualDateTime.Day);
            Assert.AreEqual(exptectedDateTime.Hour, actualDateTime.Hour);
            Assert.AreEqual(exptectedDateTime.Minute, actualDateTime.Minute);
            Assert.AreEqual(exptectedDateTime.Second, actualDateTime.Second);
            Assert.AreEqual(exptectedDateTime.Millisecond, actualDateTime.Millisecond);
        }
    }
}
