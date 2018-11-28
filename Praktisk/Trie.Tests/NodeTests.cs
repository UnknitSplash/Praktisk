using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Trie.Tests
{
    public class NodeTests
    {
        public static IEnumerable<object[]> NodeTestData = new List<object[]>
        {
            new object[] {new Node('a', NodeType.Final)},
            new object[] {new Node('a', NodeType.Final), new Node('b', NodeType.Intermediate)}
        };

        [Theory]
        [MemberData(nameof(NodeTestData))]
        public void Test_AddValidNodes_NodesAdded(params object[] nodes)
        {
            //PREPARE
            var childNodes = nodes.Select(n => (Node) n).ToArray();
            var parentNode = new Node('b', NodeType.Final);

            //ACT
            foreach (var childNode in childNodes)
            {
                parentNode.AddNode(childNode);
            }

            //CHECK
            parentNode.Children.ShouldBeEqualTo(childNodes);
        }

        [Fact]
        public void Test_AddEmptyNode_ExceptionThrown()
        {
            //PREPARE
            var parentNode = new Node('b', NodeType.Final);

            //ACT & CHECK
            Assert.Throws<ArgumentNullException>(() => parentNode.AddNode(null));            
        }

        [Theory]
        [MemberData(nameof(NodeTestData))]
        public void Test_RemoveNode_Removed(params object[] nodes)
        {
            //PREPARE
            var childNodes = nodes.Select(n => (Node) n).ToList();
            var parentNode = new Node('b', NodeType.Final);

            foreach (var childNode in childNodes)
            {
                parentNode.AddNode(childNode);
            }
            
            var nodeToRemove = childNodes.First();
            childNodes.Remove(nodeToRemove);
            
            //ACT
            parentNode.RemoveNode(nodeToRemove);
            
            //CHECK
            parentNode.Children.ShouldBeEqualTo(childNodes); 
        }
        
        [Fact]
        public void Test_RemoveEmptyNode_ExceptionThrown()
        {
            //PREPARE
            var parentNode = new Node('b', NodeType.Final);

            //ACT & CHECK
            Assert.Throws<ArgumentNullException>(() => parentNode.RemoveNode(null));            
        }
    }
}