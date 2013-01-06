namespace BinarySerializer
{
    using System.Collections.Generic;

    public class ObjectWriter<T> where T : class
    {
        private readonly IBinarySerializationWriter writer;

        private readonly Dictionary<T, int> cache = new Dictionary<T, int>();

        public ObjectWriter(IBinarySerializationWriter writer)
        {
            this.writer = writer;
        }

        void Write(T value)
        {
            int token;
            if (cache.TryGetValue(value, out token))
            {
                
            }


        }
    }
}