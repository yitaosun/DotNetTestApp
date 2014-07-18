using System;
using System.Timers;

namespace TestApp.Tests
{
    class TimerTimerTest : ATest
    {
        private Timer m_worker;

        public TimerTimerTest()
        {
            m_worker = new Timer();
            m_worker.Interval = 1000;
            m_worker.Elapsed += DoWork;
            m_worker.Start();
        }

        private static void DoWork(object sender, EventArgs args)
        {
            Console.WriteLine("Doing work " + DateTime.Now.ToLongTimeString());
        }
    }
}
