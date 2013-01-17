namespace BinarySerializer
{
    using System;

    /// <summary>
    /// Creates a delegate that represents calling the appropriate write method 
    /// based on the requested type.    
    /// </summary>
    public interface IWriteMethodFactory
    {
        Action<BinarySerializationWriter, T> CreateWriteMethod<T>(Type actualType);
    }
}