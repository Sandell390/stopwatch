using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;


namespace stopwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The timer object
        private Timer timer = new Timer();

        // The Timer Thread
        private Thread t;

        public MainWindow()
        {
            
            InitializeComponent();
        }

        /// <summary>
        /// Increase the time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            Button b = (Button)sender;

            string tag = b.Tag.ToString();

            timer.AddTime(tag);
            UpdateCounter();
        }

        /// <summary>
        /// Decrement the time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            string tag = b.Tag.ToString();

            timer.DecrementTime(tag);
            UpdateCounter();
        }

        /// <summary>
        /// Updates the Timer Counter 
        /// </summary>
        void UpdateCounter()
        {
            HourValue.Content = timer.Hour.ToString("00");
            MinuteValue.Content = timer.Minute.ToString("00");
            SecondValue.Content = timer.Second.ToString("00");
        }

        /// <summary>
        /// Runs when the start button is clicked and stars the timer/countdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        /// <summary>
        /// Starts a new thread that counts down and plays a sound when done
        /// </summary>
        void StartTimer()
        {
            timer.CountDownChanged += Timer_CountDownChanged;
            timer.CountDownStopped += Timer_CountDownStopped;

            timer.StartCountDown();

            HideCounter();
            
        }

        private void Timer_CountDownStopped(object sender, TimerCountDownEventArgs e)
        {
            timer.isStarted = false;
            TimeLeft.Dispatcher.Invoke(() => { TimeLeft.Content = $"{e.Hour:00}:{e.Minute:00}:{e.Second:00}"; });
            timer.PlaySound();
            ShowCounter();

            timer.CountDownChanged -= Timer_CountDownChanged;
            timer.CountDownStopped -= Timer_CountDownStopped;
        }

        private void Timer_CountDownChanged(object sender, TimerCountDownEventArgs e)
        {
            Dispatcher.Invoke(() => { TimeLeft.Content = $"{e.Hour:00}:{e.Minute:00}:{e.Second:00}"; });
        }



        /*
        /// <summary>
        /// Counts down 
        /// </summary>
        void CountDown()
        {
            

            string timeleft = $"{timer.Hour:00}:{timer.Minute:00}:{timer.Second:00}";
            do
            {

                TimeLeft.Dispatcher.Invoke(() => { TimeLeft.Content = timeleft; });

                Thread.Sleep(1000);

                timeleft = timer.CountDown();

            } while (timeleft != "00:00:00");

            TimeLeft.Dispatcher.Invoke(() => { TimeLeft.Content = timeleft; });

            timer.PlaySound();
            timer.isStarted = false;

            

            ShowCounter();
        }
        */
        /// <summary>
        /// Shows the plus and minus buttons and hides the timer
        /// </summary>
        void ShowCounter()
        {
            StartButton.Dispatcher.Invoke(() =>
            {
                CounterPanel.Visibility = Visibility.Visible;
                TimeLeft.Visibility = Visibility.Hidden;
                StopButton.IsEnabled = false;
                StopButton.Content = "Pause";
                UpdateCounter();
            });
        }

        /// <summary>
        /// Hides the plus and minus button and shows the timer
        /// </summary>
        void HideCounter()
        {

            TimeLeft.Visibility = Visibility.Visible;
            StartButton.IsEnabled = false;
            CounterPanel.Visibility = Visibility.Hidden;
            StopButton.IsEnabled = true;

        }

        /// <summary>
        /// Runs the pause button is clicked and pauses the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton.Content = timer.PauseTimer();
        }

        /// <summary>
        /// Runs the Self destruct button is clicked and actives self destruct mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelfButton_Click(object sender, RoutedEventArgs e)
        {
            timer.ActiveSelfDestruct();

            if (!timer.isStarted)
            {
                StartTimer();
            }

            StopButton.IsEnabled = false;
        }
    }
}
