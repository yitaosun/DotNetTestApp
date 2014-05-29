using HelloWorld.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HelloWorldService.wcfservice
{
    public class ImageService:IImageService
    {

        public int GetImageCount()
        {
            int storedMB = WeakReferenceTest.GetRandomImageCount();
            return storedMB;
        }
    }

    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IImageService
    {
        [OperationContract]
        int GetImageCount();
    }
}
