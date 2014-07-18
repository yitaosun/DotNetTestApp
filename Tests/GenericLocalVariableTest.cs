using System;
using System.Collections.Generic;

namespace TestApp.Tests
{
    public class GenericLocalVariableTest : ATest
    {
        public override void DoSomething()
        {
            int i = 0;
            Random rand = new Random();
            List<int> container = new List<int>();
            for (i = 0; i < 100; i++)
            {
                container.Add(rand.Next(i));
            }
        }
    }
}
