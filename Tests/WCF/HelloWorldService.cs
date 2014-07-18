
using System.Threading;

namespace TestApp.Tests.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloWorldService" in both code and config file together.
    public class HelloWorldService : IHelloWorldService
    {
        public string Hello(Person person)
        {
            Thread.Sleep(500);
            return person == null ? "Hello?" : string.Format("Hello {0}", person.Name);
        }
    }
}
