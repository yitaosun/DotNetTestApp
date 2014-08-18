using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestApp.Tests;
using TestApp.Tests.WCF;

namespace TestApp
{
    class Program
    {
        private static List<ATest> DefaultTests = new List<ATest>();

        static Program()
        {
            DefaultTests.Add(new ThreadingTimerTest());
            DefaultTests.Add(new AThreadedTest(new HelloWorldTest()));
            DefaultTests.Add(new ServiceTest());
            for (int i = 0; i < 50; i++) { DefaultTests.Add(new ClientTest()); }
            DefaultTests.Add(new ChannelTest());
            DefaultTests.Add(new AsyncTest());
            DefaultTests.Add(new GenericLocalVariableTest());
            DefaultTests.Add(new ReturnBeforeTryTest());
        }

        static void Main(string[] args)
        {
            List<ATest> tests = args.Length == 0 ? DefaultTests : CreateTests(args);
            foreach (ATest test in tests)
            {
                try
                {
                    Console.ReadKey();
                    Console.WriteLine("Started {0}", test.GetName());
                    test.DoSomething();
                    Console.WriteLine("Finished {0}", test.GetName());
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} threw exception {1}", test, e);
                    Console.WriteLine(e.StackTrace);
                }
            }
            Console.WriteLine("Goodbye cruel world");
            Console.ReadKey(false);
            Environment.Exit(0);
        }

        static List<ATest> CreateTests(string[] testClassNames)
        {
            return testClassNames.Select(testClassName => (ATest) Assembly.GetExecutingAssembly().CreateInstance(string.Format("TestApp.Tests.{0}", testClassName))).ToList();
        }
    }
}
