using FibHeap;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FibHeapTests
    {
        [Test]
        public void TestConstructor()
        {
            var fh = new FibHeap.FibHeap();
            Assert.IsNull(fh.GetMinNode());
        }        
        
        [Test]
        public void TestPushOne()
        {
            var fh = new FibHeap.FibHeap();
            fh.Push(1);
            Assert.True(fh.GetMin() == 1);
            Assert.False(fh.GetMinNode().HasNeighbors());
            Assert.True(fh.GetMinNode().Degree == 0);
            Assert.IsNull(fh.GetMinNode().Parent);
        }        
        
        [Test]
        public void TestPushTwo()
        {
            var fh = new FibHeap.FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.True(fh.GetMin() == 2);
            Assert.True(fh.GetMinNode().HasNeighbors());
            Assert.True(fh.GetMinNode().Right.NodeKey == 4);
            Assert.True(fh.GetMinNode().Degree == 0);
            Assert.IsNull(fh.GetMinNode().Parent);
        }        
        
        [Test]
        public void TestPopOne()
        {
            var fh = new FibHeap.FibHeap();
            fh.Push(4);
            fh.Push(2);
            Assert.True(fh.Pop() == 2);
            Assert.True(fh.GetMin() == 4);
            Assert.False(fh.GetMinNode().HasNeighbors());
        }      
    }
}