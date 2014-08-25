using System.Threading;

namespace TestApp.Tests.SelfHosted
{
    class Suite : ATest
    {
        private HttpSelfHostServerTest m_serverTest;

        public Suite()
        {
            m_serverTest = new HttpSelfHostServerTest();
        }

        public override void DoSomething()
        {
            m_serverTest.DoSomething();
            Thread.Sleep(2000);
            var thread = new Thread(() =>
            {
                for (int i = 0; i < 200; i++)
                {
                    var clientTest = new HttpClientTest();
                    clientTest.DoSomething();
                    var invokerTest = new HttpMessageInvokerTest();
                    invokerTest.DoSomething();
                    Thread.Sleep(500);
                }
            });
            thread.Start();
        }
    }
}
