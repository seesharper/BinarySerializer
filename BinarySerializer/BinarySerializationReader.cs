namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;
    using System.Text;
    using BinarySerializer.Configuration;
    /// <summary>
    /// A class that is capable of deserializing an object graph from a byte stream. 
    /// </summary>        
    public class BinarySerializationReader : IBinarySerializationReader
    {
        private Stream stream;
        private readonly byte[] buffer = new byte[0x10];
        
        private ReadCache cache = new ReadCache();

        private readonly IDictionary<ulong, string> stringCache = new Dictionary<ulong, string>();
        private readonly IDictionary<ulong, object> objectCache = new Dictionary<ulong, object>();
        private Encoding encoding;

        private static readonly ConcurrentDictionary<Type, Func<BinarySerializationReader, object>> ReadMethods = new ConcurrentDictionary<Type, Func<BinarySerializationReader, object>>(); 

        private readonly SerializerOptions options = new SerializerOptions();

        private readonly ICompressor compressor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySerializationReader"/> class.
        /// </summary>
        /// <param name="stream">The target <see cref="Stream"/></param>        
        public BinarySerializationReader(Stream stream)
        {
            cache.Invalidate();
            this.stream = stream;
            this.VerifySerializerVersion();
            options.Deserialize(this);
            encoding = Encoding.GetEncoding((int)options.CodePage);
            compressor = (ICompressor)Activator.CreateInstance(options.CompressorType);
        }

        static BinarySerializationReader()
        {
            ReadMethods.TryAdd(typeof(byte), (reader) => reader.ReadByte());            
        }

        /// <summary>
        /// Reads an <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="short"/> read from the current stream.</returns>
        public ushort ReadUInt16()
        {
            byte nextByte;
            ushort result = 0;
            int bitShift = 0;
            do
            {
                nextByte = ReadByte();
                result |= (ushort)((nextByte & 0x7f) << bitShift);
                bitShift += 7;
            }
            while ((nextByte & 0x80) != 0);
            return result;
        }

        /// <summary>
        /// Reads a nullable <see cref="ushort"/> from the current stream.
        /// </summary>        
        /// <returns>A nullable <see cref="ushort"/> read from the current stream.</returns>
        public ushort? ReadNullableUInt16()
        {
            return ReadBoolean() ? (ushort?)null : ReadUInt16();            
        }

        /// <summary>
        /// Reads an <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="short"/> read from the current stream.</returns>
        public short ReadInt16()
        {                        
            byte firstByte = this.ReadByte();

            // Check bit number six to see if the value is int.MinValue
            if ((firstByte & 0x20) == 0x20)
            {
                return short.MinValue;
            }

            // Retrieve the first five bits
            var result = (short)(firstByte & 0x1f);
            int bitShift = 5;
            byte nextByte = firstByte;

            // Check the eight bit to see if we must read another byte. 
            while ((nextByte & 0x80) != 0)
            {
                nextByte = this.ReadByte();
                result |= (short)((nextByte & 0x7f) << bitShift);
                bitShift += 7;
            }

            // Check the seventh bit of the first byte to see if we must negate the result.
            return (firstByte & 0x40) == 0x40 ? (short)-result : result;
        }

        /// <summary>
        /// Reads a nullable <see cref="short"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="short"/> read from the current stream.</returns>
        public short? ReadNullableInt16()
        {
            return ReadBoolean() ? (short?)null : ReadInt16();
        }

        /// <summary>
        /// Reads an <see cref="uint"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="uint"/> read from the current stream.</returns>
        public uint ReadUInt32()
        {
            byte nextByte;
            uint result = 0;
            int bitShift = 0;
            do
            {
                nextByte = ReadByte();
                result |= (uint)((nextByte & 0x7f) << bitShift);
                bitShift += 7;
            }
            while ((nextByte & 0x80) != 0);
            return result;
        }

        /// <summary>
        /// Reads a nullable <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="int"/> read from the current stream.</returns>
        public uint? ReadNullableUInt32()
        {
            return ReadBoolean() ? (uint?)null : ReadUInt32();
        }

        /// <summary>
        /// Reads an <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="int"/> read from the current stream.</returns>
        public int ReadInt32()
        {            
            byte firstByte = this.ReadByte();

            // Check bit number six to see if the value is int.MinValue
            if ((firstByte & 0x20) == 0x20)
            {
                return int.MinValue;
            }

            // Retrieve the first five bits
            int result = firstByte & 0x1f;
            int bitShift = 5;
            byte nextByte = firstByte;
            
            // Check the eight bit to see if we must read another byte. 
            while ((nextByte & 0x80) != 0)
            {
                nextByte = this.ReadByte();
                result |= (nextByte & 0x7f) << bitShift;
                bitShift += 7;                    
            }
            
            // Check the seventh bit of the first byte to see if we must negate the result.
            return (firstByte & 0x40) == 0x40 ? -result : result;
        }

        /// <summary>
        /// Reads a nullable <see cref="int"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="int"/> read from the current stream.</returns>
        public int? ReadNullableInt32()
        {
            return ReadBoolean() ? (int?)null : ReadInt32();
        }

        /// <summary>
        /// Reads an <see cref="ulong"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="ulong"/> read from the current stream.</returns>
        public ulong ReadUInt64()
        {
            byte nextByte;
            ulong result = 0;
            int bitShift = 0;
            do
            {
                nextByte = ReadByte();
                result |= (ulong)((nextByte & 0x7f) << bitShift);
                bitShift += 7;
            }
            while ((nextByte & 0x80) != 0);
            return result;
        }

        /// <summary>
        /// Reads a nullable <see cref="ulong"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="ulong"/> read from the current stream.</returns>
        public ulong? ReadNullableUInt64()
        {
            return ReadBoolean() ? (ulong?)null : ReadUInt64();
        }

        /// <summary>
        /// Reads an <see cref="long"/> from the current stream.
        /// </summary>
        /// <returns>An <see cref="long"/> read from the current stream.</returns>
        public long ReadInt64()
        {
            byte firstByte = this.ReadByte();

            // Check bit number six to see if the value is int.MinValue
            if ((firstByte & 0x20) == 0x20)
            {
                return long.MinValue;
            }

            // Retrieve the first five bits
            long result = firstByte & 0x1f;
            int bitShift = 5;
            byte nextByte = firstByte;

            // Check the eight bit to see if we must read another byte. 
            while ((nextByte & 0x80) != 0)
            {
                nextByte = this.ReadByte();
                result |= ((long)nextByte & 0x7f) << bitShift;
                bitShift += 7;
            }

            // Check the seventh bit of the first byte to see if we must negate the result.
            return (firstByte & 0x40) == 0x40 ? -result : result;
        }

        /// <summary>
        /// Reads a nullable <see cref="long"/> from the current stream.
        /// </summary>
        /// <returns>A nullable <see cref="long"/> read from the current stream.</returns>
        public long? ReadNullableInt64()
        {
            return ReadBoolean() ? (int?)null : ReadInt32();
        }

        /// <summary>
        /// Reads the next <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> read from the current stream.</returns>
        public byte ReadByte()
        {
            return (byte)this.stream.ReadByte();
        }

        /// <summary>
        /// Reads the next <see cref="byte"/> array from the current stream.
        /// </summary>
        /// <returns>The next <see cref="byte"/> array read from the current stream.</returns>
        public byte[] ReadBytes()
        {
            ulong metadata = ReadUInt64();
            
            // Check the first bit of the metadata to determine if the value is null.
            if ((metadata & 0x1) == 0x1)
            {
                return null;
            }

            int length = (int)metadata >> 1;
            var readBuffer = new byte[length];
            stream.Read(readBuffer, 0, length);
            return readBuffer;
        }

        /// <summary>
        /// Reads the next nullable <see cref="byte"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="byte"/> read from the current stream.</returns>
        public byte? ReadNullableByte()
        {
            return ReadBoolean() ? (byte?)null : this.ReadByte();
        }
        
        /// <summary>
        /// Reads the next <see cref="decimal"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="decimal"/> read from the current stream.</returns>
        public decimal ReadDecimal()
        {
            var bits = new[] { ReadInt32(), ReadInt32(), ReadInt32(), ReadInt32() };
            return new decimal(bits);
        }

        /// <summary>
        /// Reads the next nullable <see cref="decimal"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="decimal"/> read from the current stream.</returns>
        public decimal? ReadNullableDecimal()
        {
            return ReadBoolean() ? (decimal?)null : this.ReadDecimal();
        }

        /// <summary>
        /// Reads the next <see cref="float"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="float"/> read from the current stream.</returns>
        public float ReadSingle()
        {
            stream.Read(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Reads the next nullable <see cref="float"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="float"/> read from the current stream.</returns>
        public float? ReadNullableSingle()
        {
            return ReadBoolean() ? (float?)null : this.ReadSingle();
        }

        /// <summary>
        /// Reads the next <see cref="double"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="double"/> read from the current stream.</returns>
        [SecuritySafeCritical]
        public double ReadDouble()
        {
            stream.Read(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Reads the next nullable <see cref="double"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="double"/> read from the current stream.</returns>
        public double? ReadNullableDouble()
        {
            return ReadBoolean() ? (double?)null : this.ReadDouble();
        }

        /// <summary>
        /// Reads the next <see cref="Guid"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="Guid"/> read from the current stream.</returns>
        public Guid ReadGuid()
        {
            stream.Read(buffer, 0, 0x10);
            return new Guid(buffer);
        }

        /// <summary>
        /// Reads the next nullable <see cref="Guid"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="Guid"/> read from the current stream.</returns>
        public Guid? ReadNullableGuid()
        {
            return ReadBoolean() ? (Guid?)null : this.ReadGuid();
        }

        /// <summary>
        /// Reads the next <see cref="DateTime"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="DateTime"/> read from the current stream.</returns>
        public DateTime ReadDateTime()
        {
            var packedDate = new BitVector32(ReadInt32());
            int year = packedDate[DateTimeBitVectorSections.Year];
            int month = packedDate[DateTimeBitVectorSections.Month];
            int day = packedDate[DateTimeBitVectorSections.Day];
            bool hasTime = packedDate[DateTimeBitVectorSections.HasTime] == 1;
            if (hasTime)
            {
                var packedTime = new BitVector32(ReadInt32());
                int hour = packedTime[DateTimeBitVectorSections.Hour];
                int minute = packedTime[DateTimeBitVectorSections.Minute];
                int second = packedTime[DateTimeBitVectorSections.Second];
                int milliSecond = packedTime[DateTimeBitVectorSections.MilliSecond];
                return new DateTime(year, month, day, hour, minute, second, milliSecond);                
            }
            
            return new DateTime(year, month, day);            
        }

        /// <summary>
        /// Reads the next nullable <see cref="DateTime"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="DateTime"/> read from the current stream.</returns>
        public DateTime? ReadNullableDateTime()
        {
            return ReadBoolean() ? (DateTime?)null : this.ReadDateTime();
        }

        /// <summary>
        /// Reads the next <see cref="bool"/> from the current stream.
        /// </summary>
        /// <returns>The next <see cref="bool"/> read from the current stream.</returns>
        public bool ReadBoolean()
        {
            return ReadByte() != 0;
        }

        /// <summary>
        /// Reads the next nullable <see cref="bool"/> from the current stream.
        /// </summary>
        /// <returns>The next nullable <see cref="bool"/> read from the current stream.</returns>
        public bool? ReadNullableBoolean()
        {
            byte byteRead = ReadByte();
            
            // Check the second bit to see if the value is null.
            if ((byteRead & 0x2) == 0x2)
            {
                return null;
            }

            return byteRead != 0;
        }

        public string ReadString()
        {                        
            var metadata = ReadUInt64();
            if (metadata == 0)
            {
                return null;
            }
            
            ulong token = metadata >> 1; 
                       
            string stringValue;
            stringCache.TryGetValue(token, out stringValue);
            if (stringValue == null)
            {
                byte[] stringBuffer;
                                
                // Check the first bit to determine if the string is compressed.
                if ((metadata & 0x1) == 0x1)
                {
                    uint length = ReadUInt32();
                    stringBuffer = ReadBytes();                
                    stringBuffer = compressor.Decompress(stringBuffer, length);
                }
                else
                {
                    stringBuffer = ReadBytes();                
                }
                stringValue = encoding.GetString(stringBuffer);
                stringCache.Add(token, stringValue);
            }

            return stringValue;
        }

        public T Read<T>()
        {
            Type type = ReadType();
            if (type == null)
            {
                return default(T);
            }
            Func<BinarySerializationReader, T> readMethod = CreateReadMethod<T>(type);

            return readMethod(this);
        }


        //private Func<BinarySerializationReader, object> GetReadMethod(Type type)
        //{
        //    return ReadMethods.GetOrAdd(type, ResolveReadMethod);
        //}

        //private Func<BinarySerializationReader, object> ResolveReadMethod(Type type)
        //{
        //    if (typeof(IBinarySerializable).IsAssignableFrom(type))
        //    {
        //        return reader => reader.ReadBinarySerializableObject();
        //    }


        //    return reader => reader.ReadSerializableObject();
        //}

        private Func<BinarySerializationReader, T> CreateReadMethod<T>(Type actualType)
        {
            var dynamicMethod = new DynamicMethod("DynamicMethod", typeof(T), new[] { typeof(BinarySerializationReader) }, typeof(BinarySerializationReader).Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            var methodInfo = BinarySerializer.ReadMethods.GetReadMethod(actualType);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, methodInfo);
            if (!typeof(T).IsValueType && actualType.IsValueType)
            {
                il.Emit(OpCodes.Box, actualType);
            }
            // Todo check T to see if we need to box the value

            il.Emit(OpCodes.Ret);
            return (Func<BinarySerializationReader, T>)dynamicMethod.CreateDelegate(typeof(Func<BinarySerializationReader, T>));
        }


        /// <summary>
        /// Reads the next <see cref="IBinarySerializable"/> object from the current stream.
        /// </summary>        
        /// <returns>The next <see cref="IBinarySerializable"/> object read from the current stream.</returns>
        internal T ReadBinarySerializableObject<T>() where T : IBinarySerializable, new() 
        {
            var token = ReadUInt64();
            if (token == 0)
            {
                return default(T);
            }

            T value;
            if (!cache.TryGetValue(token, out value))
            {
                //Type type = this.ReadType();                
                value = new T();
                cache.Add(token, value);
                ((IBinarySerializable)value).Deserialize(this);                
            }
            return value;
        }

        public Type ReadType()
        {
            byte typeCode = ReadByte();
            if (typeCode == TypeCodes.Null)
            {
                return null;
            }

            if (typeCode <= TypeCodes.String)
            {
                return TypeCodes.GetType(typeCode);
            }

            return TypeHelper.GetType(ReadString());
        }

        internal TCollection ReadCollectionInternal<TCollection, TValue>() where TCollection : ICollection<TValue>, new()
        {
            ulong token = ReadUInt64();
            if (token == 0)
            {
                return default(TCollection);
            }
            
            TCollection collection;            
            if (!cache.TryGetValue(token, out collection))
            {
                collection  = new TCollection();
                cache.Add(token, collection);
                uint count = ReadUInt32();
                for (uint i = 0; i < count; i++)
                {
                    collection.Add(Read<TValue>());
                }
            }

            return collection;
        }
         
     

        private object Activate(Type type)
        {
            return Cache<Type, Func<object>>.GetOrAdd(type, CreateActivatorDelegate)();
        }

        private Func<object> CreateActivatorDelegate(Type type)
        {
            ConstructorInfo constructorInfo = Cache<Type, ConstructorInfo>.GetOrAdd(type, GetConstructorInfo);
            var dynamicMethod = new DynamicMethod("ActivatorMethod", typeof(object),Type.EmptyTypes,type.Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Newobj, constructorInfo);
            il.Emit(OpCodes.Ret);
            return (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
        }

        private ConstructorInfo GetConstructorInfo(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes);
        }

        private object ReadSerializableObject()
        {
            ulong token = ReadUInt64();
            object value;
            if (!objectCache.TryGetValue(token, out value))
            {
                var binaryFormatter = new BinaryFormatter();
                value = binaryFormatter.Deserialize(stream);
                objectCache.Add(token, value);
            }
            return value;
        }

        

         private Func<object> GetObjectReadMethod(byte typeCode)
         {
             if (typeCode == TypeCodes.Byte)
             {
                 Func<object> method = () => (object)ReadByte();
                 return method;
             }

             if (typeCode == TypeCodes.Object)
             {
                 return ReadSerializableObject;                 
             }

             throw new NotSupportedException();
         }

         private void VerifySerializerVersion()
         {
             Version thisVersion = typeof(BinarySerializationWriter).Assembly.GetName().Version;
             Version streamVersion = new Version((int)this.ReadUInt32(), (int)this.ReadUInt32(), (int)this.ReadUInt32(), (int)this.ReadUInt32());
             if (thisVersion != streamVersion)
             {
                 throw new InvalidOperationException();
             }
         }
    }
}