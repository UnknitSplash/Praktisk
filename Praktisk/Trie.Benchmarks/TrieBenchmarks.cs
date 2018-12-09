using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Trie.Benchmarks
{
    public class TrieBenchmarks
    {
        private static string[] testWords = File.ReadAllText("TestDataWords460k").Split('\n').Distinct().ToArray();
        private Trie<string> _trie;

        [Params("aal", "Barabb", "camis", "catechi", "lean-", "niob", "nona", "rer", "resalutation", "sharpene",
            "siliqua", "symp", "strivin", "turquo", "umpte", "Vern", "woodge", "wreath", "wreathwort")]
        public string Prefix;

        [GlobalSetup]
        public void Setup()
        {
            _trie = new Trie<string>(testWords.Select(w => (w, w)));
        }

        [Benchmark]
        public void TrieContainsPrefix()
        {
            _trie.ContainsPrefix(Prefix);
        }

        [Benchmark]
        public void TrieGetByPrefix()
        {
            _trie.GetByPrefix(Prefix);
        }
    }
}