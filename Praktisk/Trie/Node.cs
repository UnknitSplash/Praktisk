using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Trie.Tests")]
namespace Trie
{
    internal class Node : IEnumerable<Node>, IComparable<Node>
    {
        private readonly HashSet<Node> _children = new HashSet<Node>();
        internal char Value { get; }

        internal NodeType NodeType { get; set; }
        internal IReadOnlyCollection<Node> Children => _children;

        public Node(char value, NodeType nodeType)
        {
            Value = value;
            NodeType = nodeType;
        }

        public Node AddNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
            _children.Add(node);
            return node;
        }

        public bool RemoveNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
            return _children.Remove(node);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(Node other)
        {
            return Equals(other) ? 0 : 1;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;

            if (node == null)
            {
                return false;
            }

            return NodeType == node.NodeType && Value == node.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() | _children.GetHashCode();
        }
    }

    internal enum NodeType
    {
        Root,
        Intermediate,
        Final
    }
}