using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySerializer
{
    public static class Cache<TKey,TValue>
    {
        private static readonly KeyValueStorage<TKey, TValue> CachedValues = new KeyValueStorage<TKey, TValue>(); 
        
        public static TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return CachedValues.GetOrAdd(key, valueFactory);
        }
    }
}
