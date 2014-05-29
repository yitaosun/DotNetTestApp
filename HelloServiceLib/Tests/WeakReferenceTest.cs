using HelloWorldService;
using HelloWorldService.HelloServiceLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace HelloWorld.Tests
{
    public class WeakReferenceTest : ATest
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static Cache c = null;
        private static int cacheSize = 10;

        private static Random r1 = new Random();
        public static long GetRandomImagedata()
        {
            int index = r1.Next(cacheSize);
            long data = c[index].TotalSizeCaptured();
            //for (int i = 0; i < cacheSize; i++)
            //{
            //    data = c[i].TotalSizeCaptured();
            //}
            return data;
        }

        internal static int GetRandomImageCount()
        {
            int index = r1.Next(cacheSize);
            int data = c[index].FilesLoaded();
            return data;
        }

        public override void DoSomething()
        {
            // Create the cache. 
            //int cacheSize = 10;
            if (ConfigurationManager.AppSettings["CacheSize"] != null)
            {
                string size = ConfigurationManager.AppSettings["CacheSize"];
                Int32.TryParse(size, out cacheSize);
            }

            Random r = new Random();
            c = new Cache(cacheSize);

            string DataName = "";
            //GC.Collect(0);

            // Randomly access objects in the cache. 
            for (int i = 0; i < c.Count; i++)
            {
                int index = r.Next(c.Count);

                // Access the object by getting a property value.
                DataName = c[index].TotalSizeCaptured().ToString();
            }
            // Show results. 
            double regenPercent = c.RegenerationCount / (double)c.Count;
            _logger.Trace("Cache size: {0}, Regenerated: {1:P2}%", c.Count, regenPercent);
            Thread.Sleep(25);
        }

        class Cache
        {
            //private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

            // Dictionary to contain the cache. 
            static Dictionary<int, WeakReference> _cache;

            // Track the number of times an object is regenerated. 
            int regenCount = 0;

            public Cache(int count)
            {
                _cache = new Dictionary<int, WeakReference>();

                // Add objects with a short weak reference to the cache. 
                for (int i = 0; i < count; i++)
                {
                    _cache.Add(i, new WeakReference(new ImageData(), true));
                }
            }

            // Number of items in the cache. 
            public int Count
            {
                get { return _cache.Count; }
            }

            // Number of times an object needs to be regenerated. 
            public int RegenerationCount
            {
                get { return regenCount; }
            }

            // Retrieve a data object from the cache. 
            public ImageData this[int index]
            {
                get
                {
                    ImageData d = _cache[index].Target as ImageData;
                    if (d == null)
                    {
                        // If the object was reclaimed, generate a new one.
                        _logger.Trace("Regenerate object at {0}: Yes", index);
                        d = new ImageData();
                        _cache[index].Target = d;
                        regenCount++;
                    }
                    else
                    {
                        // Object was obtained with the weak reference.
                        _logger.Trace("Regenerate object at {0}: No", index);
                    }

                    return d;
                }
            }
        }

    }


}
