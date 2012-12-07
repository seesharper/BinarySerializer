namespace BinarySerializer.Tests
{
    using System;
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
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




        private unsafe decimal CalculatePerformanceGain(long orginalValue, long newvalue)
        {
            return (((decimal)(newvalue - orginalValue)) / orginalValue) * 100;
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
    }

    
   
}