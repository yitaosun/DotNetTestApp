using System;
using TestApp.Tests.WCF.Async;

namespace TestApp.Tests.WCF
{
    class AsyncTest : ATest
    {
        private HelloWorldServiceClient m_client;

        public AsyncTest()
        {
            m_client = new HelloWorldServiceClient();
        }

        public override void DoSomething()
        {
            Console.WriteLine(m_client.Hello(new TestApp.Tests.WCF.Async.Person() { Name = "WCFAsyncTestPerson1" }));
            m_client.BeginHello(new TestApp.Tests.WCF.Async.Person() { Name = "WCFAsyncTestPerson2" }, result => { Console.WriteLine(m_client.EndHello(result)); }, null);
        }
    }
}
