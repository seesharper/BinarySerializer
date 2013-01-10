namespace BinarySerializer
{
    using System;
    using System.Collections.Generic;
            
    public class WriteCache : IDisposable
    {
        [ThreadStatic]
        private static Dictionary<Type, Action> InvalidateActions;

        public WriteCache()
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
                
        public bool TryGetValue<TKey>(TKey key, out ulong value)
        {
            RegisterInvalidateAction<TKey>();
            return PerThreadCache<TKey, ulong>.TryGetValue(key, out value);
        }

        public void Add<TKey>(TKey key, ulong value)
        {
            RegisterInvalidateAction<TKey>();
            PerThreadCache<TKey, ulong>.Add(key, value);
        }

        private void RegisterInvalidateAction<TKey>()
        {
            var actions = GetInvalidateActions();
            if (!actions.ContainsKey(typeof(TKey)))
            {
                actions.Add(typeof(TKey), PerThreadCache<TKey, ulong>.Clear);
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