namespace BinarySerializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if a given type implements the <see cref="ICollection{T}"/> interface.
        /// </summary>
        /// <param name="type">The target <see cref="Type"/>.</param>
        /// <returns><b>true</b> if the <paramref name="type"/> implements <see cref="ICollection{T}"/>, otherwise <b>false</b></returns>       
        public static bool IsCollectionType(this Type type)
        {
            return IsCollectionOfT(type) || (!type.IsArray && type.GetInterfaces().Any(IsCollectionOfT));
        }

        private static bool IsCollectionOfT(Type @interface)
        {
            return @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(ICollection<>);
        } 

        public static Type GetCollectionType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                return type;
            }
            var interfaces = type.GetInterfaces();
            foreach (var interfaceType in interfaces)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    return interfaceType;
                }
            }

            return null;
        }

    }
}