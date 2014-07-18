using System;
using System.Threading;

namespace TestApp.Tests
{
    class ThreadingTimerTest : ATest
    {
        private Timer m_worker;

        public ThreadingTimerTest()
        {
            m_worker = new Timer(DoWork, null, 0, 1000);
        }

        private static void DoWork(object data)
        {
            Console.WriteLine("Doing ThreadingTimerTest work {0}", DateTime.Now.ToLongTimeString());
        }
    }
}
