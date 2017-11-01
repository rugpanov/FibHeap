using System;

namespace FibHeap
{
    class FibHeap
    {
        private FibNode myMinFibNode; // узел с минимальным корнем
        private int mySize; // количество узлов

        public void Push(int key)
        {
            var newNode = new FibNode(key);
            if (mySize == 0)
            {
                InitMinNode(newNode);
            }
            else
            {
                PlaceNewNodeOnMinRight(newNode);
            }

            if (newNode.NodeKey < myMinFibNode.NodeKey)
            {
                myMinFibNode = newNode;
            }
            mySize++;
        }

        private void InitMinNode(FibNode newFibNode)
        {
            myMinFibNode = newFibNode;
            FibUtils.SetNeighbors(newFibNode, newFibNode);
        }

        private void PlaceNewNodeOnMinRight(FibNode newFibNode)
        {
            var prevRightNode = myMinFibNode.Right;
            FibUtils.SetNeighbors(myMinFibNode, newFibNode);
            FibUtils.SetNeighbors(newFibNode, prevRightNode);
        }

        public int GetMin()
        {
            return myMinFibNode.NodeKey;
        }        
        
        public FibNode GetMinNode()
        {
            return myMinFibNode;
        }

        public void Merge(FibHeap second)
        {
            if (second.mySize == 0)
            {
                return;
            }
            if (mySize == 0)
            {
                myMinFibNode = second.myMinFibNode;
                mySize = second.mySize;
            }
            else
            {
                FibUtils.UnionNodes(myMinFibNode, second.myMinFibNode);
                mySize += second.mySize;
            }
            if (myMinFibNode == null || GetMin() > second.GetMin())
            {
                myMinFibNode = second.myMinFibNode;
            }
        }

        public int Pop()
        {
            int extractedKey;
            if (mySize == 0)
            {
                throw new InvalidOperationException();
            }
            if (!myMinFibNode.HasNeighbors())
            {
                extractedKey = myMinFibNode.NodeKey;
                myMinFibNode = myMinFibNode.Child;
                return extractedKey;
            }
            if (myMinFibNode.Child != null)
            {
                FibUtils.UnionNodes(myMinFibNode, myMinFibNode.Child);
            }
            FibUtils.SetNeighbors(myMinFibNode.Left, myMinFibNode.Right);
            extractedKey = myMinFibNode.NodeKey;
            myMinFibNode =
                myMinFibNode.Right; // перекинем указатель min на правого сына, далее consolidate() скорректирует min
            Consolidate();
            mySize--;
            return extractedKey;
        }

        private void Consolidate()
        {
            var degreeArray = new FibNode[mySize];
            degreeArray[myMinFibNode.Degree] = myMinFibNode;
            var current = myMinFibNode.Right;
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
                    addTo.SetChild(adding);
                    degreeArray[adding.Degree] = null;
                    current = addTo;
                }
                if (myMinFibNode.NodeKey > current.NodeKey) // обновляем минимум, если нужно
                {
                    myMinFibNode = current;
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
        }
        
        private void Cut(FibNode node)
        {
            FibUtils.RemoveNode(node);
            FibUtils.UnionNodes(myMinFibNode, node);
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
    }
}