using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySerializer
{
    internal class KeyValueStorage<TKey, TValue>
    {
        private readonly object lockObject = new object();
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                lock (lockObject)
                {
                    value = TryAddValue(key, valueFactory);
                }
            }

            return value;
        }

        private TValue TryAddValue(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                var snapshot = new Dictionary<TKey, TValue>(dictionary);
                value = valueFactory(key);
                snapshot.Add(key, value);
                dictionary = snapshot;
            }

            return value;
        }
    }
}
