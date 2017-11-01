using System;
using System.Diagnostics;

namespace FibHeap.Tests
{
    public static class FibHeapTest
    {
        public static void Test()
        {
            
            int n = 2;
            for (var j = 0; j < 25; j++)
            {
                var fh = new FibHeap();
                n = n * 2;
                var timePerParse = Stopwatch.StartNew();
                for (var i = 0; i < n; i++)
                {
                    fh.Push(i);
                }
                timePerParse.Stop();
                Console.WriteLine("Add {0}: {1}", n, timePerParse.ElapsedTicks * 10000 / n);
            }
            n = 2;
            for (var j = 0; j < 25; j++)
            {
                var fh = new FibHeap();
                n = n * 2;
                for (var i = 0; i < n; i++)
                {
                    fh.Push(i);
                }
                var timePerParse = Stopwatch.StartNew();
                for (var i = 0; i < n; i++)
                {
                    fh.Pop();
                }
                timePerParse.Stop();
                Console.WriteLine("Remove {0}: {1}", n, (int)(timePerParse.ElapsedTicks * 10000 / n / Math.Log(n)));
            }
        }
    }
}