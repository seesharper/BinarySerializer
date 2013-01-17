namespace BinarySerializer
{
    using System;
    using System.Reflection.Emit;

    public class WriteMethodSkeleton : IWriteMethodSkeleton
    {
        private DynamicMethod dynamicMethod;
        
        public ILGenerator GetILGenerator<T>()
        {
            dynamicMethod = new DynamicMethod("DynamicMethod", typeof(void), new Type[] { typeof(BinarySerializationWriter), typeof(T) }, typeof(BinarySerializationWriter).Module);
            return dynamicMethod.GetILGenerator();
        }

        public Action<BinarySerializationWriter, T> CreateDelegate<T>()
        {
            return (Action<BinarySerializationWriter, T>)dynamicMethod.CreateDelegate(typeof(Action<BinarySerializationWriter, T>));
        }
    }
}