using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-09-28
/// </summary>
namespace UnityCommon
{
    // equals compare each task's source

    public partial struct Task : IEquatable<Task>
    {
        public static bool operator ==(Task left, Task right)
        {
            return left.m_src == right.m_src;
        }

        public static bool operator !=(Task left, Task right)
        {
            return left.m_src == right.m_src;
        }

        public bool Equals(Task other)
        {
            return m_src == other.m_src;
        }

        public override bool Equals(object other)
        {
            if (other is Task otherTask)
            {
                Debug.Log("ValueType boxing ocurred");

                return m_src.Equals(otherTask.m_src);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return m_src.GetHashCode();
        }
    }

    public partial struct Task<TR> : IEquatable<Task<TR>>
    {
        public static bool operator ==(Task<TR> left, Task<TR> right)
        {
            return left.m_src == right.m_src;
        }

        public static bool operator !=(Task<TR> left, Task<TR> right)
        {
            return left.m_src == right.m_src;
        }

        public bool Equals(Task<TR> other)
        {
            return m_src == other.m_src;
        }

        public override bool Equals(object other)
        {
            if (other is Task<TR> otherTask)
            {
                Debug.Log("ValueType boxing ocurred");

                return m_src.Equals(otherTask);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return m_src.GetHashCode();
        }
    }
}
