﻿using System;
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
            Assert.False(fh.GetMinNode().HasNeighbors());
            Assert.AreEqual(fh.GetMinNode().Degree, 0);
            Assert.IsNull(fh.GetMinNode().Parent);
        }

        [Test]
        public void TestPushTwo()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.GetMin(), 2);
            Assert.True(fh.GetMinNode().HasNeighbors());
            Assert.AreEqual(fh.GetMinNode().Right.NodeKey, 4);
            Assert.AreEqual(fh.GetMinNode().Degree, 0);
            Assert.IsNull(fh.GetMinNode().Parent);
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
        public void TestPopElementWithoutNeighbors()
        {
            var fh = new FibHeap();
            fh.Push(4);
            Assert.AreEqual(fh.Pop(), 4);
            Assert.Throws<IndexOutOfRangeException>(() => fh.GetMin());
            Assert.IsNull(fh.GetMinNode());
        }

        [Test]
        public void TestPopElementWithNeighbors()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.Pop(), 2);
            Assert.False(fh.GetMinNode().HasNeighbors());
            Assert.AreEqual(fh.GetMin(), 4);
        }

        [Test]
        public void TestPopElementWithoutChild()
        {
            var fh = new FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.AreEqual(fh.Pop(), 2);
            Assert.False(fh.GetMinNode().HasNeighbors());
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
            Assert.IsNotNull(fh.GetMinNode().Child);
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
            Assert.AreEqual(fh.GetMin(), 2);
            fh.Delete(fh.GetMinNode());
            Assert.AreEqual(fh.GetMin(), 3);
            Assert.True(fh.GetMinNode().HasNeighbors());
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
            Assert.IsNotNull(fh.GetMinNode().Child);
            Assert.True(fh.GetMinNode().Child.HasNeighbors());
            Assert.IsNotNull(fh.GetMinNode().Child.Parent);
            var oldChildKey = fh.GetMinNode().Child.NodeKey;
            fh.Delete(fh.GetMinNode().Child);
            Assert.AreNotEqual(fh.GetMinNode().Child.NodeKey, oldChildKey);
        }
    }
}