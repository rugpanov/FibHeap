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

        public static void PrintHeap(FibNode node, int level = 0)
        {
            throw new NotImplementedException();
        }
    }
}