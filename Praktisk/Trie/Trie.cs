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
            return new Node(' ', NodeType.Root);
        }

        private int _wordCount;

        public Trie(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                AddWord(word);
            }
        }

        private void AddWord(string word)
        {
            var currentNode = _root;
            foreach (var letter in word)
            {
                currentNode = currentNode.Children.FirstOrDefault(n => n.Value == letter) ??
                              currentNode.AddNode(new Node(letter, NodeType.Intermediate));
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
                yield return new Word(true, node.previousNodes.Select(n => n.Value).Append(node.currentNode.Value));
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
            if (string.IsNullOrEmpty(item))
            {
                return false;
            }

            var traverser = new FinalMatchingTraverser(_root, item);
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
            var traverser = new FinalMatchingTraverser(_root, item);

            var nodes =
                traverser.Go().Select(t =>
                {
                    t.currentNode.NodeType = NodeType.Intermediate;
                    return t.previousNodes.Append(t.currentNode).ToArray();
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