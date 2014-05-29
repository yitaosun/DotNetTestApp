using System;

namespace HelloWorld
{
    abstract class ATest : MarshalByRefObject
    {
        public abstract void DoSomething();

        public virtual string GetName()
        {
            return GetType().Name;
        }
    }
}
