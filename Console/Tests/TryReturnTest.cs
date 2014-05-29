using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Tests
{
    class ReturnBeforeTryTest : ATest
    {
        public override void DoSomething()
        {
            for (int i = 0; i < 10; i++)
            {
                string result;
                bool rv = DoSomething(i, out result);
                Console.WriteLine("{0}:{1}", result, rv);
                rv = DoSomething(i);
                Console.WriteLine("RV={0}", rv);
            }
        }

        internal bool DoSomething(int arg)
        {
            if (arg >= 5)
            {
                return false;
            }
            try
            {
                if (arg%2 == 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        internal bool DoSomething(int arg, out string result)
        {
            if (arg >= 5)
            {
                result = "Nothing";
                return arg % 2 == 0;
            }
            try
            {
                switch (arg)
                {
                    case 0:
                        result = null;
                        return (0 / arg) == 0;
                    case 1:
                        result = null;
                        return (new[] {0, 1, 2})[3] == 0;
                    case 2:
                        result = null;
                        return result.Contains("0");
                    case 3:
                        result = null;
                        return (new Uri("helloWorld")).IsWellFormedOriginalString();
                    default:
                        throw new Exception();
                }
            }
            catch (DivideByZeroException e)
            {
                result = "DBZ";
                return true;
            }
            catch (NullReferenceException e)
            {
                result = "NPE";
                return true;
            }
            catch (IndexOutOfRangeException e)
            {
                result = "IOOR";
                return true;
            }
            catch (UriFormatException e)
            {
                result = "UFE";
                return true;
            }
            catch (Exception e)
            {
                result = "Unknown";
                return false;
            }
        }

        private bool Branch()
        {
            return true;
        }
    }
}
