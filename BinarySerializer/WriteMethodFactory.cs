namespace BinarySerializer
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public class WriteMethodFactory : IWriteMethodFactory
    {
        private readonly IWriteMethodSkeleton writeMethodSkeleton;

        public WriteMethodFactory(IWriteMethodSkeleton writeMethodSkeleton)
        {
            this.writeMethodSkeleton = writeMethodSkeleton;
        }

        public Action<BinarySerializationWriter, T> CreateWriteMethod<T>(Type actualType)
        {            
            
            
            ILGenerator il = writeMethodSkeleton.GetILGenerator<T>();
            MethodInfo methodInfo = WriteMethods.GetWriteMethod(actualType);
            il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Ldarg_1);
            
            if (actualType.IsNullable())
            {
                il.Emit(OpCodes.Ldarga_S, (byte)1);
                MethodInfo getValueMethod = actualType.GetProperty("Value").GetGetMethod();
                il.Emit(OpCodes.Call, getValueMethod);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_1);
            }



            if (!typeof(T).IsValueType && actualType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, actualType);
            }

            if (!typeof(Type).IsAssignableFrom(typeof(T)) && typeof(Type).IsAssignableFrom(actualType))
            {
                il.Emit(OpCodes.Castclass, typeof(Type));
            }

            
            //TODO Abstract this to enable PEVerify. It is strange that we don't we need OpCodes.CastClass            
            il.Emit(OpCodes.Call, methodInfo);
            il.Emit(OpCodes.Ret);
            return writeMethodSkeleton.CreateDelegate<T>();
        }
    }
}