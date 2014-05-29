using HelloWorld;
using HelloWorld.Tests;
using HelloWorldService.wcfservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            Tests.Add(new WeakReferenceTest());
            Tests.Add(new BackgroundTest(new AppDomainTest(typeof(WCFChannelTest))));
            Tests.Add(new BackgroundTest(new AppDomainTest(typeof(WeakReferenceTest))));

            if (ConfigurationManager.AppSettings["Throw"] != null)
            {
                throwError = ("true".Equals(ConfigurationManager.AppSettings["Throw"]));
            }
        }
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public ServiceHost serviceHost = null;
        private bool throwError = true;

        protected override void OnStart(string[] args)
        {
            // load all images available in directory
            this.RequestAdditionalTime(600000);

            foreach (ATest test in Tests)
            {
                RunTest(test);
            }

            // Host wcf service
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            try
            {
                // Create a ServiceHost for the CalculatorService type and 
                // provide the base address.
                serviceHost = new ServiceHost(typeof(ImageService));

                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                serviceHost.Open();

                createWCFcall();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            // starting a thread to keep doing it..
            Task t = Task.Factory.StartNew(() => { RunAsyncTest(); });
        }

        private void createWCFcall()
        {
            ServiceReference1.ImageServiceClient client = new ServiceReference1.ImageServiceClient();
            client.GetImageCount();
            client.Close();
        }

        void RunAsyncTest()
        {
            while (true)
            {
                try
                {
                    long storedMB = WeakReferenceTest.GetRandomImagedata();
                    //_logger.Trace(storedMB);
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    _logger.Error("RunAsyncTest-WeakReferenceTest call " + ex.Message);
                }
                try
                {
                    ServiceReference1.ImageServiceClient client = new ServiceReference1.ImageServiceClient();
                    client.GetImageCount();
                    client.Close();
                }
                catch (Exception ex)
                {
                    _logger.Error("RunAsyncTest-wcf call " + ex.Message);
                    if (throwError)
                        throw ex;
                }
            }
        }

        private static List<ATest> Tests = new List<ATest>();

        void RunTest(ATest test)
        {
            _logger.Trace("{0} started", test.GetName());
            try
            {
                test.DoSomething();
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to run test-{0}.. - {1]", test.GetName(), ex.Message);
            }
            _logger.Trace("{0} finished", test.GetName());
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
