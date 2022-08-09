using UnityEngine;

namespace UnityCommon
{
    public class MonobehaviourSingletone<TDerived> : MonoBehaviour
        where TDerived : MonobehaviourSingletone<TDerived>
    {
        protected static object m_singletoneMutex;
        static TDerived m_instance;

        static MonobehaviourSingletone()
        {
            m_singletoneMutex = new object();
        }

        public static TDerived Instance
        {
            get
            {
                if (!m_instance)
                {
                    lock (m_singletoneMutex)
                    {
                        if (!m_instance)
                        {
                            m_instance = FindObjectOfType<TDerived>();

                            if (!m_instance)
                            {
                                string tname = typeof(TDerived).FullName;
                                Debug.LogError("Can't Find Singletone Object " + tname);
                            }
                        }
                    }
                }

                return m_instance;
            }
        }

        protected void SetInstance(TDerived instance)
        {
            m_instance = instance;
        }
    }

    public class MonobehaviourSingletone<TDerived, TServ> : MonoBehaviour
        where TDerived : MonoBehaviour, TServ
    {
        protected static object m_singletoneMutex;
        static TServ m_instance;

        static MonobehaviourSingletone()
        {
            m_singletoneMutex = new object();
        }

        public static TServ Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_singletoneMutex)
                    {
                        if (m_instance == null)
                        {
                            TDerived found = FindObjectOfType<TDerived>();

                            // check bool operation with TDerived type not TServ which will be assinged.
                            // for prevent malfunction that destroyed unity object will return false but
                            // can be assigned and it will not null
                            if (!found)
                            {
                                string tname = typeof(TDerived).FullName;
                                Debug.LogError("Can't Find Singletone Object " + tname);
                            }
                            else
                            {
                                m_instance = found;
                            }
                        }
                    }
                }

                return m_instance;
            }
        }
        protected void SetInstance(TDerived instance)
        {
            m_instance = instance;
        }
    }
}
