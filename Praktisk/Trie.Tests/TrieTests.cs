using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Trie.Tests
{
    public class TrieTests
    {
        [Fact]
        public void Test_TrieEnumeration_Ctor_OutputWordsMatchInput()
        {
            //PREPARE
            var inputWords = new[] {"word", "war", "world"};
            var outputWords = new List<string>();
            
            //ACT
            var trie = new Trie(inputWords);

            outputWords.AddRange(trie);
            //CHECK
            outputWords.Should().BeEquivalentTo(inputWords);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        [InlineData("new", "old", "all", "always", "allocate")]
        public void Test_TrieAddItem_EnumerateAllWords(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie(originalWords);
            var newWord = "alright";
            var outputWords = new List<string>();
            
            //ACT
            trie.Add(newWord);
            outputWords.AddRange(trie.ToList());
            
            //CHECK
            outputWords.Should().BeEquivalentTo(originalWords.Append(newWord));
        }
        
        [Theory]
        [ClassData(typeof(TestData))]
        [InlineData("new", "old", "all", "always", "allocate")]
        public void Test_TrieRemoveItem_EnumerateAllOtherWords(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie(originalWords);
            var outputWords = new List<string>();
            var wordToRemove = originalWords.Take(1).SingleOrDefault();
            
            //ACT
            trie.Remove(wordToRemove);
            outputWords.AddRange(trie.ToList());
            
            //CHECK
            outputWords.Should().BeEquivalentTo(originalWords.Skip(1));
        }
        
        [Theory]
        [ClassData(typeof(TestData))]
        [InlineData("new", "old", "all", "always", "allocate")]
        public void Test_TrieContainsItem_ReturnsTrue(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie(originalWords);
            var wordToFind = originalWords.Take(1).SingleOrDefault();
            
            //ACT
            var contains = trie.Contains(wordToFind);
       
            //CHECK
            contains.Should().Be(originalWords.Length > 0);
        }
        
        [Theory]
        [ClassData(typeof(TestData))]
        [InlineData("new", "old", "all", "always", "allocate")]
        public void Test_TrieClear_EmptyTrie(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie(originalWords);
            
            //ACT
            trie.Clear();
       
            //CHECK
            trie.Count.Should().Be(0);
            trie.ToArray().Should().BeEmpty();
        }
    }
}