using System;
using System.Reflection;

namespace TestApp.Tests
{
    public class AAppDomainTest : ATest
    {
        private Type m_TestType;

        public AAppDomainTest(Type testType)
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
            Assembly asm = Assembly.GetAssembly(m_TestType);
            ATest test = (ATest)ad.CreateInstanceAndUnwrap(asm.FullName, m_TestType.FullName);
            test.DoSomething();
            AppDomain.Unload(ad);
        }

        public override string GetName()
        {
            return string.Format("AAppDomainTest({0})", m_TestType.Name);
        }
    }
}
