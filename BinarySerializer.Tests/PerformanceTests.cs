namespace BinarySerializer.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, Ignore]
    public class PerformanceTests
    {
        [TestMethod] 
        public unsafe void AbsoluteValue()
        {
            Console.WriteLine("Math.Abs vs bit shift ");            
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var value = Math.Abs(-1);                
            }
            stopwatch.Stop();
            long mathAbsDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {                
                var value = (-1 ^ (-1 >> 31)) - (-1 >> 31);
            }
            stopwatch.Stop();            
            long bitShiftDuration = stopwatch.ElapsedMilliseconds; 

            Console.WriteLine("Using Math.Abs: {0}", mathAbsDuration);
            Console.WriteLine("Using bit shift: {0}", bitShiftDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(bitShiftDuration, mathAbsDuration));            
        }

        private unsafe decimal CalculatePerformanceGain(long orginalValue, long newvalue)
        {
            return (((decimal)(newvalue - orginalValue)) / orginalValue) * 100;
        }

        [TestMethod]
        public void Negation()
        {
            var postiveValue = 1;
            Console.WriteLine("Standard negation vs bitwise OR ");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var value = -postiveValue;
            }
            stopwatch.Stop();
            long stardardNegationDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var value = (postiveValue ^ -1) + 1;
            }
            stopwatch.Stop();
            long bitShiftDuration = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("Using Standard Negation: {0}", stardardNegationDuration);
            Console.WriteLine("Using XOR: {0}", bitShiftDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(bitShiftDuration, stardardNegationDuration)); 
        }

        [TestMethod]
        public unsafe void DoubleGetBytes()
        {
            Console.WriteLine("Bitconverter vs bit shift");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double value = 42;            
            var buffer = new byte[0x8];
            for (int i = 0; i < iterations; i++)
            {
                buffer = BitConverter.GetBytes(value);
            }
            stopwatch.Stop();
            long bitconverterDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                ulong num = *((ulong*)&value);
                buffer[0] = (byte)num;
                buffer[1] = (byte)(num >> 8);
                buffer[2] = (byte)(num >> 0x10);
                buffer[3] = (byte)(num >> 0x18);
                buffer[4] = (byte)(num >> 0x20);
                buffer[5] = (byte)(num >> 40);
                buffer[6] = (byte)(num >> 0x30);
                buffer[7] = (byte)(num >> 0x38);
            }
            stopwatch.Stop();
            long bitShiftDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Using BitConverter: {0}", bitconverterDuration);
            Console.WriteLine("Using Bit shift: {0}", bitShiftDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(bitShiftDuration, bitconverterDuration)); 
        }

        [TestMethod]
        public void WriteToStream()
        {
            var memoryStream = new MemoryStream();
            var buffer = new byte[0x10];
            buffer[0x0] = 42;
            buffer[0x1] = 42;
            buffer[0x2] = 42;
            buffer[0x3] = 42;
            buffer[0x4] = 42;
            buffer[0x5] = 42; 
            buffer[0x6] = 42;
            buffer[0x7] = 42;
            buffer[0x8] = 42;
            buffer[0x9] = 42;
            buffer[0xa] = 42;
            buffer[0xb] = 42;
            buffer[0xc] = 42;
            buffer[0xd] = 42;
            buffer[0xe] = 42;
            buffer[0xf] = 42;

            Console.WriteLine("Single write vs batch write");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                memoryStream.WriteByte(42);
            }
            stopwatch.Stop();
            long writeDuration = stopwatch.ElapsedMilliseconds;
            memoryStream = new MemoryStream();
            stopwatch.Reset();
            stopwatch.Start();
            
            for (int i = 0; i < 62500; i++)
            {
                memoryStream.Write(buffer,0,16);
            }
            stopwatch.Stop();
            long bufferedWriteDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Using single write: {0}", writeDuration);
            Console.WriteLine("Using buffered write: {0}", bufferedWriteDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(bufferedWriteDuration, writeDuration)); 
        }


       

        private unsafe void BitShift(int value)
        {
            var intPointer = &value;
            int test = *intPointer >> 6;
        }

        [TestMethod]
        public void Experimental()
        {
            int value = int.MinValue;
            //int absoluteValue = (value ^ (value >> 31)) - (value >> 31);
            int absoluteValue = Math.Abs(value);
        }

        [TestMethod]
        public void DeflateStreamVsNoCompression()
        {            
            byte[] bytesToCompress = Encoding.Default.GetBytes(Text.LoremIpsum);
            Console.WriteLine("Enum.ToObject vs DynamicMethod");
            MemoryStream targetStream = new MemoryStream();
            DeflateStream deflateStream = new DeflateStream(targetStream, CompressionMode.Compress, true);
            int iterations = 1000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {                                
                deflateStream.Write(bytesToCompress, 0, bytesToCompress.Length);
                targetStream.WriteByte(0);
            }

            stopwatch.Stop();
            deflateStream.Close();
            deflateStream.Dispose();
            long compressDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                
                targetStream.Write(bytesToCompress,0,bytesToCompress.Length);
            }
            stopwatch.Stop();
            long noCompressDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Using compression: {0}", compressDuration);
            Console.WriteLine("Using no compression: {0}", noCompressDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(compressDuration,compressDuration)); 
        }

        [TestMethod]
        public void EnumValues()
        {
            var dynamicFunc = this.CreateEnumFunc();
            StringSplitOptions splitOptions;    
            Console.WriteLine("Enum.ToObject vs DynamicMethod");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                splitOptions = (StringSplitOptions)Enum.ToObject(typeof(StringSplitOptions), 1);
            }
            stopwatch.Stop();
            long toObjectDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                splitOptions = ((Func<PerformanceTests, StringSplitOptions>)dynamicFunc)(this);
            }
            stopwatch.Stop();
            long dynamicMethodDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Using ToObject: {0}", toObjectDuration);
            Console.WriteLine("Using DynamicMethod: {0}", dynamicMethodDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(dynamicMethodDuration, toObjectDuration)); 

        }

        public int ReadInt32()
        {            
            return 1;
        }


        private Delegate CreateEnumFunc()
        {
            DynamicMethod dynamicMethod = new DynamicMethod("EnumReadMethod", typeof(StringSplitOptions), new[] { typeof(PerformanceTests) }, typeof(PerformanceTests).Module);
            var generator = dynamicMethod.GetILGenerator();
            var localBuilder = generator.DeclareLocal(typeof(StringSplitOptions));
            
            generator.Emit(OpCodes.Ldarg_0);            
            var methodInfo = typeof(PerformanceTests).GetMethod("ReadInt32");
            generator.Emit(OpCodes.Callvirt, methodInfo);
            generator.Emit(OpCodes.Stloc, localBuilder);
            generator.Emit(OpCodes.Ldloc, localBuilder);
            generator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(typeof(Func<PerformanceTests, StringSplitOptions>));
        }


        [TestMethod]
        public void GetEnumUnderlyingType()
        {
            Console.WriteLine("Enum.GetUnderlyingType vs cached type ");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                Enum.GetUnderlyingType(typeof(StringSplitOptions));
            }
            stopwatch.Stop();
            long getUnderlyingTypeDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                EnumHelper.GetUnderLyingEnumType(typeof(StringSplitOptions));
            }
            stopwatch.Stop();
            long cachedUnderlyingTypeDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Enum.GetUnderlyingType: {0}", getUnderlyingTypeDuration);
            Console.WriteLine("Using Cache: {0}", cachedUnderlyingTypeDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(cachedUnderlyingTypeDuration, getUnderlyingTypeDuration));
        }

        [TestMethod]
        public void GetTypeInfo()
        {
            string value = "TEST";
            Console.WriteLine("GetType vs as");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                this.GetTypeMethod(value);
            }
            stopwatch.Stop();
            long getTypeDuration = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                
                this.TypeOfMethod<string>();
            }
            stopwatch.Stop();
            long asDuration = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("GetType: {0}", getTypeDuration);
            Console.WriteLine("Using as: {0}", asDuration);
            Console.WriteLine("Performance gain: {0:0.00}%", this.CalculatePerformanceGain(asDuration, getTypeDuration));
        }

        private void GetTypeMethod(object value)
        {
            var type = value.GetType();
            if (type == (typeof(int)))
            {
                Console.WriteLine("");
            }
        }

        private void TypeOfMethod<T>()
        {
            var type = typeof(T);
            if (type == (typeof(int)))
            {
                Console.WriteLine("");
            }
        }

        [TestMethod]
        public void DeflateCompression()
        {
            Console.WriteLine("Deflate stream vs no compression");
            int iterations = 10000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string stringToWrite = Text.LoremIpsum;
            for (int i = 0; i < iterations; i++)
            {
               //DeflateStream deflateStream = new DeflateStream();
            }
        }

    }

    
   
}