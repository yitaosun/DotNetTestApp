using System;
using System.Reflection;
using System.Threading;

namespace HelloWorld.Tests
{
    public class AppDomainTest : ATest
    {
        private Type m_TestType;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public AppDomainTest(Type testType)
        {
            m_TestType = testType;
        }
        public AppDomainTest()
        {
            m_TestType = this.GetType();
        }

        public override void DoSomething()
        {
            try
            {
                AppDomainSetup ads = new AppDomainSetup();
                ads.ApplicationBase = System.Environment.CurrentDirectory;
                ads.DisallowBindingRedirects = false;
                ads.DisallowCodeDownload = true;
                ads.LoaderOptimization = LoaderOptimization.MultiDomainHost;
                ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                AppDomain ad = AppDomain.CreateDomain("AppDomainTest", null, ads);
                //ATest test = (ATest)ad.CreateInstanceAndUnwrap(Assembly.GetEntryAssembly().FullName, m_TestType.FullName);
                ATest test = (ATest)ad.CreateInstanceAndUnwrap("HelloWorldService", m_TestType.FullName);

                test.DoSomething();
                // weak ref call..
                long storedMB = WeakReferenceTest.GetRandomImagedata();
                //..
                AppDomain.Unload(ad);
            }
            catch (Exception ex)
            {
                _logger.Error(Assembly.GetEntryAssembly().FullName + "--" + ex.Message);
            }
        }

        public override string GetName()
        {
            return string.Format("AppDomainTest({0})", m_TestType.Name);
        }
    }
}
