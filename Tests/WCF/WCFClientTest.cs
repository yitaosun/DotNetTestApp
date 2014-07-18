using System;

namespace TestApp.Tests.WCF
{
    public class WCFClientTest : ATest
    {
        public override void DoSomething()
        {
            IHelloWorldService client = new HelloWorldService();
            Console.WriteLine(client.Hello(new Person() {Name = "WCFClientTestPerson"}));
        }
    }

}
