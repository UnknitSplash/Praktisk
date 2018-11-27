using System.Linq;

namespace Trie.Traversers
{
    internal class FinalMatchingTraverser : Traverser
    {
        public FinalMatchingTraverser(Node root, string item) : base(root, (n, prev) => n.NodeType == NodeType.Final,
            (n, prev) => new Word(false, prev.Select(pn => pn.Value).Append(n.Value)) == item)
        {
        }
    }
}