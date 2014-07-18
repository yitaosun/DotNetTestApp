using System.Threading.Tasks;

namespace TestApp.Tests
{
    public class AAsyncTest : ATest
    {
        private ATest m_InnerTest;

        public AAsyncTest(ATest inner)
        {
            m_InnerTest = inner;
        }

        public override void DoSomething()
        {
            Task.Factory.StartNew(() => { m_InnerTest.DoSomething(); });
        }

        public override string GetName()
        {
            return string.Format("AAsyncTest({0})", m_InnerTest.GetType().Name);
        }
    }
}
