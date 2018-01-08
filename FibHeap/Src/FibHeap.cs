using System;

namespace FibHeap
{
    public class FibHeap
    {
        private MyLinkedList topList = new MyLinkedList();
        public int MySize = 0;

        private MyLinkedList getTopList()
        {
            return topList;
        }

        public void Push(int key)
        {
            var newNode = new FibNode(key);
            if (MySize == 0)
            {
                topList.AddLast(newNode);
            }
            else
            {
                if (topList.First.Value.NodeKey > newNode.NodeKey)
                {
                    topList.AddFirst(newNode);
                }
                else
                {
                    topList.AddLast(newNode);
                }
            }
            MySize++;
        }

        public int GetMin()
        {
            if (MySize == 0)
            {
                throw new IndexOutOfRangeException("There is no elements in the heap.");
            }
            return topList.First.Value.NodeKey;
        }

        public FibNode GetMinNode()
        {
            if (topList.Count == 0)
            {
                return null;
            }
            return topList.First.Value;
        }

        public int Pop()
        {
            if (MySize == 0)
            {
                throw new IndexOutOfRangeException("There is no elements in the heap.");
            }
            var extractedKey = topList.First.Value.NodeKey;
            topList.Concat(topList.First.Value.Children);
            topList.RemoveFirst();
            MySize--;
            Consolidate();
            return extractedKey;
        }

        public void Merge(FibHeap anotherHeap)
        {
            var anotherTopList = anotherHeap.getTopList();
            if (anotherHeap.GetMin() > GetMin())
            {
                topList.Concat(anotherTopList);
            }
            else
            {
                anotherTopList.Concat(topList);
                topList = anotherTopList;
            }
            MySize += anotherHeap.MySize;
        }

        public void DecreaseKey()
        {
            topList.First.Value.NodeKey--;
        }

        private void Consolidate()
        {
            if (topList.First.Value == null)
            {
                return;
            }
            Node min = topList.First;

            var degreeArray = new Node[MySize];
            var current = topList.First;
            while (current != null)
            {
                var node = current.Value;
                var nodeDegree = node.Degree;
                if (node.NodeKey < min.Value.NodeKey)
                {
                    min = current;
                }

                if (degreeArray[nodeDegree] == null)
                {
                    degreeArray[nodeDegree] = current;
                    current = current.Next;
                }
                else if (degreeArray[nodeDegree] == current)
                {
                    current = current.Next;
                }
                else
                {
                    var conflict = degreeArray[nodeDegree];
                    Node addTo, adding;
                    if (conflict.Value.NodeKey <= node.NodeKey)
                    {
                        addTo = conflict;
                        adding = current;
                    }
                    else
                    {
                        addTo = current;
                        adding = conflict;
                    }

                    if (adding == min)
                    {
                        min = addTo;
                    }
                    topList.Remove(adding);
                    addTo.Value.Children.AddLast(adding.Value);
                    addTo.Value.Degree += 1;

                    degreeArray[adding.Value.Degree] = null;
                    current = addTo;
                }
            }

            PlaceMinOnFirst(min);
        }

        private void PlaceMinOnFirst(Node min)
        {
            var minNode = min.Value;
            topList.Remove(min);
            topList.AddFirst(minNode);
        }

        private static void Main(string[] args)
        {
            throw new InvalidOperationException();
        }
    }
}