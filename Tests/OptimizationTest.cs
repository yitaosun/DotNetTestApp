using System;

namespace TestApp.Tests
{
    public class OptimizationTest : ATest
    {
        public override void DoSomething()
        {
            DoSomethingElse();
        }

        private static void DoSomethingElse()
        {
            try
            {
                Console.WriteLine("Doing work " + DateTime.Now.ToLongTimeString());
            }
            catch (Exception x)
            {
                Console.WriteLine("Error in WebResponse: " + x.Message);
            }
        }
    }
}
