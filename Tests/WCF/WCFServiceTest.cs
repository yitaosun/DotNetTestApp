using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TestApp.Tests.WCF
{
    public class WCFServiceTest : ATest
    {
        public override void DoSomething()
        {
            ServiceHost host = new ServiceHost(typeof(HelloWorldService));
            host.Open();
            foreach (ServiceEndpoint ep in host.Description.Endpoints)
            {
                Console.WriteLine(ep.Address);
            }
        }
    }
}
