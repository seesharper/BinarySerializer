using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Tests
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq.Expressions;

    [TestClass]
    public class SimpleTypesSerializatitonTests
    {
        private readonly MemoryStream stream = new MemoryStream();
        
        [TestMethod]
        public void Write_Byte_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((byte)42);
            }            

            Assert.AreEqual((byte)42, GetReader().ReadByte());            
        }

        [TestMethod]
        public void Write_ByteWithMinValue_CanBeRead()
        {            
            using (var writer = GetWriter())
            {
                writer.Write(byte.MinValue);
            }        

            Assert.AreEqual(byte.MinValue, GetReader().ReadByte());
        }

        [TestMethod]
        public void Write_ByteWithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(byte.MaxValue);
            }        

            Assert.AreEqual(byte.MaxValue, GetReader().ReadByte());
        }

        [TestMethod]
        public void Write_ByteArray_CanBeRead()
        {
            var bytesToWrite = new byte[] { 0xf, 0xff };
            
            using (var writer = GetWriter())
            {
                writer.Write(bytesToWrite);
            }        

            var bytesRead = GetReader().ReadBytes();
            Assert.IsTrue(bytesRead.Intersect(bytesToWrite).Count() == 2);
        }

        [TestMethod]
        public void Write_NullByteArray_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((byte[])null);
            }                                
            Assert.IsNull(GetReader().ReadBytes());
        }


        [TestMethod]
        public void Write_NullableByte_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((byte?)42);
            }                                
            
            Assert.AreEqual((byte?)42, GetReader().ReadNullableByte());
        }

        [TestMethod]
        public void Write_NullableByteWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((byte?)null);
            }                                
            
            Assert.AreEqual(null, GetReader().ReadNullableByte());
        }

        [TestMethod]
        public void Write_SByte_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((sbyte)42);
            }                                
                        
            Assert.AreEqual((sbyte)42, GetReader().ReadSByte());
        }

        [TestMethod]
        public void Write_SByteWithMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(sbyte.MinValue);
            }                                
                        
            Assert.AreEqual(sbyte.MinValue, GetReader().ReadSByte());
        }

        [TestMethod]
        public void Write_SByteWithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(sbyte.MaxValue);
            }
                                                
            Assert.AreEqual(sbyte.MaxValue, GetReader().ReadSByte());
        }
        

        [TestMethod]
        public void Write_NullableSByte_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((sbyte?)42);
            }
                                    
            Assert.AreEqual((sbyte?)42, GetReader().ReadNullableSByte());
        }

        [TestMethod]
        public void Write_NullableSByteWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((sbyte?)null);
            }
                        
            Assert.AreEqual(null, GetReader().ReadNullableSByte());
        }

        [TestMethod]
        public void Write_UInt16_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ushort)42);
            }
                        
            Assert.AreEqual((ushort)42, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_UInt16WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(ushort.MaxValue);
            }
                       
            Assert.AreEqual(ushort.MaxValue, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_UInt16WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ushort)0);
            }
                       
            Assert.AreEqual(0, GetReader().ReadUInt16());
        }

        [TestMethod]
        public void Write_NullableUInt16_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ushort?)42);
            }
                       
            Assert.AreEqual((ushort?)42, GetReader().ReadNullableUInt16());
        }

        [TestMethod]
        public void Write_NullableUInt16WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ushort?)null);
            }
                       
            Assert.AreEqual(null, GetReader().ReadNullableUInt16());
        }

        [TestMethod]
        public void Write_Int16_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((short)42);
            }
                       
            Assert.AreEqual((short)42, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_Int16WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(short.MaxValue);
            }
                        
            Assert.AreEqual(short.MaxValue, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_Int16WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((short)0);
            }
                       
            Assert.AreEqual(0, GetReader().ReadInt16());
        }
                
        [TestMethod]
        public void Write_Int16WithMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(short.MinValue);
            }
                        
            Assert.AreEqual(short.MinValue, GetReader().ReadInt16());
        }

        [TestMethod]
        public void Write_NullableInt16_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((short?)42);
            }
            
            Assert.AreEqual((short?)42, GetReader().ReadNullableInt16());
        }

        [TestMethod]
        public void Write_NullableInt16WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((short?)null);
            }
                        
            Assert.AreEqual(null, GetReader().ReadNullableInt16());
        }

        [TestMethod]
        public void Write_UInt32_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((uint)42);
            }
                        
            Assert.AreEqual((uint)42, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_UInt32WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(uint.MaxValue);
            }
                       
            Assert.AreEqual(uint.MaxValue, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_UInt32WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((uint)0);
            }
                        
            Assert.AreEqual((uint)0, GetReader().ReadUInt32());
        }

        [TestMethod]
        public void Write_NullableUInt32_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((uint?)42);
            }
                        
            Assert.AreEqual((uint?)42, GetReader().ReadNullableUInt32());
        }

        [TestMethod]
        public void Write_NullableUInt32WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((uint?)null);
            }            

            Assert.AreEqual(null, GetReader().ReadNullableUInt32());
        }

        [TestMethod]
        public void Write_Int32_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(42);
            }
                        
            Assert.AreEqual(42, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_Int32WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(0);
            }
                       
            Assert.AreEqual(0, GetReader().ReadInt32());
        }
        
        [TestMethod]
        public void Write_Int32WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(int.MaxValue);
            }
                       
            Assert.AreEqual(int.MaxValue, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_Int32WithMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(int.MinValue);
            }
                        
            Assert.AreEqual(int.MinValue, GetReader().ReadInt32());
        }

        [TestMethod]
        public void Write_NullableInt32_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((int?)42);
            }
            
            Assert.AreEqual(42, GetReader().ReadNullableInt32());
        }

        [TestMethod]
        public void Write_NullableInt32WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((int?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableInt32());
        }

        [TestMethod]
        public void Write_UInt64_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ulong)42);
            }

            Assert.AreEqual((ulong)42, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_UInt64WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(ulong.MaxValue);
            }

            Assert.AreEqual(ulong.MaxValue, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_UInt64WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ulong)0);
            }
            
            Assert.AreEqual((ulong)0, GetReader().ReadUInt64());
        }

        [TestMethod]
        public void Write_NullableUInt64_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ulong?)42);
            }
            
            Assert.AreEqual((ulong?)42, GetReader().ReadNullableUInt64());
        }

        [TestMethod]
        public void Write_NullableUInt64WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((ulong?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableUInt64());
        }

        [TestMethod]
        public void Write_Int64_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((long)42);
            }
            
            Assert.AreEqual(42, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((long)0);
            }
            
            Assert.AreEqual(0, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(long.MaxValue);
            }
            
            Assert.AreEqual(long.MaxValue, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_Int64WithMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(long.MinValue);
            }
            
            Assert.AreEqual(long.MinValue, GetReader().ReadInt64());
        }

        [TestMethod]
        public void Write_NullableInt64_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((long?)42);
            }
            
            Assert.AreEqual(42, GetReader().ReadNullableInt64());
        }

        [TestMethod]
        public void Write_NullableInt64WithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((long?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableInt64());
        }

        [TestMethod]
        public void Write_Decimal_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(42m);
            }
            
            Assert.AreEqual(42m, GetReader().ReadDecimal());
        }

        [TestMethod]
        public void Write_DecimalMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(decimal.MaxValue);
            }
            
            Assert.AreEqual(decimal.MaxValue, GetReader().ReadDecimal());
        }

        [TestMethod]
        public void Write_DecimalMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(decimal.MinValue);
            }
            
            Assert.AreEqual(decimal.MinValue, GetReader().ReadDecimal());           
        }

        [TestMethod]
        public void Write_NullableDecimal_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((decimal?)42);
            }
            
            Assert.AreEqual(42, GetReader().ReadNullableDecimal());
        }

        [TestMethod]
        public void Write_NullableDecimalWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((decimal?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableDecimal());
        }

        [TestMethod]
        public void Write_Double_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(42d);
            }
            
            Assert.AreEqual(42d, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_DoubleMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(double.MaxValue);
            }
            
            Assert.AreEqual(double.MaxValue, GetReader().ReadDouble());           
        }

        [TestMethod]
        public void Write_DoubleZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(0d);
            }
                        
            Assert.AreEqual(0d, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_DoubleMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(double.MinValue);
            }
            
            Assert.AreEqual(double.MinValue, GetReader().ReadDouble());
        }

        [TestMethod]
        public void Write_NullableDouble_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((double?)42);
            }

            Assert.AreEqual(42, GetReader().ReadNullableDouble());
        }

        [TestMethod]
        public void Write_NullableDoubleWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((double?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableDouble());
        }

        [TestMethod]
        public void Write_Single_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(42F);
            }
            
            Assert.AreEqual(42F, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(float.MaxValue);
            }
            
            Assert.AreEqual(float.MaxValue, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleZeroValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(0f);
            }
            
            Assert.AreEqual(0f, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_SingleMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(float.MinValue);
            }
            
            Assert.AreEqual(float.MinValue, GetReader().ReadSingle());
        }

        [TestMethod]
        public void Write_NullableSingle_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((float?)42);
            }
            
            Assert.AreEqual(42, GetReader().ReadNullableSingle());
        }

        [TestMethod]
        public void Write_NullableSingleWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((float?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableSingle());
        }

        [TestMethod]
        public void Write_Guid_CanBeRead()
        {
            Guid guid = Guid.NewGuid();

            using (var writer = GetWriter())
            {
                writer.Write(guid);
            }
            
            Assert.AreEqual(guid, GetReader().ReadGuid());
        }

        [TestMethod]
        public void Write_NullableGuid_CanBeRead()
        {
            Guid? guid = Guid.NewGuid();
            using (var writer = GetWriter())
            {
                writer.Write(guid);
            }

            Assert.AreEqual(guid, GetReader().ReadNullableGuid());
        }

        [TestMethod]
        public void Write_NullableGuidWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((Guid?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableGuid());
        }

        [TestMethod]
        public void Write_DateTimeMaxValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(DateTime.MaxValue);
            }
            
            CompareDateTime(DateTime.MaxValue, GetReader().ReadDateTime());
        }

        [TestMethod]
        public void Write_DateTimeMinValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(DateTime.MinValue);
            }
            
            CompareDateTime(DateTime.MinValue, GetReader().ReadDateTime());
        }

        [TestMethod]
        public void Write_NullableDateTime_CanBeRead()
        {
            DateTime? dateTime = DateTime.Now;
            using (var writer = GetWriter())
            {
                writer.Write(dateTime);
            }

            CompareDateTime(dateTime.Value, GetReader().ReadNullableDateTime().Value);            
        }

        [TestMethod]
        public void Write_NullableDateTimeWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((DateTime?)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadNullableDateTime());
        }

        [TestMethod]
        public void Write_BooleanFalse_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(false);
            }
            
            Assert.AreEqual(false, GetReader().ReadBoolean());
        }

        [TestMethod]
        public void Write_BooleanTrue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(true);
            }
            
            Assert.AreEqual(true, GetReader().ReadBoolean());
        }

        [TestMethod]
        public void Write_NullableBooleanTrue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((bool?)true);
            }
            
            Assert.AreEqual(true, GetReader().ReadNullableBoolean());            
        }

        [TestMethod]
        public void Write_NullableBooleanFalse_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((bool?)false);
            }
            
            Assert.AreEqual(false, GetReader().ReadNullableBoolean());
        }

        [TestMethod]
        public void Write_NullableBooleanWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((bool?)null);
            }
                        
            Assert.AreEqual(null, GetReader().ReadNullableBoolean());
        }

        [TestMethod]
        public void Write_String_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write("SomeValue");
            }
            
            Assert.AreEqual("SomeValue", GetReader().ReadString());
        }

        [TestMethod]
        public void Write_StringWithNullValue_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((string)null);
            }
            
            Assert.AreEqual(null, GetReader().ReadString());
        }

        [TestMethod]
        public void Write_EmptyString_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(string.Empty);
            }
            
            Assert.AreEqual(string.Empty, GetReader().ReadString());
        }

        [TestMethod]
        public void Write_CompressedString_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write(Text.LoremIpsum);
            }
            
            Assert.AreEqual(Text.LoremIpsum, GetReader().ReadString());        
        }

        private Serializer GetWriter()
        {
            stream.SetLength(0);
            return new Serializer(new Writer(stream));
        }

        private Deserializer GetReader()
        {
            stream.Position = 0;
            return new Deserializer(new Reader(stream)); 
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

        [TestMethod]
        public void Write_ByteEnum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(ByteEnum.Fri);
            }
            
            Assert.AreEqual(ByteEnum.Fri, GetReader().ReadEnum<ByteEnum>());    
        }

        [TestMethod]
        public void Write_SByteEnum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(SByteEnum.Fri);
            }

            Assert.AreEqual(SByteEnum.Fri, GetReader().ReadEnum<SByteEnum>());
        }

        [TestMethod]
        public void Write_Int16Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(Int16Enum.Fri);
            }

            Assert.AreEqual(Int16Enum.Fri, GetReader().ReadEnum<Int16Enum>());
        }

        [TestMethod]
        public void Write_UInt16Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(UInt16Enum.Fri);
            }

            Assert.AreEqual(UInt16Enum.Fri, GetReader().ReadEnum<UInt16Enum>());
        }

        [TestMethod]
        public void Write_Int32Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(Int32Enum.Fri);
            }

            Assert.AreEqual(Int32Enum.Fri, GetReader().ReadEnum<Int32Enum>());
        }

        [TestMethod]
        public void Write_UInt32Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(UInt32Enum.Fri);
            }

            Assert.AreEqual(UInt32Enum.Fri, GetReader().ReadEnum<UInt32Enum>());
        }

        [TestMethod]
        public void Write_Int64Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(Int64Enum.Fri);
            }

            Assert.AreEqual(Int64Enum.Fri, GetReader().ReadEnum<Int64Enum>());
        }

        [TestMethod]
        public void Write_UInt64Enum_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.WriteEnum(UInt64Enum.Fri);
            }

            Assert.AreEqual(UInt64Enum.Fri, GetReader().ReadEnum<UInt64Enum>());
        }

        [TestMethod]
        public void Write_Collection_CanBeRead()
        {
            var collection = new Collection<int>() { 42, 84 };

            using (var writer = GetWriter())
            {
                writer.WriteCollection(collection);
            }

            var result = (Collection<int>)GetReader().ReadCollection<int>();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(42, result[0]);
            Assert.AreEqual(84, result[1]);
        }


        [TestMethod]
        public void Write_CollectionWithNullValue_CanBeRead()
        {
            var collection = new Collection<byte?>() { null, 84 };

            using (var writer = GetWriter())
            {
                writer.WriteCollection(collection);
            }

            var result = (Collection<byte?>)GetReader().ReadCollection<byte?>();
            Assert.AreEqual(2, result.Count);
            Assert.IsNull(result[0]);
            Assert.AreEqual((byte)84, collection[1]);
        }


        [TestMethod]
        public void Write_ByteAsObject_CanBeRead()
        {
            using (var writer = GetWriter())
            {
                writer.Write((object)42);
            }

            Assert.AreEqual((byte)42, GetReader().ReadObject());        
        }
    }
}
