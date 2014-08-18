using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Tests.WCF
{
    class Suite : ATest
    {
        private ServiceTest m_serviceTest;

        public Suite()
        {
            m_serviceTest = new ServiceTest();
        }

        public override void DoSomething()
        {
            m_serviceTest.DoSomething();
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    AsyncTest asyncTest = new AsyncTest();
                    asyncTest.DoSomething();
                    ClientTest clientTest = new ClientTest();
                    clientTest.DoSomething();
                    ChannelTest channelTest = new ChannelTest();
                    channelTest.DoSomething();
                    Thread.Sleep(200);
                }
            });
            thread.Start();
        }
    }
}
