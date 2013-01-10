namespace BinarySerializer
{
    using System;
    using System.Collections.Generic;

    public class ReadCache : IDisposable
    {
        [ThreadStatic]
        private static Dictionary<Type, Action> InvalidateActions;
        
        public ReadCache()
        {
            InvalidateCaches();
        }

        private Dictionary<Type, Action> GetInvalidateActions()
        {
            if (InvalidateActions == null)
            {
                InvalidateActions = new Dictionary<Type, Action>();
            }
            return InvalidateActions;
        }
        
        
        public bool TryGetValue<TValue>(ulong key, out TValue value)
        {
            RegisterInvalidateAction<TValue>();
            return PerThreadCache<ulong, TValue>.TryGetValue(key, out value);
        }

        public void Add<TKey,TValue>(TKey key, TValue value)
        {
            RegisterInvalidateAction<TValue>();
            PerThreadCache<TKey, TValue>.Add(key, value);
        }

        private void RegisterInvalidateAction<TValue>()
        {
            var actions = GetInvalidateActions();
            if (!actions.ContainsKey(typeof(TValue)))
            {
                actions.Add(typeof(TValue), PerThreadCache<ulong, TValue>.Clear);
            }
        }

        public void Invalidate()
        {
            InvalidateCaches();
        }

        public void Dispose()
        {
            this.InvalidateCaches();
        }

        private void InvalidateCaches()
        {
            var actions = this.GetInvalidateActions();
            foreach (Action action in actions.Values)
            {
                action();
            }
            actions.Clear();
        }
    }
}