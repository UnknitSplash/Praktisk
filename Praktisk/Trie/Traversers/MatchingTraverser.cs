using System.Collections.Generic;
using System.Linq;

namespace Trie.Traversers
{
    internal class MatchingTraverser : Traverser
    {
        private readonly char[] _word;
        private readonly NodeType _nodeType;
        private readonly int _wordLength;

        public MatchingTraverser(Node root, char[] word, NodeType nodeType) : base(root)
        {
            _word = word;
            _wordLength = word.Length;
            _nodeType = nodeType;
        }

        public override IEnumerable<Node> GetNodes()
        {
            var depth = 0;
            var node = _root.Children[_word[depth]];

            while (true)
            {
                if (depth < _wordLength && _word[depth] == node.Value)
                {
                    if (node.NodeType == (node.NodeType & _nodeType))
                    {
                        yield return node;
                    }

                    var children = node.Children as Dictionary<char, Node>;
                    depth++;
                    if (children?.Count > 0 && depth < _wordLength && children.TryGetValue(_word[depth], out var nextNode))
                    {
                        node = nextNode;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }
    }
}