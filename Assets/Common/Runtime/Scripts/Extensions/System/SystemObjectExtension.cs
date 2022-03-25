using System;
using System.IO;
using System.Runtime.Serialization;

namespace Common
{
    public static class SystemObjectExtension
    {
        /// <summary>
        /// deep copy using serialization (T is Serializable)
        /// </summary>
        //public static T SerializeClone<T>(this T src) where T : ISerializable?
        //{
        //    return SerializeClone(src, Serialization.BinaryFormatter);
        //}

        /// <summary>
        /// deep copy using serialization (T is Serializable)
        /// </summary>
        public static T SerializeClone<T>(this T src, IFormatter formatter)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(src));
            }

            // Don't serialize a null object
            if (src == null)
            {
                return default;
            }
            
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, src);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}