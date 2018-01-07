using System;
using NUnit.Framework;

namespace FibHeap.Tests
{
    [TestFixture]
    public class FibHeapTests
    {
        [Test]
        public void TestConstructor()
        {
            var fh = new FibHeap();
            Assert.IsNull(fh.GetMinNode());
        }

        [Test]
        public void TestPushOne()
        {
            var fh = new FibHeap();
            fh.Push(1);
            Assert.AreEqual(fh.GetMin(), 1);
            Assert.AreEqual(fh.GetMinNode().Degree, 0);
        }

        [Test]
        public void TestPushTwo()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.GetMin(), 2);
            Assert.AreEqual(fh.GetMinNode().Degree, 0);
        }

        [Test]
        public void TestGetMin()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.GetMin(), 2);
        }

        [Test]
        public void TestGetMinAfterPopMin()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            fh.Pop();
            Assert.AreEqual(fh.GetMin(), 4);
        }

        [Test]
        public void TestPopElementWithNeighbors()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.Pop(), 2);
            Assert.AreEqual(fh.GetMin(), 4);
        }

        [Test]
        public void TestPopElementWithoutChild()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.Pop(), 2);
        }

        [Test]
        public void TestPopElementWithChilds()
        {
            var fh = new FibHeap();
            fh.Push(1);
            fh.Push(2);
            fh.Push(3);
            fh.Push(4);
            fh.Push(5);
            fh.Pop();
            Assert.AreNotEqual(fh.GetMinNode().Children, 0);
            Assert.AreEqual(fh.Pop(), 2);
            Assert.AreEqual(fh.GetMin(), 3);
        }

        [Test]
        public void TestDeleteFromTopOfTheHeap()
        {
            var fh = new FibHeap();
            fh.Push(1);
            fh.Push(2);
            fh.Push(3);
            fh.Push(4);
            fh.Push(5);
            fh.Pop();
            Assert.AreEqual(2, fh.GetMin());
            fh.Pop();
            Assert.AreEqual(3, fh.GetMin());
        }

        [Test]
        public void TestDeleteNotFromTopOfTheHeap()
        {
            var fh = new FibHeap();
            fh.Push(1);
            fh.Push(2);
            fh.Push(3);
            fh.Push(4);
            fh.Push(5);
            fh.Pop();
            Assert.AreNotEqual(fh.GetMinNode().Children, 0);
        }
    }
}