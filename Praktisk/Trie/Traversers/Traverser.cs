using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal class Traverser
    {
        private readonly Node _root;
        private readonly Func<Node, IEnumerable<Node>, bool>[] _predicates;

        public Traverser(Node root)
        {
            _root = root;
            _predicates = new Func<Node, IEnumerable<Node>, bool>[] {(n, prev) => true};
        }
        
        public Traverser(Node root, params Func<Node, IEnumerable<Node>, bool>[] predicates) : this(root)
        {
            _predicates = predicates;
        }

        public IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> Go()
        {
            foreach (var rootChildNode in _root)
            {
                foreach (var node in GetNodes(rootChildNode, new List<Node>()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> GetNodes(Node node,
            IList<Node> previousNodes)
        {
            foreach (var child in node)
            {
                var childPreviousNodes = new List<Node>(previousNodes);
                childPreviousNodes.Add(node);

                foreach (var childNodes in GetNodes(child, childPreviousNodes))
                {
                     yield return childNodes;
                }
            }

            if(_predicates.All(p => p(node, previousNodes))) yield return (node, previousNodes);
        }
    }
}