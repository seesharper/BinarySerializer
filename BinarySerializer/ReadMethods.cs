namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;    
    using System.Linq;
    using System.Reflection;

    public static class ReadMethods
    {
        private static readonly MethodInfo OpenGenericReadBinarySerializableObjectMethod;

        private static readonly MethodInfo OpenGenericReadCollectionMethod;


        private static readonly ConcurrentDictionary<Type, MethodInfo> Methods = new ConcurrentDictionary<Type, MethodInfo>();

        static ReadMethods()
        {
            OpenGenericReadBinarySerializableObjectMethod = typeof(BinarySerializationReader).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(m => m.IsGenericMethod && m.Name == "ReadBinarySerializableObject");
            OpenGenericReadCollectionMethod = typeof(BinarySerializationReader).GetMethod(
                "ReadCollectionInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public static MethodInfo GetReadMethod(Type type)
        {
            return Methods.GetOrAdd(type, ResolveReadMethod);
        }

        private static MethodInfo ResolveReadMethod(Type type)
        {
            MethodInfo methodInfo = typeof(BinarySerializationReader).GetMethods().FirstOrDefault(m => m.ReturnType == type);
            if (methodInfo != null)
            {
                return methodInfo;
            }

            if (typeof(IBinarySerializable).IsAssignableFrom(type))
            {
                MethodInfo closedGenericWriteMethod = OpenGenericReadBinarySerializableObjectMethod.MakeGenericMethod(type);
                return closedGenericWriteMethod;
            }

            Type collectionType = type.GetCollectionType();
            if (collectionType != null)
            {
                Type elementType = collectionType.GetGenericArguments().First();
                return OpenGenericReadCollectionMethod.MakeGenericMethod(type, elementType);
            }

            throw new NotSupportedException();
        }
    }
}