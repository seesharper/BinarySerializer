namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;

    public static class EnumHelper
    {
        private static ConcurrentDictionary<Type,Type> cache = new ConcurrentDictionary<Type, Type>();
    
        public static Type GetUnderLyingEnumType(Type type)
        {
            return cache.GetOrAdd(type, ResolveUnderlyingEnumType);
        }

        private static Type ResolveUnderlyingEnumType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }
    }
}