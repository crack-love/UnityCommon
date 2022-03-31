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
        public static async Task Run(Action action, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run(Action action, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T>(Action<T> action, T state, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T>(Action<T> action, T state, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T0, T1>(Action<T0, T1> action, T0 state0, T1 state1, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T0, T1>(Action<T0, T1> action, T0 state0, T1 state1, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                action(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<R>(Func<R> func, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func();

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<R>(Func<R> func, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func();

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T, R>(Func<T, R> func, T state, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func(state);

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T, R>(Func<T, R> func, T state, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func(state);

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T0, T1, R>(Func<T0, T1, R> func, T0 state0, T1 state1, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func(state0, state1);

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T0, T1, R>(Func<T0, T1, R> func, T0 state0, T1 state1, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                var res = func(state0, state1);

                return res;
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run(Func<Task> asyncAction, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run(Func<Task> asyncAction, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T>(Func<T, Task> asyncAction, T state, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T>(Func<T, Task> asyncAction, T state, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T0, T1>(Func<T0, T1, Task> asyncAction, T0 state0, T1 state1, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task Run<T0, T1>(Func<T0, T1, Task> asyncAction, T0 state0, T1 state1, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                await asyncAction(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<R>(Func<Task<R>> asyncFunc, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<R>(Func<Task<R>> asyncFunc, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc();
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T, R>(Func<T, Task<R>> asyncFunc, T state, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T, R>(Func<T, Task<R>> asyncFunc, T state, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc(state);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T0, T1, R>(Func<T0, T1, Task<R>> asyncFunc, T0 state0, T1 state1, TaskRunPlace place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        public static async Task<R> Run<T0, T1, R>(Func<T0, T1, Task<R>> asyncFunc, T0 state0, T1 state1, WorkerThread place, bool returnToMain = true)
        {
            try
            {
                await SwitchToPlace(place);

                return await asyncFunc(state0, state1);
            }
            finally
            {
                if (returnToMain)
                {
                    await new SwitchToMainAwaiter();
                }
            }
        }

        static Task SwitchToPlace(TaskRunPlace place)
        {
            switch (place)
            {
            case TaskRunPlace.Main:
                return SwitchToMain();
            case TaskRunPlace.TaskPool:
                return SwitchToTaskPool();
            case TaskRunPlace.ThreadPool:
                return SwitchToThreadPool();
            }

            throw new InvalidOperationException();
        }

        static Task SwitchToPlace(WorkerThread place)
        {
            return SwitchToWorkerThread(place);
        }
    }

    /// <summary>
    /// Where Task.Run method to Processing
    /// </summary>
    public enum TaskRunPlace
    {
        Main,
        TaskPool,
        ThreadPool,
    }
}