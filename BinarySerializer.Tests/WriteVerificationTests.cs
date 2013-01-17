namespace BinarySerializer.Tests
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WriteVerificationTests
    {
        protected readonly MemoryStream Stream = new MemoryStream();

        private string dynamicAssemblyPath;

        private MethodBuilderWriteMethodSkeleton methodSkeleton;

        [TestMethod]
        public void Verify_Byte_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<byte>(42));
        }

        [TestMethod]
        public void Verify_NullableByte_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<byte?>(42));
        }

        [TestMethod]
        public void Verify_NullableByteWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<byte?>(null));
        }
        
        [TestMethod]
        public void Verify_ByteAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((byte)42));
        }

        [TestMethod]
        public void Verify_NullableByteAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((byte?)42));
        }

        [TestMethod]
        public void Verify_SByte_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<sbyte>(42));
        }

        [TestMethod]
        public void Verify_NullableSByte_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<sbyte?>(42));
        }

        [TestMethod]
        public void Verify_NullableSByteWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<sbyte?>(null));
        }

        [TestMethod]
        public void Verify_SByteAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((sbyte)42));
        }

        [TestMethod]
        public void Verify_NullableSByteAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((sbyte?)42));
        }

        [TestMethod]
        public void Verify_Boolean_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<bool>(true));
        }

        [TestMethod]
        public void Verify_NullableBoolean_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<bool?>(true));
        }

        [TestMethod]
        public void Verify_NullableBooleanWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<bool?>(null));
        }

        [TestMethod]
        public void Verify_BooleanAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(true));
        }

        [TestMethod]
        public void Verify_NullableBooleanAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((bool?)true));
        }

        [TestMethod]
        public void Verify_Int16_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<short>(42));
        }

        [TestMethod]
        public void Verify_NullableInt16_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<short?>(42));
        }

        [TestMethod]
        public void Verify_NullableInt16WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<short?>(null));
        }

        [TestMethod]
        public void Verify_Int16AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((short)42));
        }

        [TestMethod]
        public void Verify_NullableInt16AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((short?)42));
        }

        [TestMethod]
        public void Verify_UInt16_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ushort>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt16_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ushort?>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt16WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ushort?>(null));
        }

        [TestMethod]
        public void Verify_UInt16AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((ushort)42));
        }

        [TestMethod]
        public void Verify_NullableUInt16AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((ushort?)42));
        }

        [TestMethod]
        public void Verify_Int32_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<int>(42));
        }

        [TestMethod]
        public void Verify_NullableInt32_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<int?>(42));
        }

        [TestMethod]
        public void Verify_NullableInt32WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<int?>(null));
        }

        [TestMethod]
        public void Verify_Int32AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(42));
        }

        [TestMethod]
        public void Verify_NullableInt32AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((int?)42));
        }

        [TestMethod]
        public void Verify_UInt32_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<uint>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt32_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<uint?>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt32WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<uint?>(null));
        }

        [TestMethod]
        public void Verify_UInt32AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((uint)42));
        }

        [TestMethod]
        public void Verify_NullableUInt32AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((uint?)42));
        }

        [TestMethod]
        public void Verify_Int64_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<long>(42));
        }

        [TestMethod]
        public void Verify_NullableInt64_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<long?>(42));
        }

        [TestMethod]
        public void Verify_NullableInt64WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<long?>(null));
        }

        [TestMethod]
        public void Verify_Int64AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((long)42));
        }
        
        [TestMethod]
        public void Verify_NullableInt64AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((long?)42));
        }

        [TestMethod]
        public void Verify_UInt64_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ulong>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt64_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ulong?>(42));
        }

        [TestMethod]
        public void Verify_NullableUInt642WithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ulong?>(null));
        }

        [TestMethod]
        public void Verify_UInt64AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((ulong)42));
        }

        [TestMethod]
        public void Verify_NullableUInt64AsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((uint?)42));
        }

        [TestMethod]
        public void Verify_Decimal_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<decimal>(42m));
        }

        [TestMethod]
        public void Verify_NullableDecimal_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<decimal?>(42m));
        }

        [TestMethod]
        public void Verify_NullableDecimalWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<decimal?>(null));
        }

        [TestMethod]
        public void Verify_DecimalAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(42m));
        }

        [TestMethod]
        public void Verify_NullableDecimalAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((decimal?)42m));
        }

        [TestMethod]
        public void Verify_Double_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<double>(42d));
        }

        [TestMethod]
        public void Verify_NullableDouble_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<double?>(42d));
        }

        [TestMethod]
        public void Verify_NullableDoubleWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<double?>(null));
        }

        [TestMethod]
        public void Verify_DoubleAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(42d));
        }

        [TestMethod]
        public void Verify_NullableDoubleAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((double?)42d));
        }

        [TestMethod]
        public void Verify_Single_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<float>(42f));
        }

        [TestMethod]
        public void Verify_NullableSingle_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<float?>(42f));
        }

        [TestMethod]
        public void Verify_NullableSingleWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<float?>(null));
        }

        [TestMethod]
        public void Verify_SingleAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(42f));
        }

        [TestMethod]
        public void Verify_NullableSingleAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((float?)42d));
        }

        [TestMethod]
        public void Verify_Guid_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Guid>(new Guid()));
        }

        [TestMethod]
        public void Verify_NullableGuid_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Guid?>(new Guid()));
        }

        [TestMethod]
        public void Verify_NullableGuidWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Guid?>(null));
        }

        [TestMethod]
        public void Verify_GuidAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(new Guid()));
        }

        [TestMethod]
        public void Verify_NullableGuidAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((Guid?)new Guid()));
        }

        [TestMethod]
        public void Verify_DateTime_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<DateTime>(DateTime.Now));
        }

        [TestMethod]
        public void Verify_NullableDateTime_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<DateTime?>(DateTime.Now));
        }

        [TestMethod]
        public void Verify_NullableDateTimeWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<DateTime?>(null));
        }

        [TestMethod]
        public void Verify_DateTimeAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(DateTime.Now));
        }

        [TestMethod]
        public void Verify_NullableDateTimeAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>((DateTime?)DateTime.Now));
        }

        [TestMethod]
        public void Verify_Type_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Type>(typeof(string)));
        }
               
        [TestMethod]
        public void Verify_TypeAsObject_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<object>(typeof(string)));
        }

        [TestMethod]
        public void Verify_ByteEnum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(ByteEnum.Fri));
        }

        [TestMethod]
        public void Verify_NullableByteEnum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ByteEnum?>(ByteEnum.Fri));
        }

        [TestMethod]
        public void Verify_NullableByteEnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<ByteEnum?>(null));
        }

        [TestMethod]
        public void Verify_SByteEnum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(SByteEnum.Fri));
        }

        [TestMethod]
        public void Verify_NullableSByteEnum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<SByteEnum?>(SByteEnum.Fri));
        }

        [TestMethod]
        public void Verify_NullableSByteEnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<SByteEnum?>(null));
        }

        [TestMethod]
        public void Verify_Int16Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(Int16Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt16Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int16Enum?>(Int16Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt16EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int16Enum?>(null));
        }

        [TestMethod]
        public void Verify_UInt16Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(UInt16Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt16Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt16Enum?>(UInt16Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt16EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt16Enum?>(null));
        }

        [TestMethod]
        public void Verify_Int32Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(Int32Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt32Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int32Enum?>(Int32Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt32EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int32Enum?>(null));
        }

        [TestMethod]
        public void Verify_UInt32Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(UInt32Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt32Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt32Enum?>(UInt32Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt32EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt32Enum?>(null));
        }

        [TestMethod]
        public void Verify_Int64Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(Int64Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt64Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int64Enum?>(Int64Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableInt64EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<Int64Enum?>(null));
        }

        [TestMethod]
        public void Verify_UInt64Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write(UInt64Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt64Enum_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt64Enum?>(UInt64Enum.Fri));
        }

        [TestMethod]
        public void Verify_NullableUInt64EnumWithNullValue_GeneratesValidAssembly()
        {
            Verify(() => GetWriter().Write<UInt64Enum?>(null));
        }

        private void Verify(Action writeAction)
        {
            writeAction();
            methodSkeleton.Verify();
        }

        [TestInitialize]
        public void InitializeTest()
        {
            dynamicAssemblyPath = GetDynamicAssemblyPath();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            try
            {
                File.Delete(dynamicAssemblyPath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static string GetDynamicAssemblyPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DynamicAssembly{0}.dll".FormatWith(Guid.NewGuid()));
        }

        private IBinarySerializationWriter GetWriter()
        {
            Stream.SetLength(0);
            var writer = new BinarySerializationWriter(this.Stream);
            methodSkeleton = new MethodBuilderWriteMethodSkeleton(GetDynamicAssemblyPath());
            writer.WriteMethodFactory = new WriteMethodFactory(methodSkeleton);
            return writer;
        }
    }
}