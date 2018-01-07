using System.Collections.Generic;
using System.Linq.Expressions;

namespace FibHeap
{
    public class FibNode
    {
        public int NodeKey;
        public int Degree = 0;
        public LinkedList<FibNode> Children = new LinkedList<FibNode>();

        public FibNode(int nodeKey)
        {
            NodeKey = nodeKey;
        }
    }
}