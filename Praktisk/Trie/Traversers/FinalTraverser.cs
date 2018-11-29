using System.Collections.Generic;

namespace Trie.Traversers
{
    internal class FinalTraverser : Traverser
    {
        public FinalTraverser(Node root) : base(root)
        {
        }

        protected override IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> GetNodes(Node node,
            IList<Node> previousNodes, int depth)
        {
            if (node.NodeType == NodeType.Final)
            {
                yield return (node, previousNodes);
            }

            foreach (var child in node)
            {
                var childPreviousNodes = new List<Node>(previousNodes);
                childPreviousNodes.Add(node);

                foreach (var childNodes in GetNodes(child, childPreviousNodes, ++depth))
                {
                    yield return childNodes;
                }
            }
        }
    }
}