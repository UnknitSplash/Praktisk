using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Trie.Tests
{
    public class TestData : IEnumerable<object[]>
    {
        private static readonly string[] TestWords = File.ReadAllText("TestDataWords").Split('\n');

        public IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] {new string[0]},
            new object[] {new[] {"new", "old", "all", "always", "allocate", "new2"}},
            new object[] {TestWords}
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