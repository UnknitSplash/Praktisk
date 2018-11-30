using System.Collections.Generic;

namespace Trie.Traversers
{
    internal class FinalTraverser : Traverser
    {
        public FinalTraverser(Node root) : base(root)
        {
        }

        protected override IEnumerable<Node> GetNodes(Node node, int depth)
        {
            if (node.NodeType == NodeType.Final)
            {
                yield return node;
            }

            foreach (var child in node)
            {
                foreach (var childNodes in GetNodes(child, ++depth))
                {
                    yield return childNodes;
                }
            }
        }
    }
}