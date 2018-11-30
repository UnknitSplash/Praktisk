using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trie.Traversers;

namespace Trie
{
    public class Trie : ICollection<string>
    {
        private Node _root = NewRoot();

        private static Node NewRoot()
        {
            return new Node(' ', NodeType.Root, null);
        }

        private int _wordCount;

        public Trie(IEnumerable<string> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            var items = words as string[];

            for (var i = 0; i < items?.Length; i++)
            {
                AddWord(items[i]);
            }
        }

        private void AddWord(string word)
        {
            var currentNode = _root;
            for (var i = 0; i < word.Length; i++)
            {
                currentNode = currentNode.Children.TryGetValue(word[i], out var existingNode)
                    ? existingNode
                    : currentNode.AddNode(new Node(word[i], NodeType.Intermediate, currentNode));
            }

            if (currentNode.NodeType != NodeType.Final)
            {
                currentNode.NodeType = NodeType.Final;
                _wordCount++;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            var traverser = new FinalTraverser(_root);

            foreach (var node in traverser.Go())
            {
                yield return new Word(true, node.GetPrecursiveNodes().Reverse().Select(n => n.Value));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string item)
        {
            AddWord(item);
        }

        public void Clear()
        {
            _root = NewRoot();
            _wordCount = 0;
        }

        public bool Contains(string item)
        {
            return ContainsItemOfType(item, NodeType.Final);
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

            var traverser = new MatchingTraverser(_root, item.ToCharArray(), Find.First, nodeType);
            return traverser.Go().Any();
        }


        public void CopyTo(string[] array, int arrayIndex)
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
                return true;
            }

            var traverser = new MatchingTraverser(_root, item.ToCharArray(), Find.First, NodeType.Final);

            var nodes =
                traverser.Go().Select(t =>
                {
                    t.NodeType = NodeType.Intermediate;
                    return t.GetPrecursiveNodes().Reverse().ToArray();
                }).SingleOrDefault();

            var nodesCount = nodes?.Length ?? 0;

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