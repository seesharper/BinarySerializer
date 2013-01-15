namespace BinarySerializer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

 
    public static class TypeCodes
    {
        private static ConcurrentDictionary<Type, byte> TypeCodeMap;

        private static ConcurrentDictionary<byte, Type> TypeMap;
                            
        public const byte Null = 0x0;
        public const byte Byte = 0x1;        
        public const byte ByteArray = 0x3;
        public const byte Boolean = 0x4;        
        public const byte Int16 = 0x6;        
        public const byte Int32 = 0x8;        
        public const byte Int64 = 0xA;        
        public const byte UInt16 = 0xC;        
        public const byte UInt32 = 0xE;        
        public const byte UInt64 = 0x10;        
        public const byte Decimal = 0x12;       
        public const byte Double = 0x14;        
        public const byte Float = 0x16;        
        public const byte Type = 0x18;
        public const byte DateTime = 0x19;        
        public const byte Guid = 0x1B;        
        public const byte String = 0x1D;        
        public const byte Array = 0x1E;
        public const byte Collection = 0x1F;
        public const byte Dictionary = 0x20;

        public const byte Object = 0xFE;
        public const byte BinarySerializable = 0xFF;

        static TypeCodes()
        {
            InitializeTypeCodeMap();
            InitializeTypeMap();
        }

        private static void InitializeTypeMap()
        {
            Dictionary<byte, Type> simpleTypes = new Dictionary<byte, Type>();

            simpleTypes.Add(TypeCodes.Byte, typeof(byte));
            simpleTypes.Add(TypeCodes.ByteArray, typeof(byte[]));
            simpleTypes.Add(TypeCodes.Boolean, typeof(bool));
            simpleTypes.Add(TypeCodes.Int16, typeof(short));
            simpleTypes.Add(TypeCodes.Int32, typeof(int));
            simpleTypes.Add(TypeCodes.Int64, typeof(long));
            simpleTypes.Add(TypeCodes.UInt16, typeof(ushort));
            simpleTypes.Add(TypeCodes.UInt32, typeof(uint));
            simpleTypes.Add(TypeCodes.UInt64, typeof(ulong));
            simpleTypes.Add(TypeCodes.Double, typeof(double));
            simpleTypes.Add(TypeCodes.Decimal, typeof(decimal));
            simpleTypes.Add(TypeCodes.Float, typeof(float));
            simpleTypes.Add(TypeCodes.Type, typeof(Type));
            simpleTypes.Add(TypeCodes.DateTime, typeof(DateTime));
            simpleTypes.Add(TypeCodes.Guid, typeof(Guid));
            simpleTypes.Add(TypeCodes.String, typeof(string));
            TypeMap = new ConcurrentDictionary<byte, Type>(simpleTypes);
        }

        private static void InitializeTypeCodeMap()
        {
            Dictionary<Type, byte> simpleTypes = new Dictionary<Type, byte>();            
            simpleTypes.Add(typeof(byte), Byte);
            simpleTypes.Add(typeof(byte[]), ByteArray);
            simpleTypes.Add(typeof(bool), Boolean);
            simpleTypes.Add(typeof(short), Int16);
            simpleTypes.Add(typeof(int), Int32);
            simpleTypes.Add(typeof(long), Int64);
            simpleTypes.Add(typeof(ushort), UInt16);
            simpleTypes.Add(typeof(uint), UInt32);
            simpleTypes.Add(typeof(ulong), UInt64);
            simpleTypes.Add(typeof(double), Double);
            simpleTypes.Add(typeof(decimal), Decimal);
            simpleTypes.Add(typeof(float), Float);
            simpleTypes.Add(typeof(Type), Type);
            simpleTypes.Add(typeof(DateTime), DateTime);
            simpleTypes.Add(typeof(Guid), Guid);
            simpleTypes.Add(typeof(string), String);
            TypeCodeMap = new ConcurrentDictionary<Type, byte>(simpleTypes);
            
        }

        public static byte GetTypeCode(Type type)
        {
            return TypeCodeMap.GetOrAdd(type, ResolveTypeCode);
        }

        public static Type GetType(byte typeCode)
        {
            Type type;
            TypeMap.TryGetValue(typeCode, out type);
            return type;
        }

        private static byte ResolveTypeCode(Type type)
        {
            if (typeof(IBinarySerializable).IsAssignableFrom(type))
            {
                return BinarySerializable;
            }
            return Object;
        }
    }
}