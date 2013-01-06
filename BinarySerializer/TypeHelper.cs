namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;

    public static class TypeHelper
    {
        private static readonly ConcurrentDictionary<string,Type> Types = new ConcurrentDictionary<string, Type>(); 

        public static Type GetType(string assemblyQualifiedName)
        {
            return Types.GetOrAdd(assemblyQualifiedName, s => ResolveType(s));
        }

        private static Type ResolveType(string s)
        {
            return Type.GetType(s);
        }
    }
}