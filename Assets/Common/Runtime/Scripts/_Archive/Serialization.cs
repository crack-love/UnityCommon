/*using UnityEngine;
using UnityCommon;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/// <summary>
/// 2021-03-15 월 오후 5:05:49, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class Serialization
    {
        static CommonSurrogateSelector m_css;
        static BinaryFormatter m_bf;

        public static BinaryFormatter BinaryFormatter => m_bf;

        static Serialization()
        {
            m_css = CommonSurrogateSelectorFactory.CreateSurroageSelector();
            m_bf = new BinaryFormatter(m_css, new StreamingContext(StreamingContextStates.All));
        }
    }
}*/