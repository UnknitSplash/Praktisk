using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal abstract class Traverser
    {
        protected readonly Node _root;

        public Traverser(Node root)
        {
            _root = root;
        }

        public abstract IEnumerable<Node> GetNodes();
    }
}