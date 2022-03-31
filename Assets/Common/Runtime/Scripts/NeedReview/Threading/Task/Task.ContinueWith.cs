using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

/// <summary>
/// 2020-08-15
/// </summary>
namespace UnityCommon
{
    public partial struct Task
    {
        // Return continuation task

        public async Task ContinueWith(Action continuation)
        {
            await this;

            continuation();
        }

        public async Task ContinueWith<T>(Action<T> continuation, T state)
        {
            await this;

            continuation(state);
        }

        public async Task ContinueWith<T0, T1>(Action<T0, T1> continuation, T0 state0, T1 state1)
        {
            await this;

            continuation(state0, state1);
        }

        public async Task<R> ContinueWith<R>(Func<R> continuation)
        {
            await this;

            return continuation();
        }

        public async Task<R> ContinueWith<T, R>(Func<T, R> continuation, T state)
        {
            await this;

            return continuation(state);
        }

        public async Task<R> ContinueWith<T0, T1, R>(Func<T0, T1, R> continuation, T0 state0, T1 state1)
        {
            await this;

            return continuation(state0, state1);
        }

        public async Task ContinueWith(Task continuation)
        {
            await this;

            await continuation;
        }

        public async Task<R> ContinueWith<R>(Task<R> continuation)
        {
            await this;

            return await continuation;
        }
    }

    public partial struct Task<TR>
    {
        public async Task ContinueWith(Action continuation)
        {
            await this;

            continuation();
        }

        public async Task ContinueWith(Action<TR> continuation)
        {
            var res = await this;

            continuation(res);
        }

        public async Task ContinueWith<T>(Action<T> continuation, T state)
        {
            await this;

            continuation(state);
        }

        public async Task ContinueWith<T>(Action<TR, T> continuation, T state)
        {
            var res = await this;

            continuation(res, state);
        }

        public async Task ContinueWith<T0, T1>(Action<T0, T1> continuation, T0 state0, T1 state1)
        {
            await this;

            continuation(state0, state1);
        }

        public async Task ContinueWith<T0, T1>(Action<TR, T0, T1> continuation, T0 state0, T1 state1)
        {
            var res = await this;

            continuation(res, state0, state1);
        }

        public async Task<R> ContinueWith<R>(Func<R> continuation)
        {
            await this;

            return continuation();
        }

        public async Task<R> ContinueWith<R>(Func<TR, R> continuation)
        {
            var res = await this;

            return continuation(res);
        }

        public async Task<R> ContinueWith<T, R>(Func<T, R> continuation, T state)
        {
            await this;

            return continuation(state);
        }

        public async Task<R> ContinueWith<T, R>(Func<TR, T, R> continuation, T state)
        {
            var res = await this;

            return continuation(res, state);
        }

        public async Task<R> ContinueWith<T0, T1, R>(Func<T0, T1, R> continuation, T0 state0, T1 state1)
        {
            await this;

            return continuation(state0, state1);
        }

        public async Task<R> ContinueWith<T0, T1, R>(Func<TR, T0, T1, R> continuation, T0 state0, T1 state1)
        {
            var res = await this;

            return continuation(res, state0, state1);
        }

        public async Task ContinueWith(Task continuation)
        {
            await this;

            await continuation;
        }

        public async Task<R> ContinueWith<R>(Task<R> continuation)
        {
            await this;

            return await continuation;
        }
    }
}