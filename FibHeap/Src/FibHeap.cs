using System;

namespace FibHeap
{
    public class FibHeap
    {
        private FibNode _myMinFibNode; // узел с минимальным корнем
        private int _mySize; // количество узлов

        public void Push(int key)
        {
            var newNode = new FibNode(key);
            if (_mySize == 0)
            {
                InitMinNode(newNode);
            }
            else
            {
                PlaceNewNodeOnMinRight(newNode);
            }

            if (newNode.NodeKey < _myMinFibNode.NodeKey)
            {
                _myMinFibNode = newNode;
            }
            _mySize++;
        }

        private void InitMinNode(FibNode newFibNode)
        {
            _myMinFibNode = newFibNode;
            FibUtils.SetNeighbors(newFibNode, newFibNode);
        }

        private void PlaceNewNodeOnMinRight(FibNode newFibNode)
        {
            var prevRightNode = _myMinFibNode.Right;
            FibUtils.SetNeighbors(_myMinFibNode, newFibNode);
            FibUtils.SetNeighbors(newFibNode, prevRightNode);
        }

        public int GetMin()
        {
            return _myMinFibNode.NodeKey;
        }        
        
        public FibNode GetMinNode()
        {
            return _myMinFibNode;
        }

        public void Merge(FibHeap second)
        {
            if (second._mySize == 0)
            {
                return;
            }
            if (_mySize == 0)
            {
                _myMinFibNode = second._myMinFibNode;
                _mySize = second._mySize;
            }
            else
            {
                FibUtils.UnionNodes(_myMinFibNode, second._myMinFibNode);
                _mySize += second._mySize;
            }
            if (_myMinFibNode == null || GetMin() > second.GetMin())
            {
                _myMinFibNode = second._myMinFibNode;
            }
        }

        public int Pop()
        {
            int extractedKey;
            if (_mySize == 0)
            {
                throw new InvalidOperationException();
            }
            if (!_myMinFibNode.HasNeighbors())
            {
                extractedKey = _myMinFibNode.NodeKey;
                _myMinFibNode = _myMinFibNode.Child;
                return extractedKey;
            }
            if (_myMinFibNode.Child != null)
            {
                FibUtils.UnionNodes(_myMinFibNode, _myMinFibNode.Child);
            }
            FibUtils.SetNeighbors(_myMinFibNode.Left, _myMinFibNode.Right);
            extractedKey = _myMinFibNode.NodeKey;
            _myMinFibNode =
                _myMinFibNode.Right; // перекинем указатель min на правого сына, далее consolidate() скорректирует min
            Consolidate();
            _mySize--;
            return extractedKey;
        }

        private void Consolidate()
        {
            var degreeArray = new FibNode[_mySize];
            degreeArray[_myMinFibNode.Degree] = _myMinFibNode;
            var current = _myMinFibNode.Right;
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
                if (_myMinFibNode.NodeKey > current.NodeKey) // обновляем минимум, если нужно
                {
                    _myMinFibNode = current;
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
            FibUtils.UnionNodes(_myMinFibNode, node);
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