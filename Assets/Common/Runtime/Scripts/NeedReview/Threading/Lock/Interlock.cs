using UnityEngine;
using UnityCommon;
using System.Threading;
using System;

/// <summary>
/// 2020-08-12 수 오후 7:56:43, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Provide Atomic Semapore Managing
    /// </summary>
    public static class Interlock
    {
        /// <summary>
        /// Decreasing semaphore to inclusive min. thread yield waiting.
        /// </summary>
        public static void Use(ref int semaphore, int min = 0)
        {
            while (true)
            {
                int dst = semaphore - 1;

                if (dst < min)
                {
                    // waiting
                    Thread.Yield();
                }
                else
                {
                    // write memory
                    if (TryExchange(ref semaphore, dst))
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Decreasing semaphore to inclusive min.
        /// </summary>
        public static bool TryUse(ref int semaphore, int min = 0)
        {
            int dst = semaphore - 1;

            while (dst >= min)
            {
                // write memory
                if (TryExchange(ref semaphore, dst))
                {
                    return true;
                }
                // retry
                else
                {
                    dst = semaphore - 1;
                }
            }

            return false;
        }

        /// <summary>
        /// Return semaphore inclusive max. thread yield waiting. increase count
        /// </summary>
        public static void Return(ref int semaphore, int max = 1)
        {
            Enter(ref semaphore, max);
        }

        public static bool TryReturn(ref int semaphore, int max = 1)
        {
            return TryEnter(ref semaphore, max);
        }

        /// <summary>
        /// Enter count inclusive max. thread yield waiting. increase count
        /// </summary>
        public static void Enter(ref int count, int max = 1)
        {
            while (true)
            {
                int dst = count + 1;

                if (dst > max)
                {
                    // waiting
                    Thread.Yield();
                }
                else
                {
                    // write memory
                    if (TryExchange(ref count, dst))
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Increase count inclusive max
        /// </summary>
        public static bool TryEnter(ref int count, int max = 1)
        {
            int dst = count + 1;

            while (dst <= max)
            {
                // write memory
                if (TryExchange(ref count, dst))
                {
                    return true;
                }
                // retry
                else
                {
                    dst = count + 1;
                }
            }

            return false;
        }

        /// <summary>
        /// decrease count inclusive min. thread yield waiting.
        /// </summary>
        public static void Exit(ref int count, int min = 0)
        {
            Use(ref count, min);
        }

        /// <summary>
        /// decrease count. break when noting to exit
        /// </summary>
        public static bool TryExit(ref int count, int min = 0)
        {
            return TryUse(ref count, min);
        }

        /// <summary>
        /// Wait count compare. thread yield waiting.
        /// </summary>
        public static void WaitCompare(ref int count, int value)
        {
            while (count != value)
            {
                Thread.Yield();
            }
        }

        /// <summary>
        /// Wait count inclusive min/max. thread yield waiting.
        /// </summary>
        public static void WaitMinMax(ref int count, int min, int max)
        {
            while (count < min || count > max)
            {
                // waiting
                Thread.Yield();
            }
        }

        public static bool TryExchange(ref float target, float value)
        {
            var copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }

        public static bool TryExchange(ref double target, double value)
        {
            var copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }

        public static bool TryExchange(ref int target, int value)
        {
            var copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }

        public static bool TryExchange(ref long target, long value)
        {
            var copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }

        public static bool TryExchange(ref IntPtr target, IntPtr value)
        {
            var copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }

        public static bool TryExchange<T>(ref T target, T value) where T : class
        {
            T copied = target;

            return Interlocked.CompareExchange(ref target, value, copied) == copied;
        }
    }

}