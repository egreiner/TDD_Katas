namespace Kata.Services.CountCharacters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountCharactersService
    {
        private readonly Dictionary<char, int> dict = new Dictionary<char, int>();


        public Dictionary<char, int> CountCharacters(string text, StringComparison stringComparison = StringComparison.Ordinal)
        {
            text?.Replace(" ", "_").ToCharArray().ToList()
                .ForEach(c => this.UpdateDictionary(c, stringComparison));

            return this.dict;
        }

        private void UpdateDictionary(char key, StringComparison stringComparison)
        {
            if (stringComparison == StringComparison.OrdinalIgnoreCase)
                this.UpdateDictionaryOrdinalIgnoreCase(key);
            else
                this.UpdateDictionaryOrdinal(key);
        }


        private void UpdateDictionaryOrdinal(char key)
        {
            if (this.dict.ContainsKey(key))
                this.dict[key]++;
            else
                this.dict.Add(key, 1);
        }

        // not a beauty but it works as expected...
        // just leave it this way, it expresses all what is needed to know
        private void UpdateDictionaryOrdinalIgnoreCase(char key)
        {
            if (this.dict.ContainsKey(key))
                this.dict[key]++;
            else if (this.dict.ContainsKey(char.ToLower(key)))
                this.dict[char.ToLower(key)]++;
            else if (this.dict.ContainsKey(char.ToUpper(key)))
                this.dict[char.ToUpper(key)]++;
            else
                this.dict.Add(key, 1);
        }
    }
}