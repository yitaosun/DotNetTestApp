using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorld.Tests
{
    class HelloWorldTest : ATest
    {
        public override void DoSomething()
        {
            System.Console.WriteLine("Hello world");
        }
    }
}
