namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ObjectReader<T> where T : class
    {
        private readonly IBinarySerializationReader reader;

        private static Func<IBinarySerializationReader, T> readMethod;
        
        private static Dictionary<string, Type> types = new Dictionary<string, Type>(); 

        private Dictionary<int, T> cache = new Dictionary<int, T>();

        public ObjectReader(IBinarySerializationReader reader)
        {
            this.reader = reader;
        }

        static ObjectReader()
        {
            readMethod = CreateReadMethod();
        }

        private static Func<IBinarySerializationReader, T> CreateReadMethod()
        {
            throw new NotImplementedException();
        }

        public T Read()
        {
            int token = reader.ReadInt32();
            if (token == 0)
            {
                return null;
            }

            T value;
            if (!cache.TryGetValue(token, out value))
            {
                string assemblyQualifiedTypename = reader.ReadString();
                var type = GetType(assemblyQualifiedTypename);
                value = Activate(type);
                cache.Add(token, value);
            }

            return value;
        }

        private T Activate(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        private static Type GetType(string typeName)
        {
            Type type;
            if (!types.TryGetValue(typeName, out type))
            {
                type = Type.GetType(typeName, true);
                types.Add(typeName, type);
            }
            return type;
        }
    }
}