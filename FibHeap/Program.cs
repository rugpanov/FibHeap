using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FibHeap
{
  class Program
  {
    static void Main(string[] args)
    {
      var fh = new FibHeap();
      fh.Push(1);
      fh.Push(2);
      fh.Push(3);
      fh.Push(4);
      fh.Push(5);
      Console.WriteLine(fh.GetMin());
      Console.WriteLine(fh.Pop());
      Console.WriteLine(fh.Pop());
      Thread.Sleep(2000);
    }
  }
}
