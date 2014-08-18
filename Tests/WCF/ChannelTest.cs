using System;
using System.ServiceModel;

namespace TestApp.Tests.WCF
{
    class ChannelTest : ATest
    {
        public override void DoSomething()
        {
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                OpenTimeout = TimeSpan.FromMinutes(5),
                CloseTimeout = TimeSpan.FromMinutes(5),
                SendTimeout = TimeSpan.FromMinutes(5),
                ReceiveTimeout = TimeSpan.FromMinutes(5),
            };
            EndpointAddress address = new EndpointAddress("http://localhost:8733/HelloWorldService/");
            ChannelFactory<IHelloWorldService> factory = new ChannelFactory<IHelloWorldService>(binding, address);
            IHelloWorldService channel = factory.CreateChannel();
            Console.WriteLine(channel.Hello(new Person() {Name = "WCFChannelClientTestPerson"}));
        }
    }
}
