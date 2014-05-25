namespace BinarySerializer
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    
    public static class WriteMethods<T>
    {
        private static readonly Lazy<Action<IWriter, T>> Cache = new Lazy<Action<IWriter, T>>(CreateWriteMethod);

        public static Action<IWriter, T> Get()
        {
            return Cache.Value;
        }

        private static Action<IWriter, T> CreateWriteMethod()
        {
            var dynamicMethod = new DynamicMethod("DynamicWriteMethod", typeof(void), new Type[] { typeof(IWriter), typeof(T) }, typeof(Writer).Module);

            Type actualType = typeof(T);

            if (actualType.IsEnum)
            {
                actualType = EnumHelper.GetUnderlyingEnumType(typeof(T));
            }

            MethodInfo method;
            if (actualType.IsNullable())
            {
                method = typeof(ISerializer).GetMethod("Write", new[] { actualType });
            }
            else
            {
                method = typeof(IWriter).GetMethod("Write", new[] { actualType });    
            }

            

            if (method == null)
            {
                method = typeof(ISerializer).GetMethod("Write", new[] { actualType });
            }
            

            var il = dynamicMethod.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);            
            il.Emit(OpCodes.Callvirt, method);
            il.Emit(OpCodes.Ret);

            return (Action<IWriter, T>)dynamicMethod.CreateDelegate(typeof(Action<IWriter, T>));
        }
    }
}