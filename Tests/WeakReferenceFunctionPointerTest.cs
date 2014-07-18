using System;
using System.Reflection;

namespace TestApp.Tests
{
    public class WeakReferenceFunctionPointerTest : ATest
    {
        static WeakReferenceFunctionPointerTest() {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(Type));
            foreach (Type t in assembly.GetTypes())
            {
                if (t.Name.StartsWith("WeakReference") && t.IsGenericType )
                {
                    MethodInfo[] methods = t.GetMethods(BindingFlags.Instance |
                                                 BindingFlags.Static |
                                                 BindingFlags.Public |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.CreateInstance);
 
                    foreach (MethodInfo method in methods)
                    {
                        var ptr = method.MethodHandle.GetFunctionPointer();
                    }
                }
            }
        }

        public override void DoSomething()
        {
            WeakReference<object> weakRef = new WeakReference<object>(this);
        }
    }
}
