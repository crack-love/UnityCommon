using UnityEngine;

namespace UnityCommon
{
    /// <summary>
    /// Adding Some Commont to GameObject, with Edit/Apply button
    /// </summary>
    public class RobustComment : Comment
    {
        [SerializeField] string m_state;

        public string State
        {
            get => m_state;
            set => m_state = value;
        }
    }
}