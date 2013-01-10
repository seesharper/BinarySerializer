namespace BinarySerializer
{
    using System;
    using System.Collections.Generic;
       
    public static class PerThreadCache<TKey, TValue>
    {
        [ThreadStatic]
        private static Dictionary<TKey, TValue> cache;

        private static Dictionary<TKey, TValue> GetCache()
        {
            if (cache == null)
            {
                cache = new Dictionary<TKey, TValue>();
            }
            return cache;
        }

        public static bool TryGetValue(TKey key, out TValue value)
        {            
            return GetCache().TryGetValue(key, out value);
        }

        public static void Add(TKey key, TValue value)
        {
            GetCache().Add(key, value);
        }

        public static void Clear()
        {
            GetCache().Clear();            
        }


        public static int Count
        {
            get
            {
                return GetCache().Count;
            }
        }
    }
}