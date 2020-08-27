using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AsinkAweight
{
    [System.Runtime.CompilerServices.AsyncMethodBuilder(typeof(TuskMethodBuilder<>))]
    public class Tusk<T>
    {
        private T _result;
        private Action _continuation;
        private bool _useDefaultScheduler;

        public Aweighter<T> GetAwaiter()
        {
            return new Aweighter<T>(this);
        }

        internal bool IsCompleted { get; private set; }

        internal void SetResult(T val)
        {
            // Warning! Race condition! Who would set the result twice anyway?!
            if (IsCompleted)
                throw new InvalidOperationException("Result already set");

            _result = val;
            IsCompleted = true;

            if (_continuation!=null)
                Queue(_continuation);
        }

        internal void OnCompleted(Action continuation)
        {
            if (_continuation != null)
                throw new InvalidOperationException("Continuation already set!");

            // It turns out that this can be called after SetResult
            if (IsCompleted)
            {
                // Should we just execute it immediately instead?
                Queue(continuation);
            }
            else
            {
                // Warning!  Probably still a race condition!...
                _continuation = continuation;
            }
        }

        internal T GetResult()
        {
            while (!IsCompleted)
                System.Threading.Thread.Sleep(100);

            return _result;
        }

        private void Queue(Action continuation)
        {
            if (_useDefaultScheduler)
                ActionScheduler.Default.Queue(continuation);
            else
                ActionScheduler.Current.Queue(continuation);
        }
        public static Tusk<T> FromResult(T val)
        {
            var tusk = new Tusk<T>();
            tusk.SetResult(val);
            return tusk;
        }

        public Tusk<T> UseDefaultScheduler()
        {
            _useDefaultScheduler = true;
            return this;
        }
    }
}
