using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibHeap
{
    class FibHeap
    {
        private Node myMinNode; // узел с минимальным корнем
        private int mySize = 0; // количество узлов

        public void Push(int key)
        {
            var newNode = new Node(key);
            if (mySize == 0)
            {
                InitMinNode(newNode);
            }
            else
            {
                PlaceNewNodeOnMinRight(newNode);
            }

            if (newNode.myKey < myMinNode.myKey)
            {
                myMinNode = newNode;
            }
            mySize++;
        }

        private void InitMinNode(Node newNode)
        {
            myMinNode = newNode;
            SetNeighbors(newNode, newNode);
        }

        private void SetNeighbors(Node left, Node right)
        {
            right.left = left;
            left.right = right;
        }

        private void PlaceNewNodeOnMinRight(Node newNode)
        {
            var prevRightNode = myMinNode.right;
            SetNeighbors(myMinNode, newNode);
            SetNeighbors(newNode, prevRightNode);
        }

        public int GetMin()
        {
            return myMinNode.myKey;
        }

        public void Merge(FibHeap second)
        {
            if (second.mySize == 0)
            {
                return;
            }
            if (mySize == 0)
            {
                myMinNode = second.myMinNode;
                mySize = second.mySize;
            }
            else
            {
                UnionNodes(myMinNode, second.myMinNode);
                mySize += second.mySize;
            }
            if (myMinNode == null || GetMin() > second.GetMin())
            {
                myMinNode = second.myMinNode;
            }
        }


        private void UnionNodes(Node first, Node second)
        {
            if (first != null && second != null)
            {
                SetNeighbors(first.left, second.right);
                SetNeighbors(second, first);
            }
        }

        public int Pop()
        {
            if (mySize == 0)
            {
                throw new InvalidOperationException();
            }
            if (myMinNode.right == myMinNode.left)
            {
                var key = myMinNode.myKey;
                myMinNode = null;
                return key;
            }
            var prevMinNode = myMinNode;
            UnionNodes(myMinNode, myMinNode.child);
            SetNeighbors(myMinNode.left, myMinNode.right);
            myMinNode = myMinNode.right; // перекинем указаиель min на правого сына, далее consolidate() скорректирует min
            Consolidate();
            mySize--;
            return prevMinNode.myKey;
        }

        private void Consolidate()
        {
            var degreeArray = new Node[mySize];
            degreeArray[myMinNode.Degree] = myMinNode; // создаем массив и инициализируем его min
            var current = myMinNode.right;
            while (degreeArray[current.Degree] != current) // пока элементы массива меняются
            {
                if (degreeArray[current.Degree] == null) // если ячейка пустая, то положим в нее текущий элемент
                {
                    degreeArray[current.Degree] = current;
                    current = current.right;
                }
                else // иначе подвесим к меньшему из текущего корня и того, который лежит в ячейке другой
                {
                    var conflict = degreeArray[current.Degree];
                    Node addTo, adding;
                    if (conflict.myKey < current.myKey)
                    {
                        addTo = conflict;
                        adding = current;
                    }
                    else
                    {
                        addTo = current;
                        adding = conflict;
                    }
                    UnionNodes(addTo.child, adding);
                    adding.parent = addTo;
                    addTo.Degree++;
                    current = addTo;
                }
                if (myMinNode.myKey > current.myKey) // обновляем минимум, если нужно
                {
                    myMinNode = current;
                }
            }
        }
    }

    internal class Node
    {
        public Node parent = null;
        public Node child = null;
        public Node left = null;
        public Node right = null;

        public int Degree = 0;

        public bool mark;        // метка - был ли удален один из дочерних элементов
        public int myKey;          // числовое значение узла

        public Node(int key)
        {
            myKey = key;
        }

    }
}
