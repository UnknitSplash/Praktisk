using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal class FinalMatchingTraverser : Traverser
    {
        private readonly char[] _word;
        private readonly Find _find;
        private readonly int _wordLength;

        public FinalMatchingTraverser(Node root, char[] word, Find find) : base(root)
        {
            _word = word;
            _wordLength = word.Length;
            _find = find;
        }

        protected override IEnumerable<(Node currentNode, IEnumerable<Node> previousNodes)> GetNodes(Node node, IList<Node> previousNodes, int depth)
        {
            if (depth < _wordLength && _word[depth] == node.Value)
            {
                if (node.NodeType == NodeType.Final)
                {
                    yield return (node, previousNodes);
                    if (_find == Find.First)
                    {
                        yield break;
                    }
                }

                var children = node.Children as List<Node>;
                for (var i = 0; i < children?.Count; i++)
                {
                    var childPreviousNodes = new List<Node>(previousNodes);
                    childPreviousNodes.Add(node);

                    foreach (var childNodes in GetNodes(children[i], childPreviousNodes, ++depth))
                    {
                        yield return childNodes;
                    }
                }
            }
        }
    }
}