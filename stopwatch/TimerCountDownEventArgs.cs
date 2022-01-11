using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stopwatch
{
    public class TimerCountDownEventArgs : EventArgs
    {
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        public TimerCountDownEventArgs(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

    }
}
