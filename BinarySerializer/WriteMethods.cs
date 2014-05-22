﻿namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class WriteMethods
    {
        private static ConcurrentDictionary<Type,MethodInfo> Methods = new ConcurrentDictionary<Type, MethodInfo>();

        private static readonly MethodInfo OpenGenericWriteBinarySerializableObjectMethod;
        private static readonly MethodInfo OpenGenericWriteSerializableObjectMethod;
        private static readonly MethodInfo OpenGenericWriteCollectionMethod;

        static WriteMethods()
        {
            OpenGenericWriteBinarySerializableObjectMethod = typeof(BinarySerializationWriter).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(m => m.IsGenericMethod && m.Name == "WriteBinarySerializableObject");
            OpenGenericWriteSerializableObjectMethod = typeof(BinarySerializationWriter).GetMethod(
                "WriteSerializableObject", BindingFlags.NonPublic | BindingFlags.Instance);
            OpenGenericWriteCollectionMethod = typeof(BinarySerializationWriter).GetMethod("WriteCollectionInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        public static MethodInfo GetWriteMethod(Type actualType)
        {
            return Methods.GetOrAdd(actualType, ResolveWriteMethod);
        }

        private static MethodInfo ResolveWriteMethod(Type actualType)
        {
            if (actualType.IsNullable())
            {
                actualType = Nullable.GetUnderlyingType(actualType);
            }
            
            MethodInfo method = typeof(BinarySerializationWriter).GetMethod("Write", new[] { actualType });
            if (method != null)
            {
                return method;
            }

            if (typeof(IBinarySerializable).IsAssignableFrom(actualType))
            {
                MethodInfo closedGenericWriteMethod = OpenGenericWriteBinarySerializableObjectMethod.MakeGenericMethod(actualType);
                return closedGenericWriteMethod;
            }

            Type collectionType = actualType.GetCollectionType();
            if (collectionType != null)
            {
                Type elementType = collectionType.GetGenericArguments().First();
                return OpenGenericWriteCollectionMethod.MakeGenericMethod(elementType);
            }

            if (actualType.IsEnum)
            {
                return ResolveWriteMethod(Enum.GetUnderlyingType(actualType));
            }

            var underlyingType = Nullable.GetUnderlyingType(actualType);
            if (underlyingType != null)
            {
                if (underlyingType.IsEnum)
                {
                    underlyingType = Enum.GetUnderlyingType(underlyingType);
                }

                var nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(underlyingType);
                return ResolveWriteMethod(nullableUnderlyingType);
            }




            MethodInfo closedGenericWriteSerializableObjectMethod = OpenGenericWriteSerializableObjectMethod.MakeGenericMethod(actualType);
            return closedGenericWriteSerializableObjectMethod;
        }

        private static MethodInfo ResolveWriteMethod_old(Type actualType)
        {
            MethodInfo method = typeof(BinarySerializationWriter).GetMethod("Write", new[] { actualType });
            if (method != null)
            {
                return method;
            }
            
            if (typeof(IBinarySerializable).IsAssignableFrom(actualType))
            {
                MethodInfo closedGenericWriteMethod = OpenGenericWriteBinarySerializableObjectMethod.MakeGenericMethod(actualType);
                return closedGenericWriteMethod;
            }

            Type collectionType = actualType.GetCollectionType();
            if (collectionType != null)
            {
                Type elementType = collectionType.GetGenericArguments().First();
                return OpenGenericWriteCollectionMethod.MakeGenericMethod(elementType);
            }
           
            if (actualType.IsEnum)
            {                
                return ResolveWriteMethod(Enum.GetUnderlyingType(actualType));
            }

            var underlyingType = Nullable.GetUnderlyingType(actualType);
            if (underlyingType != null)
            {
                if (underlyingType.IsEnum)
                {
                    underlyingType = Enum.GetUnderlyingType(underlyingType);
                }
                
                var nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(underlyingType);
                return ResolveWriteMethod(nullableUnderlyingType);
            }
            
            


            MethodInfo closedGenericWriteSerializableObjectMethod = OpenGenericWriteSerializableObjectMethod.MakeGenericMethod(actualType);
            return closedGenericWriteSerializableObjectMethod;
        }


    }
}