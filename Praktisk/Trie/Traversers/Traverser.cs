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

        public IEnumerable<Node> Go()
        {
            foreach (var rootChildNode in _root)
            {
                foreach (var node in GetNodes(rootChildNode, 0))
                {
                    yield return node;
                }
            }
        }

        protected abstract IEnumerable<Node> GetNodes(Node node, int depth);
    }
}