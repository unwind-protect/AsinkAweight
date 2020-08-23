using System;
using System.Collections.Generic;
using System.Text;

namespace AsinkAweight
{
    public class Tusk<T>
    {
        private T _result;

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
        }

        internal T GetResult()
        {
            while (!IsCompleted)
                System.Threading.Thread.Sleep(100);

            return _result;
        }

        public static Tusk<T> FromResult(T val)
        {
            var tusk = new Tusk<T>();
            tusk.SetResult(val);
            return tusk;
        }
    }
}
