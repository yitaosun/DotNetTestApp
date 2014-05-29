using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using HelloWorld.WCFService;

namespace HelloWorld.Tests
{
    class WCFChannelTest : ATest
    {
        private Queue<WeakReference<ChannelFactory<IHelloWorldService>>> m_Factories; 
        private int m_Generation;

        public WCFChannelTest()
        {
            m_Generation = 1;
            m_Factories = new Queue<WeakReference<ChannelFactory<IHelloWorldService>>>();
            for (int i = 0; i < 20; i++)
            {
                m_Factories.Enqueue(new WeakReference<ChannelFactory<IHelloWorldService>>(CreateChannelFactory()));
            }
        }

        public override void DoSomething()
        {
            Person person = new Person();
            person.Name = "WCFService #1";
            for (int i = 0; i < 100000 && m_Generation < 200; i++)
            {
                if (i%30 == 0)
                {
                    GC.Collect(0);
                }
                WeakReference<ChannelFactory<IHelloWorldService>> factoryRef = m_Factories.Dequeue();
                ChannelFactory<IHelloWorldService> factory;
                if (!factoryRef.TryGetTarget(out factory))
                {
                    person.Name = string.Format("WCFService #{0}", m_Generation++);
                    factoryRef.SetTarget(factory = CreateChannelFactory());
                }
                m_Factories.Enqueue(factoryRef);
                Console.WriteLine(factory.CreateChannel().Hello(person));
                Thread.Sleep(100);
            } 
        }

        private ChannelFactory<IHelloWorldService> CreateChannelFactory()
        {
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress("http://localhost:8733/HelloWorld/WCFService/");
                return new ChannelFactory<IHelloWorldService>(binding, address);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

}
