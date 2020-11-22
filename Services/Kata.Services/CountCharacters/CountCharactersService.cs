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
                this.dict[key] += 1;
            else
                this.dict.Add(key, 1);
        }

        private void UpdateDictionaryOrdinalIgnoreCase(char key)
        {
            if (this.dict.ContainsKey(key))
                this.dict[key] += 1;
            else if (this.dict.ContainsKey(key.ToString().ToLower()[0]))
                this.dict[key.ToString().ToLower()[0]] += 1;
            else if (this.dict.ContainsKey(key.ToString().ToUpper()[0]))
                this.dict[key.ToString().ToUpper()[0]] += 1;
            else
                this.dict.Add(key, 1);
        }
    }
}