using System;
using System.Collections.Generic;
using System.Text;

namespace AsinkAweight
{
    public class TuskCompletionSource<T>
    {
        public Tusk<T> Tusk { get; } = new Tusk<T>();

        public void SetResult(T val) { Tusk.SetResult(val); }
    }
}
