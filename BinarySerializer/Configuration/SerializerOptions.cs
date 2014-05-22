using System;
using System.Text;

namespace BinarySerializer.Configuration
{
    using System.ComponentModel;
    using System.Configuration;

    /// <summary>
    /// Represents the serialization options. 
    /// </summary>
    public class SerializerOptions : ConfigurationSection, IBinarySerializable
    {                        
        /// <summary>
        /// Gets or sets the code page used for string values.
        /// </summary>     
        [ConfigurationProperty("codePage", DefaultValue = (uint)0)]
        public uint CodePage
        {
            get { return (uint)this["codePage"]; }
            set { this["codePage"] = value; }            
        }

        /// <summary>
        /// Gets or sets the string length threshold value that determines if the string will be compressed. 
        /// </summary>
        [ConfigurationProperty("threshold", DefaultValue = (uint)0xff)]
        public uint Threshold
        {
            get { return (uint)this["threshold"]; }
            set { this["threshold"] = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="ICompressor"/> type used to compress string values.
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("compressorType", DefaultValue = typeof(DeflateCompressor))]
        public Type CompressorType
        {
            get { return (Type)this["compressorType"]; }
            set { this["compressorType"] = value; }
        }

        /// <summary>
        /// Allows an object to serialize its own data using the <paramref name="writer"/>
        /// </summary>
        /// <param name="writer">The <see cref="IBinarySerializationWriter"/> that is used to serialize the data.</param>
        public void Serialize(IWriter writer)
        {
            writer.Write(CodePage);
            writer.Write(Threshold);
            writer.Write(Encoding.Unicode.GetBytes(CompressorType.AssemblyQualifiedName));
        }

        /// <summary>
        /// Allows an object to deserialize its own data using the <paramref name="reader"/>
        /// </summary>
        /// <param name="reader">The <see cref="IBinarySerializationReader"/> that is used to deserialize the object graph.</param>
        public void Deserialize(IReader reader)
        {
            CodePage = reader.ReadUInt32();
            Threshold = reader.ReadUInt32();            
            CompressorType = TypeHelper.GetType(Encoding.Unicode.GetString(reader.ReadBytes()));
        }
    }
}
