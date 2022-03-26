using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 2020-07-04 토 오후 8:37:28, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    class CommonSurrogateSelector : SurrogateSelector
    {
        public void AddSurrogate<T>(CommonSurrogate<T> ss)
        {
            base.AddSurrogate(typeof(T), new StreamingContext(StreamingContextStates.All), ss);
        }
    }
}