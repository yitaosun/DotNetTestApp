using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using HelloWorldService.wcfservice;

namespace HelloWorld.Tests
{
    public class WCFChannelTest : ATest
    {
        private Queue<WeakReference<ChannelFactory<IImageService>>> m_Factories; 
        private int m_Generation;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public WCFChannelTest()
        {
            m_Generation = 1;
            m_Factories = new Queue<WeakReference<ChannelFactory<IImageService>>>();
            for (int i = 0; i < 20; i++)
            {
                m_Factories.Enqueue(new WeakReference<ChannelFactory<IImageService>>(CreateChannelFactory()));
            }
        }

        public override void DoSomething()
        {
            for (int i = 0; i < 100000 && m_Generation < 200; i++)
            {
                if (i%30 == 0)
                {
                    GC.Collect(0);
                }
                WeakReference<ChannelFactory<IImageService>> factoryRef = m_Factories.Dequeue();
                ChannelFactory<IImageService> factory;
                if (!factoryRef.TryGetTarget(out factory))
                {
                    //person.Name = string.Format("WCFService #{0}", m_Generation++);
                    factoryRef.SetTarget(factory = CreateChannelFactory());
                }
                m_Factories.Enqueue(factoryRef);
                _logger.Trace(factory.CreateChannel().GetImageCount());
                Thread.Sleep(100);
            } 
        }

        private ChannelFactory<IImageService> CreateChannelFactory()
        {
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                EndpointAddress address = new EndpointAddress("http://localhost:8733/HelloWorld/WCFService/");
                return new ChannelFactory<IImageService>(binding, address);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

    }

}
