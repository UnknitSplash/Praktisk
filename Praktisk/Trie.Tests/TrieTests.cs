using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Trie.Tests
{
    public class TrieTests
    {
        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieEnumeration_Ctor_OutputWordsMatchInput(params string[] originalWords)
        {
            //PREPARE
            var outputWords = new List<string>();

            //ACT
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));

            outputWords.AddRange(trie);
            //CHECK

            outputWords.ShouldBeEqualTo(originalWords.Distinct());
        }

        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieAddItem_EnumerateAllWords(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));
            var newWord = "alrighty";
            var outputWords = new List<string>();

            //ACT
            trie.Add(newWord, newWord);
            outputWords.AddRange(trie.ToList());

            //CHECK
            outputWords.ShouldBeEqualTo(originalWords.Append(newWord).Distinct());
        }

        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieRemoveItem_EnumerateAllOtherWords(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));
            var outputWords = new List<string>();
            var wordToRemove = originalWords.Take(1).SingleOrDefault();

            //ACT
            trie.Remove(wordToRemove);
            outputWords.AddRange(trie.ToList());

            //CHECK
            outputWords.ShouldBeEqualTo(originalWords.Skip(1).Distinct());
        }

        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieContainsItem_ReturnsTrue(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));
            var wordToFind = originalWords.Take(1).SingleOrDefault();

            //ACT
            var contains = trie.Contains(wordToFind);

            //CHECK
            contains.Should().Be(originalWords.Length > 0);
        }

        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieClear_EmptyTrie(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));

            //ACT
            trie.Clear();

            //CHECK
            trie.Count.Should().Be(0);
            trie.ToArray().Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(WordsTestData))]
        public void Test_TrieGetByPrefix_ReturnsFirstFinalWordWithPrefix(params string[] originalWords)
        {
            //PREPARE
            var trie = new Trie<string>(originalWords.Select(w => (w, w)));
            var selectedWord = originalWords.FirstOrDefault(w => w.Length > 3) ?? "";
            var selectedIsEmpty = selectedWord.Length == 0;
            var outputWords = new List<string>();

            //ACT
            var resultWord = trie.GetByPrefix(selectedWord);

            //CHECK
            resultWord.Should().Be(string.IsNullOrWhiteSpace(selectedWord) ? null : selectedWord);
        }
    }
}