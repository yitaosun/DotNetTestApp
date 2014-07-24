using System;

namespace TestApp.Tests
{
    class IntPtrBitnessTest : ATest
    {
        public override void DoSomething()
        {
            Console.WriteLine("IntPtr.Size is {0}", IntPtr.Size);
            try
            {
                Console.Write("Creating 64-bit IntPtr...");
                new IntPtr((long)Int32.MaxValue+1);
                Console.WriteLine("success");
            }
            catch (OverflowException)
            {
                Console.WriteLine("failed with OverflowException");
            }
        }
    }
}
