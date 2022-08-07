using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// 2020-08-15
/// </summary>
namespace UnityCommon
{
    public partial struct Task
    {
        internal static Task Completed()
        {
            return new Task();
        }

        internal static Task Completed(Exception e)
        {
            var src = CompletedSource.Create(e);

            return new Task(src);
        }

        internal static Task<T> Completed<T>(T result)
        {
            var src = CompletedSource<T>.Create(result);

            return new Task<T>(src);
        }

        internal static Task<T> Completed<T>(Exception e)
        {
            var src = CompletedSource<T>.Create(e);

            return new Task<T>(src);
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task Delay(float ms)
        {
            return new Task(DelaySource.Create(ms, false));
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task DelayUnscale(float ms)
        {
            return new Task(DelaySource.Create(ms, true));
        }

        public static async Task SwitchToMain()
        {
            await new SwitchToMainAwaiter();
        }

        public static async Task SwitchToPlayerLoop(PlayerLoopType playerLoop)
        {
            await new SwitchToPlayerLoopAwaiter(playerLoop);
        }

        public static async Task SwitchToTaskPool()
        {
            await new SwitchToTaskPoolAwaiter();
        }

        public static async Task SwitchToThreadPool()
        {
            await new SwitchToThreadPoolAwaiter();
        }

        public static async Task SwitchToWorkerThread(WorkerThread workerThread)
        {
            await new SwitchToWorkerThreadAwaiter(workerThread);
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task WhenAll(IEnumerable<Task> tasks)
        {
            return new Task(WhenAllSource.Create(tasks));
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task WhenAny(IEnumerable<Task> tasks)
        {
            return new Task(WhenAnySource.Create(tasks));
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task Until(Func<bool> trueIfFinished)
        {
            return new Task(WaitUntilSource.Create(trueIfFinished));
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task While(Func<bool> trueIfSuspending)
        {
            return new Task(WaitWhileSource.Create(trueIfSuspending));
        }

        /// <summary>
        /// Context switched to main thread
        /// </summary>
        public static Task Yield()
        {
            return SwitchToPlayerLoop(PlayerLoopType.Update);
        }

        public static Task Yield(PlayerLoopType playerLoop)
        {
            return SwitchToPlayerLoop(playerLoop);
        }

    }
}