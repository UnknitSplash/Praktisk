using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trie.Traversers;

namespace Trie
{
    public class Trie<T> : IEnumerable<T>
    {
        private Node<T> _root = NewRoot();

        private static Node<T> NewRoot()
        {
            return new Node<T>(' ', default, NodeType.Root, null);
        }

        private int _wordCount;

        public Trie(IEnumerable<(string Key, T Value)> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            var items = values.ToArray();

            for (var i = 0; i < items.Length; i++)
            {
                AddWord(items[i].Key, items[i].Value);
            }
        }

        private void AddWord(string key, T value)
        {
            if (key.Length == 0)
            {
                throw new ArgumentException("Can't add an empty key.");
            }

            var currentNode = _root;
            for (var i = 0; i < key.Length; i++)
            {
                currentNode = currentNode.Children.TryGetValue(key[i], out var existingNode)
                    ? existingNode
                    : currentNode.AddNode(new Node<T>(key[i], default, NodeType.Intermediate, currentNode));
            }

            if (currentNode.NodeType != NodeType.Final)
            {
                currentNode.NodeType = NodeType.Final;
                currentNode.Value = value;
                _wordCount++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var traverser = new FinalTraverser<T>(_root, NodeType.Final);

            foreach (var node in traverser.GetNodes())
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string key, T value)
        {
            AddWord(key, value);
        }

        public void Clear()
        {
            _root = NewRoot();
            _wordCount = 0;
        }

        public bool Contains(string key)
        {
            return ContainsItemOfType(key, NodeType.Final);
        }

        public bool ContainsPrefix(string item)
        {
            return ContainsItemOfType(item, NodeType.Intermediate | NodeType.Final);
        }

        private bool ContainsItemOfType(string item, NodeType nodeType)
        {
            if (string.IsNullOrEmpty(item))
            {
                return false;
            }

            var charArray = item.ToCharArray();
            var traverser = new MatchingTraverser<T>(_root, charArray, nodeType);
            return traverser.GetNodes().Any();
        }

        public T GetByPrefix(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return default;
            }

            var traverser = new MatchingTraverser<T>(_root, item.ToCharArray(), NodeType.Final);
            var node = traverser.GetNodes().FirstOrDefault();
            return node == null ? default : node.Value;
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            using (var enumerator = GetEnumerator())
            {
                for (var i = arrayIndex; i < array.Length; i++)
                {
                    if (!enumerator.MoveNext()) break;
                    array[i] = enumerator.Current;
                }
            }
        }

        public bool Remove(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return false;
            }

            var traverser = new MatchingTraverser<T>(_root, item.ToCharArray(), NodeType.Final);

            var node =
                traverser.GetNodes().FirstOrDefault();

            if (node == null)
            {
                return false;
            }

            node.NodeType = NodeType.Intermediate;
            var nodes = node.GetPrecursiveNodes().Reverse().ToArray();


            var nodesCount = nodes.Length;
            if (nodesCount == 0)
            {
                return false;
            }

            for (var i = nodesCount - 1; i >= 0; i--)
            {
                var currentNode = nodes?[i];

                if (currentNode?.NodeType != NodeType.Intermediate)
                {
                    break;
                }

                if (currentNode.Children.Count == 0)
                {
                    var parentNode = i == 0 ? _root : nodes[i - 1];
                    parentNode.RemoveNode(nodes[i]);
                }
            }

            _wordCount--;

            return true;
        }

        public int Count => _wordCount;
        public bool IsReadOnly => false;
    }
}