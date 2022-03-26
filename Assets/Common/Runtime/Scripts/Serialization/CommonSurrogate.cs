using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 2020-07-04 토 오후 8:37:28, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    abstract class CommonSurrogate<T> : ISerializationSurrogate
    {
        protected abstract void GetObjectData(T obj, SerializationInfo info, StreamingContext context);

        protected abstract T SetObjectData(T obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            GetObjectData((T)obj, info, context);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            return SetObjectData((T)obj, info, context, selector);            
        }
    }
}