namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;

    public static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Type, Type> Cache = new ConcurrentDictionary<Type, Type>();
    
        public static Type GetUnderlyingEnumType(Type type)
        {
            return Cache.GetOrAdd(type, ResolveUnderlyingEnumType);
        }

       


















        private static Type ResolveUnderlyingEnumType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }


        

    }
}