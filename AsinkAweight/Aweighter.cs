using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AsinkAweight
{
    public class Aweighter<T>: INotifyCompletion
    {
        public bool IsCompleted => throw new NotImplementedException();

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        public T GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
