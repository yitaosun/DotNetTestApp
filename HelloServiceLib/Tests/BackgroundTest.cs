using System;
using System.Reflection;
using System.Threading;

namespace HelloWorld.Tests
{
    public class BackgroundTest : ATest
    {
        private ATest m_InnerTest;

        public BackgroundTest(ATest inner)
        {
            m_InnerTest = inner;
        }

        public override void DoSomething()
        {
            Thread thread = new Thread(m_InnerTest.DoSomething);
            thread.Start();
        }

        public override string GetName()
        {
            return string.Format("BackgroundTest({0})", m_InnerTest.GetType().Name);
        }
    }
}
