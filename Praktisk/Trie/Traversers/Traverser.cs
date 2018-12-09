using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal abstract class Traverser<T>
    {
        protected readonly Node<T> _root;

        public Traverser(Node<T> root)
        {
            _root = root;
        }

        public abstract IEnumerable<Node<T>> GetNodes();
    }
}