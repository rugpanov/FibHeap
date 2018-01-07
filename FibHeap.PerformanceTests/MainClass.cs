using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace FibHeap.PerformanceTests
{
    internal static class MainClass
    {
        private const int FROM = 1000;
        private const int TO = 100000;
        private const int STEP = 1000;
        private const string FILE_ABS_PATH = @"C:\Users\GriGri\Documents\Projects\FibHeap\results.csv";

        private static void Main(string[] args)
        {
            var sb = new StringBuilder();
            sb.Append(
                "Measure number of ticks to execute number of Random operations with probability of Push = 0.9\n");
            var fb = new FibHeap();
            MakeNumberOfRendomOperation(fb, 1000, 0.95);
            
            var timePerParse = Stopwatch.StartNew();
            MakeNumberOfRendomOperation(fb, 10000, 0.5);
            timePerParse.Stop();
            sb.Append($"1000;{timePerParse.ElapsedTicks / 10000}\n");
            
            for (int i = 10000; i < 700000; i += 20000)
            {
                var sumTime = 0L;
                for (int tries = 0; tries < 10; tries++)
                {
                    fb = new FibHeap();
                    MakeNumberOfRendomOperation(fb, i, 0.95);

                    timePerParse = Stopwatch.StartNew();
                    MakeNumberOfRendomOperation(fb, 10000, 0.5);
                    timePerParse.Stop();
                    sumTime += timePerParse.ElapsedTicks / 10000;
                }
                sb.Append($"{i};{sumTime / 10}\n");
                Console.WriteLine(sb);
            }
            Console.WriteLine(sb);
            //System.IO.File.WriteAllText(FILE_ABS_PATH, sb.ToString());
        }

        private static StringBuilder MeasureNumberOfRendomOperation(double probabilityOfPush)
        {
            var sb = new StringBuilder();
            var fb = new FibHeap();
            var timePerParse = Stopwatch.StartNew();
            for (var i = FROM; i < TO; i += STEP)
            {
                MakeNumberOfRendomOperation(fb, STEP, probabilityOfPush);
                timePerParse.Stop();
                sb.Append($"{i};{timePerParse.ElapsedTicks}\n");
                timePerParse.Start();
            }
            timePerParse.Stop();
            return sb;
        }

        static Random rand = new Random();
        private static void MakeNumberOfRendomOperation(FibHeap fb, int number, double probabilityOfPush)
        {
            for (var i = 0; i < number; i++)
            {
                if (IsNextOperationPush(probabilityOfPush))
                {
                    fb.Push(rand.Next(100));
                }
                else if (fb.MySize > 0)
                {
                    fb.Pop();
                }
            }
            fb.Pop();
        }

        private static readonly Random Rand = new Random();

        private static bool IsNextOperationPush(double probabilityOfPush)
        {
            return Rand.Next(100) < probabilityOfPush * 100;
        }
        
    }
}