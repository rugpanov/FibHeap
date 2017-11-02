using NUnit.Framework;

namespace FibHeap.Tests
{
    [TestFixture]
    public class FibUtilsTests
    {
        [Test]
        public void TestUnionTwoNodes()
        {
            var node1 = new FibNode(200);
            var node2 = new FibNode(201);
            FibUtils.UnionNodes(node1, node2);
            Assert.AreEqual(node1.Left, node2);
            Assert.AreEqual(node2.Left, node1);
            Assert.AreEqual(node1.Right, node2);
            Assert.AreEqual(node2.Right, node1);
        }
        
        [Test]
        public void TestUnionTwoPairOfNodes()
        {
            var node1 = new FibNode(200);
            var node2 = new FibNode(201);
            FibUtils.UnionNodes(node1, node2);
            var node3 = new FibNode(200);
            var node4 = new FibNode(201);
            FibUtils.UnionNodes(node3, node4);
            
            FibUtils.UnionNodes(node2, node3);
            
            Assert.AreEqual(node1.Right, node4);
            Assert.AreEqual(node2.Right, node1);
            Assert.AreEqual(node3.Right, node2);
            Assert.AreEqual(node4.Right, node3);
            Assert.AreEqual(node1.Left, node2);
            Assert.AreEqual(node2.Left, node3);
            Assert.AreEqual(node3.Left, node4);
            Assert.AreEqual(node4.Left, node1);
        }        
        
        [Test]
        public void TestSafeRemoveNodeWithoutParent()
        {
            var node1 = new FibNode(200);
            var node2 = new FibNode(201);
            var node3 = new FibNode(201);
            FibUtils.UnionNodes(node1, node2);
            FibUtils.UnionNodes(node2, node3);

            FibUtils.SafeRemoveNode(node2);
            Assert.AreEqual(node1.Right, node3);
            Assert.AreEqual(node1.Left, node3);
            Assert.AreEqual(node3.Right, node1);
            Assert.AreEqual(node3.Left, node1);
            Assert.AreEqual(node2.Right, node2);
            Assert.AreEqual(node2.Left, node2);
        }    
        
        [Test]
        public void TestSafeRemoveNodeWithParent()
        {
            var childNodeToRemove = new FibNode(200);
            var neighborToRemoveChildNode = new FibNode(201);
            var parentNode = new FibNode(203);
            FibUtils.UnionNodes(childNodeToRemove, neighborToRemoveChildNode);
            parentNode.Child = childNodeToRemove;
            childNodeToRemove.Parent = parentNode;
           
            FibUtils.SafeRemoveNode(childNodeToRemove);
            
            Assert.AreEqual(neighborToRemoveChildNode.Right, neighborToRemoveChildNode);
            Assert.AreEqual(neighborToRemoveChildNode.Left, neighborToRemoveChildNode);
            Assert.AreEqual(parentNode.Child, neighborToRemoveChildNode);
        }  
    }
}