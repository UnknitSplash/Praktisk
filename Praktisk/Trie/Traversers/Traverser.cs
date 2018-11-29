using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal abstract class Traverser
    {
        private readonly Node _root;

        public Traverser(Node root)
        {
            _root = root;
        }

        public IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> Go()
        {
            foreach (var rootChildNode in _root)
            {
                foreach (var node in GetNodes(rootChildNode, new List<Node>(), 0))
                {
                    yield return node;
                }
            }
        }

        protected abstract IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> GetNodes(Node node,
            IList<Node> previousNodes, int depth);

    }
}