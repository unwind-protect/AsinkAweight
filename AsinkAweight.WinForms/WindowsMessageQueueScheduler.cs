using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AsinkAweight.WinForms
{
    public class WindowsMessageQueueScheduler : IScheduleActions
    {
        public void Queue(Action a)
        {
            Application.OpenForms[0].BeginInvoke(a);
        }
    }
}
