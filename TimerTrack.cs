using System;
using System.Diagnostics;
namespace SNAKE_Game
{
    public class TimerTracker
    {
        private Stopwatch stopwatch=new Stopwatch();
        public TimerTracker()
        {
            stopwatch= new Stopwatch();
        }
        public void StartWatch()
        {
            stopwatch.Start();
        }
        public void StopWatch()
        {
            stopwatch.Stop();
        }
        public string Timer()
        {
            TimeSpan ts=stopwatch.Elapsed;
            //With 0,1,2,3 meaning the order of the clock display and 00 meaning the clock would display in format 0- if necessary
            return String.Format("{0:00}:{1:00}:{2:00}:{3:00}",ts.Hours,ts.Minutes,ts.Seconds,ts.Milliseconds/10);
        }
        public void ResetWatch()
        {
            stopwatch.Reset();
        }
    }
}