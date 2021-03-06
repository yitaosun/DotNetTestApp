﻿using System.Threading;

namespace TestApp.Tests.WCF
{
    class Suite : ATest
    {
        private readonly ServiceTest m_serviceTest;

        public Suite()
        {
            m_serviceTest = new ServiceTest();
        }

        public override void DoSomething()
        {
            m_serviceTest.DoSomething();
            var thread = new Thread(() =>
            {
                for (int i = 0; i < 200; i++)
                {
                    var asyncTest = new AsyncTest();
                    asyncTest.DoSomething();
                    var clientTest = new ClientTest();
                    clientTest.DoSomething();
                    var channelTest = new ChannelTest();
                    channelTest.DoSomething();
                    Thread.Sleep(500);
                }
            });
            thread.Start();
        }
    }
}
