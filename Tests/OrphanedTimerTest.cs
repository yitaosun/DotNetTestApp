using System;
using System.Threading;

namespace TestApp.Tests
{
    class OrphanedTimerTest : ATest
    {
        private Timer m_gc;

        public OrphanedTimerTest()
        {
            new Timer(DoWork, null, 0, 1000);
            m_gc = new Timer(DoDispose, null, 5000, 5000);
        }

        private static void DoWork(object data)
        {
            Console.WriteLine("Doing OrphanedTimerTest work " + DateTime.Now.ToLongTimeString());
        }

        private static void DoDispose(object data)
        {
            GC.Collect();
            Console.WriteLine("OrphanedTimerTest started GC");
        }
    }
}
