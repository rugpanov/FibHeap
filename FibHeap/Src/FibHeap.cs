using System;

namespace FibHeap
{
    class FibHeap
    {
        private FibNode myMinFibNode; // узел с минимальным корнем
        private int mySize = 0; // количество узлов

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

            if (newNode.Key < myMinFibNode.Key)
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
            return myMinFibNode.Key;
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
            if (mySize == 0)
            {
                throw new InvalidOperationException();
            }
            if (myMinFibNode.Right == myMinFibNode)
            {
                var key = myMinFibNode.Key;
                myMinFibNode = myMinFibNode.Child;
                //  FibUtils.PrintHeap(myMinFibNode);
                return key;
            }
            var prevMinNode = myMinFibNode;
            if (myMinFibNode.Child != null)
                FibUtils.UnionNodes(myMinFibNode, myMinFibNode.Child);
            FibUtils.SetNeighbors(myMinFibNode.Left, myMinFibNode.Right);
            myMinFibNode =
                myMinFibNode.Right; // перекинем указаиель min на правого сына, далее consolidate() скорректирует min
            Consolidate();
            mySize--;
            // FibUtils.PrintHeap(myMinFibNode);
            return prevMinNode.Key;
        }

        private void Consolidate()
        {
            var degreeArray = new FibNode[mySize];
            degreeArray[myMinFibNode.Degree] = myMinFibNode; // создаем массив и инициализируем его min
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
                    if (conflict.Key < current.Key)
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
                if (myMinFibNode.Key > current.Key) // обновляем минимум, если нужно
                {
                    myMinFibNode = current;
                }
            }
        }

        public void DecreaseKey(FibNode x, int newValue)
        {
            if (newValue > x.Parent.Key)// если после изменения структкра дерева сохранится, то меняем и выходим 
            {
                
                x.Key = newValue;
                return;
            }
            var parent = x.Parent; // иначе вызываем cut и cascadingCut
            Cut(x);
            CascadingCut(x.Parent);
        }

        public void Cut(FibNode node)
        {
            var oldLeft = node.Left;
            var oldRight = node.Right;
            oldRight.Left = oldLeft; // аккуратно удаляем текущую вершину
            oldLeft.Right = oldRight;
            node.Right = node;
            node.Left = node;
            if (node.Parent != null)
            {
                node.Parent.Degree--;
                if (node.Parent.Child == node) // чтобы родитель не потерял ссылку на сыновей проверяем: 
                    node.Parent.Child = node.Right == node ? null : node.Right;
                node.Parent = null;
            }
            FibUtils.UnionNodes(myMinFibNode, node); // вставляем наше поддерево в корневой список
        }

        public void CascadingCut(FibNode x)
        {
            while (x.Mark) // пока у нас помеченые вершины вырезаем их
            {
                Cut(x);
                x = x.Parent;
            }
            x.Mark = true; // последнюю вершину нужно пометить — у нее удаляли ребенка
        }

        public void Delete(FibNode x)
        {
            DecreaseKey(x, int.MinValue);
            Pop();
        }
    }
}