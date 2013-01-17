namespace BinarySerializer
{
    using System;
    using System.Reflection.Emit;

    /// <summary>
    /// Represents the skeleton of a dynamic method.
    /// </summary>    
    public interface IWriteMethodSkeleton
    {
        /// <summary>
        /// Gets the <see cref="ILGenerator"/> used to emit the method body.
        /// </summary>
        /// <returns>An <see cref="ILGenerator"/> instance.</returns>
        ILGenerator GetILGenerator<T>();

        /// <summary>
        /// Create a delegate used to invoke the dynamic method.
        /// </summary>
        /// <returns>A function delegate.</returns>
        Action<BinarySerializationWriter, T> CreateDelegate<T>();
    }
}