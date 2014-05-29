using HelloWorld;
using System.Collections.Generic;
using System.ServiceProcess;

namespace HelloWorldService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
            //Tests.Add(new WeakReferenceTest());

            //foreach (ATest test in Tests)
            //{
            //    RunTest(test);
            //}
            //Console.ReadLine();
        }

        private static List<ATest> Tests = new List<ATest>();
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        static void RunTest(ATest test)
        {
            _logger.Trace("{0} started", test.GetName());
            test.DoSomething();
            _logger.Trace("{0} finished", test.GetName());
        }

    }
}
