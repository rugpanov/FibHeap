using System;

namespace FibHeap
{
    public class FibHeap
    {
        private FibNode myMyMinFibNode; // узел с минимальным корнем
        private int myMySize; // количество узлов

        public void Push(int key)
        {
            var newNode = new FibNode(key);
            if (myMySize == 0)
            {
                InitMinNode(newNode);
            }
            else
            {
                PlaceNewNodeOnMinRight(newNode);
            }

            if (newNode.NodeKey < myMyMinFibNode.NodeKey)
            {
                myMyMinFibNode = newNode;
            }
            myMySize++;
        }

        private void InitMinNode(FibNode newFibNode)
        {
            myMyMinFibNode = newFibNode;
            newFibNode.SetRightNeighbor(newFibNode);
        }

        private void PlaceNewNodeOnMinRight(FibNode newFibNode)
        {
            var prevRightNode = myMyMinFibNode.Right;
            myMyMinFibNode.SetRightNeighbor(newFibNode);
            newFibNode.SetRightNeighbor(prevRightNode);
        }

        public int GetMin()
        {
            if (myMyMinFibNode == null)
            {
                throw new IndexOutOfRangeException("There is no elements in the heap.");
            }
            return myMyMinFibNode.NodeKey;
        }        
        
        public FibNode GetMinNode()
        {
            return myMyMinFibNode;
        }

        public void Merge(FibHeap second)
        {
            if (second.myMySize == 0)
            {
                return;
            }
            if (myMySize == 0)
            {
                myMyMinFibNode = second.myMyMinFibNode;
                myMySize = second.myMySize;
            }
            else
            {
                FibUtils.UnionNodes(myMyMinFibNode, second.myMyMinFibNode);
                myMySize += second.myMySize;
            }
            if (myMyMinFibNode == null || GetMin() > second.GetMin())
            {
                myMyMinFibNode = second.myMyMinFibNode;
            }
        }

        public int Pop()
        {
            int extractedKey;
            if (myMySize == 0)
            {
                throw new InvalidOperationException();
            }
            if (!myMyMinFibNode.HasNeighbors())
            {
                extractedKey = myMyMinFibNode.NodeKey;
                myMyMinFibNode = myMyMinFibNode.Child;
                return extractedKey;
            }
            if (myMyMinFibNode.Child != null)
            {
                FibUtils.UnionNodes(myMyMinFibNode, myMyMinFibNode.Child);
            }
            myMyMinFibNode.Left.SetRightNeighbor(myMyMinFibNode.Right);
            extractedKey = myMyMinFibNode.NodeKey;
            myMyMinFibNode =
                myMyMinFibNode.Right; // перекинем указатель min на правого сына, далее consolidate() скорректирует min
            Consolidate();
            myMySize--;
            return extractedKey;
        }

        private void Consolidate()
        {
            var degreeArray = new FibNode[myMySize];
            degreeArray[myMyMinFibNode.Degree] = myMyMinFibNode;
            var current = myMyMinFibNode.Right;
            current.Parent = null;
            while (degreeArray[current.Degree] != current) // пока элементы массива меняются
            {
                if (degreeArray[current.Degree] == null) // если ячейка пустая, то положим в нее текущий элемент
                {
                    degreeArray[current.Degree] = current;
                    current = current.Right;
                }
                else // иначе подвесим к меньшему из текущего корня и того, который лежит в ячейке другой
                {
                    var conflict = degreeArray[current.Degree];
                    FibNode addTo, adding;
                    if (conflict.NodeKey < current.NodeKey)
                    {
                        addTo = conflict;
                        adding = current;
                    }
                    else
                    {
                        addTo = current;
                        adding = conflict;
                    }
                    addTo.CutFromCurrentPlaceAndSetAsChild(adding);
                    degreeArray[adding.Degree] = null;
                    current = addTo;
                }
                if (myMyMinFibNode.NodeKey > current.NodeKey) // обновляем минимум, если нужно
                {
                    myMyMinFibNode = current;
                }
            }
        }

        public void Delete(FibNode node)
        {
            DecreaseKey(node, int.MinValue);
            Pop();
        }
        
        private void DecreaseKey(FibNode node, int newValue)
        {
            if (node.Parent == null || newValue > node.Parent.NodeKey)// если после изменения структура дерева сохранится, то меняем и выходим 
            {
                node.NodeKey = newValue;
                return;
            }
            Cut(node);
            CascadingCut(node.Parent);
            node.Parent = null;
        }
        
        private void Cut(FibNode node)
        {
            FibUtils.SafeRemoveNode(node);
            FibUtils.UnionNodes(myMyMinFibNode, node);
            node.WasDeletedChildNode = false;
        }

        private void CascadingCut(FibNode node)
        {
            while (node.WasDeletedChildNode)
            {
                Cut(node);
                if (node.Parent != null)
                {
                    node = node.Parent;
                }
                else
                {
                    break;
                }
            }
            //Мы пометим в FibNode#RemoveLinkFromParent
            //node.Mark = true; 
        }
        
        private static void Main(string[] args)
        {
            throw new InvalidOperationException();
        }
    }
}