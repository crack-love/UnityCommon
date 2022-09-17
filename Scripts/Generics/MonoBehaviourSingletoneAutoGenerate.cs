using UnityEngine;

namespace UnityCommon
{
    public class MonoBehaviourSingletoneAutoGenerate<TDerived> : MonoBehaviour
        where TDerived : MonoBehaviourSingletoneAutoGenerate<TDerived>
    {
        static readonly object m_mutext = new object();
        static TDerived m_instance = null;

        public static TDerived Instance
        {
            get
            {
                if (IsInstanceInvalid())
                {
                    lock (m_mutext)
                    {
                        if (IsInstanceInvalid())
                        {
                            m_instance = FindObject();
                        }
                    }
                }

                return m_instance;
            }
        }

        static bool IsInstanceInvalid()
        {
            return !m_instance;
        }

        static TDerived FindObject()
        {
            m_instance = FindObjectOfType<TDerived>();

            if (IsInstanceInvalid())
            {
                GameObject o = new GameObject(typeof(TDerived).Name);
                m_instance = o.AddComponent<TDerived>();
            }

            return m_instance;
        }
    }

    public class MonoBehaviourSingletoneAutoGenerate<TDerived, TServ> : MonoBehaviour
        where TDerived : MonoBehaviourSingletoneAutoGenerate<TDerived, TServ>, TServ
        where TServ : class
    {
        static readonly object m_mutext = new object();
        static TServ m_instance = null;

        public static TServ Instance
        {
            get
            {
                if (IsInstanceInvalid())
                {
                    lock (m_mutext)
                    {
                        if (IsInstanceInvalid())
                        {
                            m_instance = FindObject();
                        }
                    }
                }

                return m_instance;
            }
        }

        static bool IsInstanceInvalid()
        {
            return !(m_instance as MonoBehaviour);
        }

        static TServ FindObject()
        {
            m_instance = FindObjectOfType<TDerived>();

            if (IsInstanceInvalid())
            {
                GameObject o = new GameObject(typeof(TDerived).Name);
                m_instance = o.AddComponent<TDerived>();
            }

            return m_instance;
        }
    }
}
