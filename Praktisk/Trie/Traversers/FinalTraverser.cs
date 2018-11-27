namespace Trie.Traversers
{
    internal class FinalTraverser : Traverser
    {
        public FinalTraverser(Node root) : base(root, (n, prev) => n.NodeType == NodeType.Final)
        {
        }
    }
}