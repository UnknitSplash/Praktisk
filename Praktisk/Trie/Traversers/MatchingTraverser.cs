using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal class MatchingTraverser : Traverser
    {
        private readonly char[] _word;
        private readonly Find _find;
        private readonly NodeType _nodeType;
        private readonly int _wordLength;

        public MatchingTraverser(Node root, char[] word, Find find, NodeType nodeType) : base(root)
        {
            _word = word;
            _wordLength = word.Length;
            _find = find;
            _nodeType = nodeType;
        }

        protected override IEnumerable<Node> GetNodes(Node node, int depth)
        {
            if (depth < _wordLength && _word[depth] == node.Value)
            {
                if (node.NodeType == (node.NodeType & _nodeType))
                {
                    yield return node;
                    if (_find == Find.First)
                    {
                        yield break;
                    }
                }

                var children = node.Children as List<Node>;
                for (var i = 0; i < children?.Count; i++)
                {
                    foreach (var childNodes in GetNodes(children[i], ++depth))
                    {
                        yield return childNodes;
                    }
                }
            }
        }
    }
}