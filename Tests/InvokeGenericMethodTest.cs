using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestApp.Tests
{
    class InvokeGenericMethodTest : ATest
    {
        public override void DoSomething()
        {
            Type type = typeof(GenericMethodTest);
            object obj = new GenericMethodTest();
            object[] methodParams = new object[] { 10 };
            MethodInfo genericMethod = type.GetMethod("GetSomething");
            MethodInfo resolvedMethod = genericMethod.MakeGenericMethod(new Type[] {typeof(int)});
            TryInvoke(obj, genericMethod, methodParams);
            TryInvoke(obj, resolvedMethod, methodParams);
        }

        private void TryInvoke(object obj, MethodInfo m, object[] p)
        {
            try
            {
                Console.Write("Trying to invoke {0}... ", m);
                m.Invoke(obj, p);
                Console.WriteLine("succeeded");
            }
            catch (Exception e)
            {
                Console.WriteLine("failed");
                Console.WriteLine(e);
            }            
        }

    }
}
