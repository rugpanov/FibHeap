using System;

namespace FibHeap
{
    public class MyLinkedList
    {
        private Node mark;
        public int Count;

        public MyLinkedList()
        {
            mark = new Node(mark, mark, null);
            mark.UnsafeNext = mark;
            mark.Prev = mark;
            Count = 0;
        }

        public void AddFirst(FibNode value)
        {
            Node node = new Node(mark, mark.UnsafeNext, value);
            mark.UnsafeNext.Prev = node;
            mark.UnsafeNext = node;
            Count++;
        }

        public void AddLast(FibNode value)
        {
            Node node = new Node(mark.Prev, mark, value);
            mark.Prev.UnsafeNext = node;
            mark.Prev = node;
            Count++;
        }

        public Node First => mark.UnsafeNext;
        public Node Last => mark.Prev;

        public void RemoveFirst()
        {
            mark.UnsafeNext = mark.UnsafeNext.UnsafeNext;
            mark.UnsafeNext.Prev = mark;
            Count--;
        }

        public void RemoveLast()
        {
            mark.Prev = mark.Prev.Prev;
            mark.Prev.UnsafeNext = mark;
            Count--;
        }

        public void Remove(Node node)
        {
            if (Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            var prevNode = node.Prev;
            var nextNode = node.UnsafeNext;
            prevNode.UnsafeNext = nextNode;
            nextNode.Prev = prevNode;
            Count--;
        }

        public void Concat(MyLinkedList list)
        {
            if (list.Count == 0)
            {
                return;
            }

            Count += list.Count;

            Last.UnsafeNext = list.First;
            list.First.Prev = Last;

            mark.Prev = list.Last;
            mark.Prev.UnsafeNext = mark;
        }
    }

    public class Node
    {
        public Node UnsafeNext;
        public Node Prev;
        public FibNode Value;

        public Node(Node prev, Node unsafeNext, FibNode value)
        {
            Prev = prev;
            UnsafeNext = unsafeNext;
            Value = value;
        }

        public Node Next
        {
            get
            {
                if (UnsafeNext.Value == null)
                {
                    return null;
                }

                return UnsafeNext;
            }
        }
    }
}