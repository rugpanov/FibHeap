namespace FibHeap
{
    public class FibNode
    {
        public FibNode Parent;
        public FibNode Child;
        public FibNode Left;
        public FibNode Right;
        public int Degree;
        public bool WasDeletedChildNode;
        public int NodeKey;

        public FibNode(int nodeKey)
        {
            NodeKey = nodeKey;
            Right = this;
            Left = this;
        }

        public void CutFromCurrentPlaceAndSetAsChild(FibNode node)
        {
            FibUtils.SafeRemoveNode(node);
            node.Parent = this;
            if (Child == null)
            {
                Child = node;
            }
            else
            {
                FibUtils.UnionNodes(Child, node);
                if (Child.NodeKey > node.NodeKey)
                {
                    Child = node;
                }
            }
            Degree += 1 + node.Degree;
        }

        public bool HasNeighbors()
        {
            return this != Right;
        }

        public void RemoveLinkFromParent()
        {
            if (Parent == null) return;
            Parent.Degree--;
            Parent.WasDeletedChildNode = true;
            if (Parent.Child == this)
            {
                Parent.Child = HasNeighbors() ? Right : null;
            }
        }

        public void SetRightNeighbor(FibNode rightNeighbor)
        {
            rightNeighbor.Left = this;
            Right = rightNeighbor;
        }
    }
}