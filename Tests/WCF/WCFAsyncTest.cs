using System;
using TestApp.Tests.WCF.Async;

namespace TestApp.Tests.WCF
{
    class WCFAsyncTest : ATest
    {
        private HelloWorldServiceClient m_client;

        public WCFAsyncTest()
        {
            m_client = new HelloWorldServiceClient();
        }

        public override void DoSomething()
        {
            while (true)
            {
                Console.WriteLine(m_client.Hello(new TestApp.Tests.WCF.Async.Person() { Name = "WCFAsyncTestPerson1" }));
                m_client.BeginHello(new TestApp.Tests.WCF.Async.Person() { Name = "WCFAsyncTestPerson2" }, result => { Console.WriteLine(m_client.EndHello(result)); }, null);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    return;
            } 
        }
    }
}
