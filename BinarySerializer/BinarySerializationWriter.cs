namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    using BinarySerializer.Configuration;

    /// <summary>
    /// A class that is capable of serializing an object graph to a byte stream.
    /// </summary>
    public class BinarySerializationWriter : IBinarySerializationWriter
    {       
        private Stream stream;

        private WriteCache cache = new WriteCache();

        private readonly SerializerOptions options;

        private readonly byte[] buffer = new byte[0x10];

        private readonly Dictionary<string, ulong> stringCache = new Dictionary<string, ulong>();
        private readonly IDictionary<object, ulong> objectCache = new Dictionary<object, ulong>();

        private readonly Encoding encoding;

        private ICompressor compressor;

        private static readonly ConcurrentDictionary<Type, Action<BinarySerializationWriter, object>> WriterMethods = new ConcurrentDictionary<Type, Action<BinarySerializationWriter, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySerializationWriter"/> class.
        /// </summary>
        /// <param name="stream">
        /// The target <see cref="Stream"/> to to write to.
        /// </param>
        public BinarySerializationWriter(Stream stream):this(stream, GetSerializationOptions())
        {
            cache.Invalidate();    
        }
        public BinarySerializationWriter(Stream stream, SerializerOptions options)
        {
            WriteMethodFactory = new WriteMethodFactory(new WriteMethodSkeleton());
            this.stream = stream;
            this.options = options;
            WriteAssemblyVersion();
            WriteSerializerOptions(options);
            encoding = Encoding.GetEncoding((int)options.CodePage);
            compressor = (ICompressor)Activator.CreateInstance(options.CompressorType);
        }

        public IWriteMethodFactory WriteMethodFactory { get; set; }

        static BinarySerializationWriter()
        {
            WriterMethods.TryAdd(typeof(byte), (writer, value) => writer.Write((byte)value));
            WriterMethods.TryAdd(typeof(int), (writer, value) => writer.Write((int)value));
        }

        private void WriteAssemblyVersion()
        {
            Version assembly = typeof(BinarySerializationWriter).Assembly.GetName().Version;
            this.Write(assembly.Major);
            this.Write(assembly.Minor);
            this.Write(assembly.Build);
            this.Write(assembly.Revision);
        }

       

        private void WriteSerializerOptions(SerializerOptions serializerOptions)
        {
            Write(serializerOptions.CodePage);
            Write(serializerOptions.Threshold);
            var typeName = serializerOptions.CompressorType.AssemblyQualifiedName;            
            Write(Encoding.Unicode.GetBytes(typeName));
        }

        

        private static SerializerOptions GetSerializationOptions()
        {
            return GetSettings();            
        }

        private static SerializerOptions GetSettings()
        {
            return (SerializerOptions)ConfigurationManager.GetSection("binarySerializer") ?? new SerializerOptions();            
        }


        /// <summary>
        /// Writes a <see cref="ushort"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="ushort"/> value to write.</param>
        public void Write(ushort value)
        {
            byte bufferIndex = 0;
            
            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (value >= 0x80)
            {             
                buffer[bufferIndex] = (byte)(value | 0x80);
                value >>= 7;
                bufferIndex++;
            }

            buffer[bufferIndex] = (byte)value;
            stream.Write(buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="ushort"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="ushort"/> value to write.</param>
        public void Write(ushort? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes a <see cref="short"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="short"/> value to write.</param>
        public void Write(short value)
        {            
            byte bufferIndex = 0;

            // Same as Math.Abs(value), only faster
            var absoluteValue = (short)((value ^ (value >> 15)) - (value >> 15));

            // Get the first five bits from the first byte
            var firstByte = (byte)(absoluteValue & 0x1f);

            if (value < 0)
            {
                if (value == short.MinValue)
                {
                    firstByte = (byte)(firstByte | 0x3F);
                    this.Write(firstByte);
                    return;
                }

                // Use the seventh bit to indicate a negative number
                firstByte = (byte)(firstByte | 0x40);
            }

            if (absoluteValue >= 0x20)
            {
                // Use the eight bit to indicate that there is a byte following this one when reading.
                firstByte = (byte)(firstByte | 0x80);
            }

            this.buffer[bufferIndex] = firstByte;

            // Skip the first six bits are they are now already processed.
            absoluteValue >>= 5;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (absoluteValue >= 0x80)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)(absoluteValue | 0x80);
                absoluteValue >>= 7;
            }

            // write the last byte to the buffer          
            if (absoluteValue > 0)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)absoluteValue;
            }

            this.stream.Write(this.buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="short"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="short"/> value to write.</param>
        public void Write(short? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes a <see cref="uint"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="uint"/> value to write.</param>
        public void Write(uint value)
        {
            byte bufferIndex = 0;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (value >= 0x80)
            {
                buffer[bufferIndex] = (byte)(value | 0x80);
                value >>= 7;
                bufferIndex++;
            }

            buffer[bufferIndex] = (byte)value;
            stream.Write(buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="uint"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="uint"/> value to write.</param>
        public void Write(uint? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes a <see cref="int"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to write.</param>
        public void Write(int value)
        {
            byte bufferIndex = 0;

            // Same as Math.Abs(value), only faster
            int absoluteValue = (value ^ (value >> 31)) - (value >> 31);

            // Get the first five bits from the first byte
            var firstByte = (byte)(absoluteValue & 0x1f);

            if (value < 0)
            {
                if (value == int.MinValue)
                {
                    firstByte = (byte)(firstByte | 0x3F);
                    this.Write(firstByte);
                    return;
                }

                // Use the seventh bit to indicate a negative number
                firstByte = (byte)(firstByte | 0x40);
            }

            if (absoluteValue >= 0x20)
            {
                // Use the eight bit to indicate that there is a byte following this one when reading.
                firstByte = (byte)(firstByte | 0x80);
            }

            this.buffer[bufferIndex] = firstByte;

            // Skip the first six bits are they are now already processed.
            absoluteValue >>= 5;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (absoluteValue >= 0x80)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)(absoluteValue | 0x80);
                absoluteValue >>= 7;
            }

            // write the last byte to the buffer          
            if (absoluteValue > 0)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)absoluteValue;
            }

            this.stream.Write(this.buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="int"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="int"/> value to write.</param>
        public void Write(int? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes a <see cref="ulong"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value to write.</param>
        public void Write(ulong value)
        {
            byte bufferIndex = 0;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (value >= 0x80)
            {
                buffer[bufferIndex] = (byte)(value | 0x80);
                value >>= 7;
                bufferIndex++;
            }

            buffer[bufferIndex] = (byte)value;
            stream.Write(buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="ulong"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="ulong"/> value to write.</param>
        public void Write(ulong? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes a <see cref="long"/> value to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value to write.</param>
        public void Write(long value)
        {
            byte bufferIndex = 0;

            // Same as Math.Abs(value), only faster
            long absoluteValue = (value ^ (value >> 63)) - (value >> 63);

            // Get the first five bits from the first byte
            var firstByte = (byte)(absoluteValue & 0x1f);

            if (value < 0)
            {
                if (value == long.MinValue)
                {
                    firstByte = (byte)(firstByte | 0x3F);
                    this.Write(firstByte);
                    return;
                }

                // Use the seventh bit to indicate a negative number
                firstByte = (byte)(firstByte | 0x40);
            }

            if (absoluteValue >= 0x20)
            {
                // Use the eight bit to indicate that there is a byte following this one when reading.
                firstByte = (byte)(firstByte | 0x80);
            }

            this.buffer[bufferIndex] = firstByte;

            // Skip the first six bits are they are now already processed.
            absoluteValue >>= 5;

            // Continue to write the remaining bytes until the value can fit within 7 bytes.
            while (absoluteValue >= 0x80)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)(absoluteValue | 0x80);
                absoluteValue >>= 7;
            }

            // write the last byte to the buffer          
            if (absoluteValue > 0)
            {
                bufferIndex++;
                this.buffer[bufferIndex] = (byte)absoluteValue;
            }

            this.stream.Write(this.buffer, 0, bufferIndex + 1);
        }

        /// <summary>
        /// Writes a nullable <see cref="long"/> value to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="long"/> value to write.</param>
        public void Write(long? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes an <see cref="byte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to write.</param>
        public void Write(byte value)
        {
            this.WriteByte(value);
        }

        /// <summary>
        /// Writes an <see cref="byte"/> array to the current stream.
        /// </summary>
        /// <param name="bytes">The <see cref="byte"/> array to write.</param>
        public void Write(byte[] bytes)
        {            
            if (bytes == null)
            {
                // Set the first bit to indicate that the value is null.
                Write((ulong)0x1);
            }
            else
            {
                int length = bytes.Length;
                ulong metadata = (ulong)length << 1;
                Write(metadata);
                stream.Write(bytes, 0, length);
            }
        }

        private void WriteByte(byte value)
        {
            this.stream.WriteByte(value);
        }

        /// <summary>
        /// Writes a nullable <see cref="byte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to write.</param>
        public void Write(byte? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes an <see cref="sbyte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="sbyte"/> value to write.</param>
        public void Write(sbyte value)
        {
            stream.WriteByte((byte)value);
        }

        /// <summary>
        /// Writes a nullable <see cref="sbyte"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="sbyte"/> value to write.</param>
        public void Write(sbyte? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }
        }

        /// <summary>
        /// Writes an <see cref="decimal"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="decimal"/> value to write.</param>
        public void Write(decimal value)
        {
            var bits = decimal.GetBits(value);            
            Write(bits[0]);
            Write(bits[1]);
            Write(bits[2]);
            Write(bits[3]);
        }

        /// <summary>
        /// Writes a nullable <see cref="decimal"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="decimal"/> value to write.</param>
        public void Write(decimal? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }            
        }

        /// <summary>
        /// Writes an <see cref="float"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to write.</param>
        public unsafe void Write(float value)
        {
            uint num = *((uint*)&value);
            buffer[0] = (byte)num;
            buffer[1] = (byte)(num >> 8);
            buffer[2] = (byte)(num >> 0x10);
            buffer[3] = (byte)(num >> 0x18);
            stream.Write(buffer, 0, 4);
        }

        /// <summary>
        /// Writes a nullable <see cref="float"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="float"/> value to write.</param>
        public void Write(float? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }            
        }

        /// <summary>
        /// Writes an <see cref="double"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to write.</param>
        public unsafe void Write(double value)
        {                                    
            ulong num = *((ulong*)&value);           
            this.buffer[0] = (byte)num;
            this.buffer[1] = (byte)(num >> 8);
            this.buffer[2] = (byte)(num >> 0x10);
            this.buffer[3] = (byte)(num >> 0x18);
            this.buffer[4] = (byte)(num >> 0x20);
            this.buffer[5] = (byte)(num >> 40);
            this.buffer[6] = (byte)(num >> 0x30);
            this.buffer[7] = (byte)(num >> 0x38);                       
            this.stream.Write(this.buffer, 0, 8);
        }

        /// <summary>
        /// Writes a nullable <see cref="double"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="double"/> value to write.</param>
        public void Write(double? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }            
        }

        /// <summary>
        /// Writes an <see cref="Guid"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="Guid"/> value to write.</param>
        public void Write(Guid value)
        {
            this.stream.Write(value.ToByteArray(), 0, 0x10);
        }

        /// <summary>
        /// Writes a nullable <see cref="Guid"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="Guid"/> value to write.</param>
        public void Write(Guid? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }            
        }

        /// <summary>
        /// Writes an <see cref="DateTime"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        public void Write(DateTime value)
        {
            bool hasTime = value.TimeOfDay.Ticks > 0;
            var packedDateTime = new BitVector32();
            packedDateTime[DateTimeBitVectorSections.Year] = value.Year;
            packedDateTime[DateTimeBitVectorSections.Month] = value.Month;
            packedDateTime[DateTimeBitVectorSections.Day] = value.Day;
            packedDateTime[DateTimeBitVectorSections.HasTime] = hasTime ? 1 : 0;
            Write(packedDateTime.Data);
            if (hasTime)
            {
                var packedTime = new BitVector32();
                packedTime[DateTimeBitVectorSections.Hour] = value.Hour;
                packedTime[DateTimeBitVectorSections.Minute] = value.Minute;
                packedTime[DateTimeBitVectorSections.Second] = value.Second;
                packedTime[DateTimeBitVectorSections.MilliSecond] = value.Millisecond;
                Write(packedTime.Data);
            }
        }

        /// <summary>
        /// Writes a nullable <see cref="DateTime"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="DateTime"/> value to write.</param>
        public void Write(DateTime? value)
        {
            if (value == null)
            {
                Write(true);
            }
            else
            {
                Write(false);
                Write(value.Value);
            }            
        }

        /// <summary>
        /// Writes a <see cref="bool"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to write.</param>
        public void Write(bool value)
        {
            Write(value ? ((byte)1) : ((byte)0));
        }

        /// <summary>
        /// Writes a nullable <see cref="bool"/> to the current stream.
        /// </summary>
        /// <param name="value">The nullable <see cref="bool"/> value to write.</param>
        public void Write(bool? value)
        {
            byte byteToWrite = 0;
            if (value == null)
            {
                // Use the second bit to indicate that the value is null;
                byteToWrite = (byte)(byteToWrite | 0x2);
            }
            else
            {
                byteToWrite = value.Value ? ((byte)1) : ((byte)0);
            }
            
            Write(byteToWrite);
        }

        /// <summary>
        /// Writes a <see cref="string"/> to the current stream.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to write.</param>
        public void Write(string value)
        {            
            if (value == null)
            {                
                //Write a zero token to indicate that the value is null;
                Write((ulong)0);
            }
            else
            {
                ulong token;
                if (stringCache.TryGetValue(value, out token))
                {
                    Write(token);
                }
                else
                {
                    token = (ulong)stringCache.Count + 1;
                    token = token << 1;
                    
                    byte[] bytes = encoding.GetBytes(value);
                    int length = bytes.Length; 
                    if (length >= options.Threshold)
                    {
                        bytes = compressor.Compress(bytes);
                        // Use the first bit to indicate that the value is compressed.
                        token = (byte)(token | 0x1);
                        Write(token);
                        Write((uint)length);
                        Write(bytes); 
                    }
                    else
                    {
                        Write(token);
                        Write(bytes); 
                    }                   
                    stringCache.Add(value, token);
                }                
            }            
        }
             
        internal void WriteCollectionInternal<T>(ICollection<T> collection)
        {
            ulong token;
            if (cache.TryGetValue(collection, out token))
            {
                this.Write(token);
            }
            else
            {                             
                Write(cache.Add(collection));                
                Write((uint)collection.Count);                                
                foreach (var value in collection)
                {
                    Write(value);
                }
            }
        }
      
        internal void WriteSerializableObject(object value)
        {
            ulong token;
            if (objectCache.TryGetValue(value, out token))
            {
                Write(token);
            }
            else
            {
                token = (ulong)objectCache.Count + 1;
                objectCache.Add(value, token);
                Write(token);
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, value);
            }            
        }
       



        private Action<BinarySerializationWriter,object> GetWriteMethod(Type type)
        {
            return WriterMethods.GetOrAdd(type, t => ResolveWriteMethod(t));

        }

        private Action<BinarySerializationWriter, object> ResolveWriteMethod(Type type)
        {
            Type collectionType = type.GetCollectionType();
            if (collectionType != null)
            {
                
            }

            return (writer, value) => writer.WriteSerializableObject(value);
        }

        private Action<object> GetObjectWriteMethod(Type type)
        {
            if (type == typeof(byte))
            {
                return value => WriteByte((byte)value);                               
            }

            return this.WriteSerializableObject;
        }

        public void Write<T>(T value) 
        {            
            if (value == null)
            {
                Write(TypeCodes.Null);
            }
            else
            {
                Type type = typeof(T);
                if (!type.IsSealed)
                {
                    type = value.GetType();
                }                
                Write(type);
                CreateWriteMethod<T>(type)(this, value);
            }               
        }

        private Action<BinarySerializationWriter, T> CreateWriteMethod<T>(Type actualType)
        {
            return WriteMethodFactory.CreateWriteMethod<T>(actualType);                     
        }

        

        internal void WriteBinarySerializableObject<T>(T value) where T : IBinarySerializable, new()
        {            
            if (value == null)
            {
                Write((ulong)0);
            }
            else
            {
                ulong token;
                if (cache.TryGetValue(value, out token))
                {
                    Write(token);
                }
                else
                {                                     
                    Write(cache.Add(value));                   
                    value.Serialize(this);                    
                }           
            }
        }

        


        public void Write(Type type)
        {
            if (type == null)
            {
                Write(TypeCodes.Null);
            }
            else
            {
                byte typeCode = TypeCodes.GetTypeCode(type);
                Write(typeCode);
                if (typeCode >= TypeCodes.Array)
                {
                    Write(type.AssemblyQualifiedName);
                }                
            }            
            
        }

        public void Write<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            throw new NotImplementedException();
        }
              
    }
}