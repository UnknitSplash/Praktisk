using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Trie.Tests
{
    public static class TestingExtentions
    {
        public static void ShouldBeEqualTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            var actualList = actual.ToList();
            actualList.Sort();
            
            var expectedList = expected.ToList();
            expectedList.Sort();

            actualList.Should().Equal(expectedList, (v1, v2) => v1.Equals(v2));
        }
    }
}