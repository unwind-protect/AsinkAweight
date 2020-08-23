using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AsinkAweight
{
    public class ThreadPoolScheduler : IScheduleActions
    {
        public void Queue(Action a)
        {
            ThreadPool.QueueUserWorkItem(Run, a, true);
        }

        private static void Run(Action a)
        {
            a();
        }
    }
}
