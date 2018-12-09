using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Trie.Tests")]

namespace Trie
{
    internal class Node<T> : IEnumerable<Node<T>>, IComparable<Node<T>>
    {
        private Node<T> _parent;
        private readonly Dictionary<char, Node<T>> _children = new Dictionary<char, Node<T>>();
        internal char Key { get; }
        internal T Value { get; set; }

        internal NodeType NodeType { get; set; }
        internal IReadOnlyDictionary<char, Node<T>> Children => _children;

        public Node(char key, T value, NodeType nodeType, Node<T> parent)
        {
            if (parent == null && nodeType != NodeType.Root)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Key = key;
            Value = value;
            NodeType = nodeType;
            _parent = parent;
        }

        public IEnumerable<Node<T>> GetPrecursiveNodes()
        {
            var current = this;
            yield return current;
            while (current._parent.NodeType != NodeType.Root)
            {
                current = current._parent;
                yield return current;
            }
        }

        public Node<T> AddNode(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.NodeType == NodeType.Root)
            {
                throw new ArgumentException("Can't add a Root Node");
            }

            _children.Add(node.Key, node);
            return node;
        }

        public bool RemoveNode(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return _children.Remove(node.Key);
        }

        public void RemoveParent()
        {
            _parent = null;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(Node<T> other)
        {
            return Equals(other) ? 0 : 1;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node<T>;

            if (node == null)
            {
                return false;
            }

            return NodeType == node.NodeType && Key == node.Key;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return (int)NodeType + Key.GetHashCode() * 17;
        }
    }

    [Flags]
    internal enum NodeType
    {
        Root,
        Intermediate,
        Final
    }
}