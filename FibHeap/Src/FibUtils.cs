using System;
using System.Collections.Generic;

namespace FibHeap
{
    public static class FibUtils
    {

        public static void ConcatLists(LinkedList<FibNode> to, IEnumerable<FibNode> from)
        {
            foreach (var node in from)
            {
                to.AddLast(node);
            }
        }

        public static void ConcatLists(MyLinkedList to, MyLinkedList from)
        {
            if (from.Count == 0)
            {
                return;
            }
            var current = from.First;
            while (current != null)
            {
                to.AddLast(current.Value);
                current = current.Next;
            }
        }

        public static void PrintHeap(FibNode node, int level = 0)
        {
            throw new NotImplementedException();
        }
    }
}