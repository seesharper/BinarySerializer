namespace BinarySerializer
{
    using System;

    /// <summary>
    /// Creates a delegate that represents calling the appropriate write method 
    /// based on the requested type.    
    /// </summary>
    public interface IWriteMethodFactory
    {
        /// <summary>
        /// Creates a delegate that represents calling the appropriate write method 
        /// based on the requested type.    
        /// </summary>
        /// <typeparam name="T">The type of the value to be written.</typeparam>
        /// <param name="actualType">The <see cref="Type"/> for which to create a write method.</param>
        /// <returns>A delegate that invokes the </returns>
        Action<IWriter, T> CreateWriteMethod<T>(Type actualType);
    }
}