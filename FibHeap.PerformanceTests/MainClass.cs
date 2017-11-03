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
            sb.Append("Measure number of ticks to push 10 elements to FibHeap\n");
            for (var i = FROM; i < TO; i += STEP)
            {
                var fh = GenerateFibHeapWithNumberOfElements(i);
                var numberOfTicks = MeasureTicksToPush(fh);
                sb.Append($"{i};{numberOfTicks}\n");
            }

            sb.Append("Measure number of ticks to pop an element from a flat FibHeap\n");
            for (var i = FROM; i < TO; i += STEP)
            {
                var fh = GenerateFibHeapWithNumberOfElements(i);
                var numberOfTicks = MeasureTicksToPop(fh);
                sb.Append($"{i};{numberOfTicks}\n");
            }

            var fibHeap = new FibHeap();
            sb.Append("Measure number of ticks to pop an element from a not-flat FibHeap\n");
            for (var i = FROM; i < TO; i += STEP)
            {
                AddNumberOfElements(fibHeap, STEP);
                var numberOfTicks = MeasureTicksToPop(fibHeap);
                sb.Append($"{i};{numberOfTicks}\n");
            }

            sb.Append(
                "Measure number of ticks to execute number of Random operations with probability of Push = 0.7\n");
            sb.Append(MeasureNumberOfRendomOperation(0.7));

            sb.Append(
                "Measure number of ticks to execute number of Random operations with probability of Push = 0.9\n");
            sb.Append(MeasureNumberOfRendomOperation(0.9));
            System.IO.File.WriteAllText(FILE_ABS_PATH, sb.ToString());
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

        private static void MakeNumberOfRendomOperation(FibHeap fb, int number, double probabilityOfPush)
        {
            for (var i = 0; i < number; i++)
            {
                if (IsNextOperationPush(probabilityOfPush))
                {
                    fb.Push(100);
                }
                else if (fb.GetMinNode() != null)
                {
                    fb.Pop();
                }
            }
        }

        private static readonly Random Rand = new Random();

        private static bool IsNextOperationPush(double probabilityOfPush)
        {
            return Rand.Next(100) < probabilityOfPush * 100;
        }

        private static void AddNumberOfElements(FibHeap fh, int number)
        {
            for (var i = 0; i < number; i++)
            {
                fh.Push(i);
            }
        }

        private static FibHeap GenerateFibHeapWithNumberOfElements(int number)
        {
            var fh = new FibHeap();
            for (var i = 0; i < number; i++)
            {
                fh.Push(i);
            }
            return fh;
        }

        private static long MeasureTicksToPush(FibHeap fh)
        {
            var timePerParse = Stopwatch.StartNew();
            for (var i = 0; i < 10; i++)
            {
                fh.Push(239);
            }
            timePerParse.Stop();
            return timePerParse.ElapsedTicks;
        }

        private static long MeasureTicksToPop(FibHeap fh)
        {
            var timePerParse = Stopwatch.StartNew();
            for (var i = 0; i < 1; i++)
            {
                fh.Pop();
            }
            timePerParse.Stop();
            return timePerParse.ElapsedTicks;
        }
    }
}