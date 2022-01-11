using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stopwatch
{
    public class Timer
    {
        public bool isStarted = false;

        private bool selfdestruct = false;

        private int hour;

        private int minute;

        private int second;

        public bool isPaused;

        public int Hour
        {
            get => hour;
        }

        public int Minute
        {
            get => minute;
        }
        
        public int Second
        {
            get => second;
        }

        public event EventHandler<TimerCountDownEventArgs> CountDownChanged;

        public event EventHandler<TimerCountDownEventArgs> CountDownStopped; 

        public Timer()
        {

        }

        /// <summary>
        /// Adds time
        /// </summary>
        /// <param name="type"></param>
        public void AddTime(string type)
        {
            switch (type.ToLower())
            {
                case "hour":
                    hour += 1;
                    break;
                case "minute":

                    if ((minute + 1) >= 60)
                    {
                        hour += 1;
                        minute = 0;
                        break;
                    }
                    minute += 1;
                    break;
                case "second":
                    if ((second + 1) >= 60)
                    {
                        minute += 1;
                        second = 0;
                        break;
                    }
                    second += 1;
                    break;
            }
        }

        /// <summary>
        /// Decrement time
        /// </summary>
        /// <param name="type"></param>
        public void DecrementTime(string type)
        {
            switch (type.ToLower())
            {
                case "hour":
                    if ((hour - 1) >= 0)
                    {
                        hour -= 1;
                    }
                    break;
                case "minute":

                    if ((minute - 1) >= 0)
                    {
                        minute -= 1;
                    }
                    break;
                case "second":
                    if ((second - 1) >= 0)
                    {
                        second -= 1;
                    }
                    break;
            }
        }


        public void StartCountDown()
        {
            isStarted = true;

            Thread t = new Thread(CountDown);
            t.Name = "Timer";
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// counts down
        /// </summary>
        public void CountDown()
        {
            while (true)
            {
                while (isPaused)
                {
                    Thread.Sleep(100);
                }

                CountDownChanged?.Invoke(this, new TimerCountDownEventArgs(hour, minute, second));

                if (second <= 0)
                {
                    if (hour > 0)
                    {
                        hour--;
                        minute = 60;
                    }
                    if (minute > 0)
                    {
                        minute--;
                        second = 60;
                    }

                }

                if (hour == 0 && minute == 0 && second == 0)
                {
                    CountDownStopped?.Invoke(this, new TimerCountDownEventArgs(hour, minute, second));
                    return;
                }

                second--;

                Thread.Sleep(1000);
            }
            
        }

        /// <summary>
        /// Pause the timer
        /// </summary>
        /// <returns> The pause text</returns>
        public string PauseTimer()
        {
            if (isPaused)
            {
                isPaused = false;
                return "Pause";
            }
            else
            {
                isPaused = true;
                return "Unpause";
            }

        }

        /// <summary>
        /// Play a sound
        /// </summary>
        public void PlaySound()
        {
            AudioPlayer audio = new AudioPlayer(selfdestruct);

            audio.PlaySound();

            if (selfdestruct)
            {
                SelfDestruct();
            }
        }

        /// <summary>
        /// DESTROYS EVERYTHING
        /// </summary>
        void SelfDestruct()
        {
            Environment.Exit(1);
        }

        /// <summary>
        /// Turns on Self Destruct mode 
        /// </summary>
        public void ActiveSelfDestruct()
        {
            if (!selfdestruct)
            {
                isPaused = false;
                selfdestruct = true;
                hour = 0;
                minute = 0;
                second = 10;
            }
        }

    }
}
