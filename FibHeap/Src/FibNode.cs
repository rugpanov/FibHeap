using System.Xml;

namespace FibHeap
{
    public class FibNode
    {
        public FibNode Parent = null;
        public FibNode Child = null;
        public FibNode Left = null;
        public FibNode Right = null;
        public int Degree;
        public bool Mark = false;        // метка - был ли удален один из дочерних элементов
        public int Key;          // числовое значение узла

        public FibNode(int key)
        {
            Key = key;
        }

        public void SetChild(FibNode node)
        {
            FibUtils.SetNeighbors(node.Left, node.Right);
            FibUtils.SetNeighbors(node, node);
            if (Child == null)
            {
                Child = node;
            }
            else
            {
                FibUtils.UnionNodes(Child, node);
                node.Parent = this;
                if (Child.Key > node.Key)
                {
                    Child = node;
                }
            }
            Degree += 1 + node.Degree;
        }
    }
}