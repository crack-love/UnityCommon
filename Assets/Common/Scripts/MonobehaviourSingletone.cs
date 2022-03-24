using UnityEngine;

namespace Common
{
    public class MonobehaviourSingletone<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected static object m_mutex;
        static T m_instance;

        static MonobehaviourSingletone()
        {
            m_mutex = new object();Debug.Log("Moutex " + typeof(T).FullName);
        }

        public static T Instance
        {
            get
            {
                if (!m_instance)
                {
                    lock (m_mutex)
                    {
                        if (!m_instance)
                        {
                            m_instance = FindObjectOfType<T>();
                        }
                    }
                }

                return m_instance;
            }
        }
    }
}
