using System;
using System.Collections.Generic;
using System.Text;

namespace AsinkAweight
{
    public static class ActionScheduler
    {
        static ActionScheduler()
        {
            Current = Default = new ThreadPoolScheduler();
        }

        public static IScheduleActions Default { get; set; }
        public static IScheduleActions Current { get; set; }
    }

    public interface IScheduleActions
    {
        void Queue(Action a);
    }
}
