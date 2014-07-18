using System;
using System.Threading;

namespace TestApp.Tests
{
    class DisposableTimerTest : ATest
    {
        private Timer m_worker;
        private int m_period;
        private Timer m_distraction;
        private Timer m_disposer;

        public DisposableTimerTest()
        {
            m_period = 1000;
            m_worker = new Timer(DoWork, this, 1000, m_period);
            m_distraction = new Timer(DoDistraction, this, 2000, 1000);
            m_disposer = new Timer(DoDisposal, this, 20000, Timeout.Infinite);
        }

        private static void DoWork(object data)
        {
            Console.WriteLine("Doing some disposable work " + DateTime.Now.ToLongTimeString());
        }

        private static void DoDistraction(object data)
        {
            DisposableTimerTest test = (DisposableTimerTest) data;
            test.m_worker.Change(1000, (test.m_period += 1000));
        }

        private static void DoDisposal(object data)
        {
            DisposableTimerTest test = (DisposableTimerTest)data;
            test.m_distraction.Dispose();
            test.m_disposer.Dispose();
        }
    }
}
