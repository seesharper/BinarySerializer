namespace BinarySerializer
{
    using System;
    
    public enum TypeCode : byte
    {
        Null,
        Object,
        BinarySerializable,
        Boolean,
        Byte,
        Int16,
        Uint16,
        Int32,
        UInt32
    }

    public static class TypeCodes
    {
        public const byte Null = 0x0;
        public const byte Byte = 0x1;
        
        public const byte BinarySerializable = 0x11;
        public const byte Object = 0x11;

    }
}