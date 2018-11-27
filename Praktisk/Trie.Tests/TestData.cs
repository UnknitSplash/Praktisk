using System.Collections;
using System.Collections.Generic;

namespace Trie.Tests
{
    public class TestData : IEnumerable<object[]>
    {
        public IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { new string[0] }
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