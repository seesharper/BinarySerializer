namespace BinarySerializer
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Exposes a <see cref="BitVector32.Section"/> for each element of a <see cref="DateTime"/> structure.
    /// </summary>
    internal static class DateTimeBitVectorSections
    {
        /// <summary>
        /// A 14 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Year"/>.
        /// </summary>
        internal static readonly BitVector32.Section Year = BitVector32.CreateSection(9999);

        /// <summary>
        /// A 4 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Month"/>.
        /// </summary>
        internal static readonly BitVector32.Section Month = BitVector32.CreateSection(12, Year);

        /// <summary>
        /// A 5 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Day"/>.
        /// </summary>
        internal static readonly BitVector32.Section Day = BitVector32.CreateSection(31, Month); // 5 bits

        /// <summary>
        /// A 1 bit <see cref="BitVector32.Section"/> used to indicate if the target <see cref="DateTime"/> 
        /// instance contains time information.
        /// </summary>
        internal static readonly BitVector32.Section HasTime = BitVector32.CreateSection(1, Day); // 1

        /// <summary>
        /// A 5 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Hour"/>.
        /// </summary>
        internal static readonly BitVector32.Section Hour = BitVector32.CreateSection(23);

        /// <summary>
        /// A 6 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Minute"/>.
        /// </summary>
        internal static readonly BitVector32.Section Minute = BitVector32.CreateSection(59, Hour);

        /// <summary>
        /// A 6 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Second"/>.
        /// </summary>
        internal static readonly BitVector32.Section Second = BitVector32.CreateSection(59, Minute);        

        /// <summary>
        /// A 10 bit <see cref="BitVector32.Section"/> used to represent <see cref="DateTime.Millisecond"/>.
        /// </summary>
        internal static readonly BitVector32.Section MilliSecond = BitVector32.CreateSection(999, Second);        
    }
}