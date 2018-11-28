using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        
        public static IEnumerable<object[]> NodeEqualityTestData = new List<object[]>
        {
            new object[] {new Node('a', NodeType.Final), null, false},
            new object[] {new Node('a', NodeType.Final), new Node('b', NodeType.Intermediate), false},
            new object[] {new Node('a', NodeType.Final), new Node('a', NodeType.Final), true}
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
        
        [Fact]
        public void Test_AddRootNode_ExceptionThrown()
        {
            //PREPARE
            var parentNode = new Node('b', NodeType.Final);

            //ACT & CHECK
            Assert.Throws<ArgumentException>(() => parentNode.AddNode(new Node(' ', NodeType.Root)));
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

        [Theory]
        [InlineData]
        [MemberData(nameof(NodeTestData))]
        public void Test_Enumerate_AllChildrenReturned(params object[] nodes)
        {
            //PREPARE
            var childNodes = nodes.Select(n => (Node) n).ToList();
            var parentNode = new Node('b', NodeType.Final);

            foreach (var childNode in childNodes)
            {
                parentNode.AddNode(childNode);
            }

            //ACT
            var returnedNodes = parentNode.ToList();

            //CHECK
            returnedNodes.ShouldBeEqualTo(childNodes);
        }

        [Fact]
        public void Test_GetChildNodes_IReadOnlyCollectionReturned()
        {
            //PREPARE
            var node = new Node(' ', NodeType.Intermediate);
            node.AddNode(new Node('a', NodeType.Intermediate));

            //ACT
            node.Children.ToList().Clear();

            //CHECK
            node.Children.Should().HaveCount(1);
        }

        [Theory]
        [MemberData(nameof(NodeEqualityTestData))]
        public void Test_CompareNodes_EqualIfValueAndTypeAreTheSame(object node1, object node2, bool areEqual)
        {
            //PREPARE
            var nodeA = (Node) node1;
            var nodeB = (Node) node2;
            //ACT
            var actualEquality = nodeA.Equals(nodeB);

            //CHECK
            actualEquality.Should().Be(areEqual);
        }
        
        [Theory]
        [MemberData(nameof(NodeEqualityTestData))]
        public void Test_CompareHashCodes_EqualIfValueAndTypeAreTheSame(object node1, object node2, bool areEqual)
        {
            //PREPARE
            var nodeA = (Node) node1;
            var nodeB = (Node) node2;
            //ACT
            var actualEquality = nodeA.GetHashCode() == (nodeB?.GetHashCode() ?? 0);

            //CHECK
            actualEquality.Should().Be(areEqual);
        }
    }
}