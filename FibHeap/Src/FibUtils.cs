using System;

namespace FibHeap
{
    public static class FibUtils
    {
        public static void UnionNodes(FibNode first, FibNode second)
        {
            SetNeighbors(first.Left, second.Right);
            SetNeighbors(second, first);
        }

        public static void SetNeighbors(FibNode left, FibNode right)
        {
            right.Left = left;
            left.Right = right;
        }

        public static void PrintHeap(FibNode node, int level = 0)
        {
            if (node == null)
            {
                return;
            }
            var last = node.Left;
            var current = node;
            Console.WriteLine(new string(' ', level * 2) + current.Key + "->" + current.Right.Key);
            PrintHeap(current.Child, level + 1);
            while (current != last)
            {
                current = current.Right;
                PrintHeap(current.Child, level + 1);
                Console.WriteLine(new string(' ', level * 2) + current.Key + "->" + current.Right.Key);
            }
        }
    }
}