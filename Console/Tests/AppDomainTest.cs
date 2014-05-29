using System;
using System.Reflection;
using System.Threading;

namespace HelloWorld.Tests
{
    class AppDomainTest : ATest
    {
        private Type m_TestType;

        public AppDomainTest(Type testType)
        {
            m_TestType = testType;
        }

        public override void DoSomething()
        {
            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase = System.Environment.CurrentDirectory;
            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.LoaderOptimization = LoaderOptimization.MultiDomainHost;
            ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            AppDomain ad = AppDomain.CreateDomain("AppDomainTest", null, ads);
            ATest test = (ATest)ad.CreateInstanceAndUnwrap(Assembly.GetEntryAssembly().FullName, m_TestType.FullName);
            test.DoSomething();
            AppDomain.Unload(ad);
        }

        public override string GetName()
        {
            return string.Format("AppDomainTest({0})", m_TestType.Name);
        }
    }
}
