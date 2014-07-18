using System.Runtime.Serialization;
using System.ServiceModel;

namespace TestApp.Tests.WCF
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string Hello(Person person);
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
    }
}
