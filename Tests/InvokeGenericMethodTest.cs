using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestApp.Tests
{
    class InvokeGenericMethodTest : ATest
    {
        public override void DoSomething()
        {
            Type type = typeof(System.ServiceModel.Channels.MessageHeaders);
            MethodInfo method = type.GetMethod("GetHeader<object>");
            Console.WriteLine("Got method {0}", method);
        }

    }
}
