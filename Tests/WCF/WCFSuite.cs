using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Tests.WCF
{
    class WCFSuite : ATest
    {
        public override void DoSomething()
        {
            (new WCFServiceTest()).DoSomething();
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    WCFAsyncTest asyncTest = new WCFAsyncTest();
                    asyncTest.DoSomething();
                    WCFClientTest clientTest = new WCFClientTest();
                    clientTest.DoSomething();
                    WCFChannelTest channelTest = new WCFChannelTest();
                    channelTest.DoSomething();
                    Thread.Sleep(200);
                }
            });
            thread.Start();
        }
    }
}
