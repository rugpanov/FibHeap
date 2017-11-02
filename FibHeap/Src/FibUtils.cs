using System;

namespace FibHeap
{
    public static class FibUtils
    {
        public static void UnionNodes(FibNode first, FibNode second)
        {
            first.Left.SetRightNeighbor(second.Right);
            second.SetRightNeighbor(first);
        }

        public static void PrintHeap(FibNode node, int level = 0)
        {
            if (node == null)
            {
                return;
            }
            var last = node.Left;
            var current = node;
            Console.WriteLine(new string(' ', level * 2) + current.NodeKey + "->" + current.Right.NodeKey);
            PrintHeap(current.Child, level + 1);
            while (current != last)
            {
                current = current.Right;
                PrintHeap(current.Child, level + 1);
                Console.WriteLine(new string(' ', level * 2) + current.NodeKey + "->" + current.Right.NodeKey);
            }
        }

        public static void SafeRemoveNode(FibNode fibNode)
        {
            fibNode.RemoveLinkFromParent();
            fibNode.Left.SetRightNeighbor(fibNode.Right);
            fibNode.SetRightNeighbor(fibNode);
        }
    }
}