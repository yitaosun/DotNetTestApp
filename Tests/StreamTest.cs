using System;
using System.IO;
using System.Threading;

namespace TestApp.Tests
{
    public class StreamTest : ATest
    {
        private WeakReference<MemoryStream> _StreamRef;

        public StreamTest()
        {
            _StreamRef = new WeakReference<MemoryStream>(null);
        }

        public override void DoSomething()
        {
            for (int iterations = 0; iterations < 100; iterations++)
            {
                WriteNext(10000, 10000);
            }
        }

        private void WriteNext(int count, int blockSize)
        {
            try
            {
                GetStream(false);
                byte[] block = new byte[blockSize];
                for (int i = count; i > 0; i--)
                {
                    MemoryStream stream = GetStream(true);
                    stream.Write(block, 0, blockSize);
                    Thread.Sleep(10);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Skipping iteration");
                Console.WriteLine(e);
                Thread.Sleep(100);
            }
        }

        private MemoryStream GetStream(Boolean append)
        {
            MemoryStream stream;
            if (!append || !_StreamRef.TryGetTarget(out stream))
            {
                _StreamRef.SetTarget(stream = new MemoryStream());
            }
            return stream;
        }
    }
}
