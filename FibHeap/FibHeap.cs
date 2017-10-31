using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibHeap
{
  class FibHeap
  {
    private Node myMinNode = null; // узел с минимальным корнем
    private int mySize = 0; // количество узлов

    public void Push(int key)
    {
      var newNode = new Node(key);
      if (mySize == 0)
      {
        myMinNode = newNode;
        myMinNode.left = newNode;
        myMinNode.right = newNode;
      }
      else
      {
        var prevRightNode = myMinNode.right;
        myMinNode.right = newNode;
        newNode.left = myMinNode;
        newNode.right = prevRightNode;
        prevRightNode.left = newNode;
      }
      if (newNode.myKey < myMinNode.myKey)
      {
        myMinNode = newNode;
      }
      newNode.parent = null;
      mySize++;
    }

    public int GetMin()
    {
      return myMinNode.myKey;
    }

    public void UnionNodes(Node first, Node second)
    {
      if (first != null && second != null)
      {
        var oldFirstLeft = first.left;
        var oldSecondRight = second.right;
        second.right = first;
        first.left = second;
        oldFirstLeft.right = oldSecondRight;
        oldSecondRight.left = oldFirstLeft;
      }
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
      var oldMinLeft = myMinNode.left;
      var oldMinRight = myMinNode.right;
      oldMinLeft.right = oldMinRight;
      oldMinRight.left = oldMinLeft;
      myMinNode = myMinNode
        .right; // пока что перекинем указаиель min на правого сына, а далее consolidate() скорректирует min в процессе выполнения
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

    private int GetMaxDegree()
    {
      var maxDegree = myMinNode.Degree;
      var current = myMinNode.right;
      while (current != myMinNode)
      {
        if (current.Degree > maxDegree)
        {
          maxDegree = current.Degree;
        }
        current = current.right;
      }
      return maxDegree;
    }
  }

  internal class Node
  {
    public Node parent;
    public Node child;
    public Node left;
    public Node right;

    public int Degree = 0;

    public bool mark;        // метка - был ли удален один из дочерних элементов
    public int myKey;          // числовое значение узла

    public Node(int key)
    {
      myKey = key;
    }
  }
}
