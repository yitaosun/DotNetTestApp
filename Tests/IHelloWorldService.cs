using System.Runtime.Serialization;
using System.ServiceModel;

namespace HelloWorld.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHelloWorldService" in both code and config file together.
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string Hello(Person person);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "HelloWorldWCF.ContractType".
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
    }
}
