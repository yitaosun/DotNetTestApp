using System.Threading;

namespace TestApp.Tests
{
    public class AThreadedTest : ATest
    {
        private ATest m_InnerTest;

        public AThreadedTest(ATest inner)
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
            return string.Format("AThreadedTest({0})", m_InnerTest.GetType().Name);
        }
    }
}
