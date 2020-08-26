using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AsinkAweight
{
    public sealed class TuskMethodBuilder<T>
    {
        Tusk<T> tusk;

        public TuskMethodBuilder()
        {
            tusk = new Tusk<T>();
        }

        public static TuskMethodBuilder<T> Create()
            => new TuskMethodBuilder<T>();

        public void SetResult(T val)
        {
            tusk.SetResult(val);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public Tusk<T> Task => tusk;

        public void SetException(Exception exception) 
        { /* */ }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            var local = stateMachine;
            awaiter.OnCompleted(() => local.MoveNext());
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
           ref TAwaiter awaiter,
           ref TStateMachine stateMachine)
           where TAwaiter : ICriticalNotifyCompletion
           where TStateMachine : IAsyncStateMachine
        {
            var local = stateMachine;
            awaiter.OnCompleted(() => local.MoveNext());
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        { /* */ }


    }
}
