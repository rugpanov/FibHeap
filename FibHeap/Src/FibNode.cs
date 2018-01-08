namespace FibHeap
{
    public class FibNode
    {
        public int NodeKey;
        public int Degree = 0;
        public MyLinkedList Children = new MyLinkedList();

        public FibNode(int nodeKey)
        {
            NodeKey = nodeKey;
        }
    }
}