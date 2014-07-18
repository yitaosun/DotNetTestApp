using System;

namespace TestApp.Tests
{
    abstract public class ATest : MarshalByRefObject
    {
        public virtual void DoSomething()
        {
            
        }

        public virtual string GetName()
        {
            return GetType().Name;
        }
    }
}
