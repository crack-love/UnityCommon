using UnityEngine;

namespace UnityCommon
{
    /// <summary>
    /// Adding Some Comment to GameObject
    /// </summary>
    public class Comment : MonoBehaviour
    {
        [SerializeField] string m_context;

        public string Text
        {
            get => m_context;
            set => m_context = value;
        }
    }
}