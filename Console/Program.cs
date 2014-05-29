using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HelloWorld.Tests;

namespace HelloWorld
{
    class Program
    {
        private static List<ATest> Tests = new List<ATest>();
        static Program()
        {
            //Tests.Add(new HelloWorldTest());
            //Tests.Add(new WeakReferenceTest());
            Tests.Add(new WCFServiceTest());
            Tests.Add(new BackgroundTest(new WCFChannelTest()));
            //Tests.Add(new BackgroundTest(new AppDomainTest(typeof(WCFChannelTest))));
            //Tests.Add(new BackgroundTest(new AppDomainTest(typeof(WeakReferenceTest))));
            //Tests.Add(new GenericLocalVariableTest());
            //Tests.Add(new ReturnBeforeTryTest());
            //Tests.Add(new GenericWeakReferenceTest());
            for (int i = 0; i < 50; i++)
            {
                Tests.Add(new BackgroundTest(new StreamTest()));
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    foreach (ATest test in Tests)
                    {
                        RunTest(test);
                    }
                }
                else
                {
                    foreach (String testName in args)
                    {
                        RunTest((ATest)Assembly.GetExecutingAssembly().CreateInstance(testName));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.WriteLine("Goodbye cruel world");
                Console.ReadKey(false);
                Environment.Exit(0);
            }
        }

        static void RunTest(ATest test)
        {
            Console.WriteLine("{0} started", test.GetName());
            test.DoSomething();
            Console.WriteLine("{0} finished", test.GetName());
        }
    }
}
