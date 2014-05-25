namespace BinarySerializer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class ReadMethods<T>
    {
        private static readonly Lazy<Func<IReader, T>> Cache = new Lazy<Func<IReader, T>>(CreateReadMethod);

        public static Func<IReader, T> Get()
        {
            return Cache.Value;
        }

        private static Func<IReader, T> CreateReadMethod()
        {
            Type actualType = typeof(T);

            if (actualType.IsEnum)
            {
                actualType = EnumHelper.GetUnderlyingEnumType(actualType);
            }


            MethodInfo methodInfo;

            if (actualType.IsNullable())
            {
                methodInfo = typeof(IDeserializer).GetMethods().FirstOrDefault(m => m.ReturnType == actualType);
            }
            else
            {
                methodInfo = typeof(IReader).GetMethods().FirstOrDefault(m => m.ReturnType == actualType);
            }


            var dynamicMethod = new DynamicMethod("DynamicReadMethod", typeof(T), new[] { typeof(IReader) }, typeof(IReader).Module);
            

            var il = dynamicMethod.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);            
            il.Emit(OpCodes.Call, methodInfo);            
            il.Emit(OpCodes.Ret);

            return (Func<IReader, T>)dynamicMethod.CreateDelegate(typeof(Func<IReader, T>));
        }
    }
}