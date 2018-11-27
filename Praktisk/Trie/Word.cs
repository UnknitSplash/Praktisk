using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    public class Word
    {
        private readonly char[] _characters;

        public bool IsFinal { get; }
        public IEnumerable<char> Characters => _characters;

        public Word(bool isFinal, IEnumerable<char> characters)
        {
            IsFinal = isFinal;
            _characters = characters.ToArray();
        }

        public static implicit operator string(Word word)
        {
            return word.ToString();
        }

        public override string ToString()
        {
            return new string(_characters);
        }
    }
}