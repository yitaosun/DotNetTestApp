using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace HelloWorld.Tests
{
    class WCFServiceTest : ATest
    {
        public override void DoSomething()
        {
            ServiceHost host = new ServiceHost(typeof(HelloWorld.WCFService.HelloWorldService));
            host.Open();
            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                Console.WriteLine(ep.Address);
            }
        }
    }
}
