using System.Collections.Generic;

namespace Trie.Traversers
{
    internal class FinalTraverser : Traverser
    {
        private readonly NodeType _nodeType;

        public FinalTraverser(Node root, NodeType nodeType) : base(root)
        {
            _nodeType = nodeType;
        }

        public override IEnumerable<Node> GetNodes()
        {
            foreach (var rootChildNode in _root)
            {
                foreach (var node in GetChildNodes(rootChildNode))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<Node> GetChildNodes(Node node)
        {
            if (node.NodeType == (node.NodeType & _nodeType))
            {
                yield return node;
            }

            foreach (var child in node)
            {
                foreach (var childNodes in GetChildNodes(child))
                {
                    yield return childNodes;
                }
            }
        }
    }
}