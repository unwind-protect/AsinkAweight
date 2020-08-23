using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AsinkAweight
{
    public class Aweighter<T>: INotifyCompletion
    {
        private Tusk<T> _tusk;


        public Aweighter(Tusk<T> tusk)
        {
            _tusk = tusk;
        }


        public bool IsCompleted => _tusk.IsCompleted;


        public void OnCompleted(Action continuation)
        {
            _tusk.OnCompleted(continuation);
        }


        public T GetResult() => _tusk.GetResult();
    }
}
