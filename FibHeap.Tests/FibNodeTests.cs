using NUnit.Framework;

namespace FibHeap.Tests
{
    [TestFixture]
    public class FibNodeTest
    {
        [Test]
        public void TestConstructor()
        {
            var node = new FibNode(239);
            Assert.AreEqual(node.NodeKey, 239);
            Assert.AreEqual(node.Left, node);
            Assert.AreEqual(node.Right, node);
            Assert.AreEqual(node.Degree, 0);
            Assert.AreEqual(node.Parent, null);
            Assert.AreEqual(node.Child, null);
            Assert.AreEqual(node.WasDeletedChildNode, false);
            Assert.False(node.HasNeighbors());
        }

        [Test]
        public void TestSetRightNeighbor()
        {
            var leftNode = new FibNode(239);
            var rightNode = new FibNode(239);
            leftNode.SetRightNeighbor(rightNode);
            Assert.AreEqual(leftNode.Right, rightNode);
            Assert.AreEqual(rightNode.Left, leftNode);
            Assert.AreNotEqual(rightNode.Right, leftNode);
            Assert.AreNotEqual(leftNode.Left, rightNode);
        }

        [Test]
        public void TestHasNeighbors()
        {
            var node = new FibNode(239);
            Assert.False(node.HasNeighbors());
            node.SetRightNeighbor(new FibNode(10));
            Assert.True(node.HasNeighbors());
        }

        [Test]
        public void TestRemoveLinkFromParent()
        {
            var childNodeToRemove = new FibNode(200);
            var neighborToRemoveChildNode = new FibNode(201);
            FibUtils.UnionNodes(childNodeToRemove, neighborToRemoveChildNode);
            var parentNode = new FibNode(203) {Child = childNodeToRemove};
            childNodeToRemove.Parent = parentNode;

            childNodeToRemove.RemoveLinkFromParent();

            Assert.AreEqual(parentNode.Child, neighborToRemoveChildNode);
        }

        [Test]
        public void TestCutFromCurrentPlaceAndSetAsChild()
        {
            var childNodeToRemove = new FibNode(200);
            var neighborToRemoveChildNode = new FibNode(201);
            FibUtils.UnionNodes(childNodeToRemove, neighborToRemoveChildNode);
            var parentNode = new FibNode(203) {Child = childNodeToRemove};
            childNodeToRemove.Parent = parentNode;
            
            var anotherParentNode = new FibNode(204) {Child = null};
            
            anotherParentNode.CutFromCurrentPlaceAndSetAsChild(childNodeToRemove);

            Assert.AreEqual(anotherParentNode.Child, childNodeToRemove);
            Assert.AreEqual(parentNode.Child, neighborToRemoveChildNode);
            Assert.False(neighborToRemoveChildNode.HasNeighbors());
        }
    }
}