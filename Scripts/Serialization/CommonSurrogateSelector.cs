using System.Runtime.Serialization;

/// <summary>
/// 2020-07-04 토 오후 8:37:28, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// <see cref="CommonSurrogate{T}"/> holder
    /// </summary>
    public partial class CommonSurrogateSelector : SurrogateSelector
    {
        void AddSurrogate<T>(CommonSurrogate<T> ss)
        {
            base.AddSurrogate(typeof(T), new StreamingContext(StreamingContextStates.All), ss);
        }

        public static SurrogateSelector Create()
        {
            CommonSurrogateSelector ss = new CommonSurrogateSelector();

            ss.AddSurrogate(new Vector3S());
            ss.AddSurrogate(new Vector3IntS());
            ss.AddSurrogate(new QuaternionS());

            return ss;
        }
    }
}