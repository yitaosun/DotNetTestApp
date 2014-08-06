using System;

namespace TestApp.Tests
{
    class GenericMethodTest : ATest
    {
        public override void DoSomething()
        {
            System.Threading.Thread.Sleep(500);
        }

        public T GetSomething<T>(int i)
        {
            Type type = typeof(T);
            if (type == typeof(string) || type == typeof(object))
                return (T)(object)i.ToString();
            else if (type == typeof(int))
                return (T)(object)i;
            else if (type == typeof(float))
                return (T)(object)(float)i;
            else
                return default(T);
        }
    }
}
