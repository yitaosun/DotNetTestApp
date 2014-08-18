using System;

namespace TestApp.Tests.WCF
{
    public class ClientTest : ATest
    {
        public override void DoSomething()
        {
            IHelloWorldService client = new HelloWorldService();
            Console.WriteLine(client.Hello(new Person() {Name = "WCFClientTestPerson"}));
        }
    }

}
