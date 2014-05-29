
using System.Dynamic;

namespace HelloWorld.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloWorldService" in both code and config file together.
    public class HelloWorldService : IHelloWorldService
    {
        public string Hello(Person person)
        {
            if (person == null)
            {
                return string.Format("Hello?");
            }
            else
            {
                return string.Format("Hello {0}", person.Name);
            }
        }
    }
}
