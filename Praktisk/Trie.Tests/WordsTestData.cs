using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Trie.Tests
{
    public class WordsTestData : IEnumerable<object[]>
    {
        private static readonly string[] TestWords50K = File.ReadAllText("TestDataWords50k").Split('\n');

        public IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] {new string[0]},
            new object[] {new[] {"2", "new", "old", "all", "always", "allocate", "new2"}},
            new object[] {TestWords50K}
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}