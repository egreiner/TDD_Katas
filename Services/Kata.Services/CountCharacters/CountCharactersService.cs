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
            var ignoreCase = stringComparison == StringComparison.OrdinalIgnoreCase;

            if (this.dict.ContainsKey(key))
                this.dict[key]++;
            else if (ignoreCase && this.dict.ContainsKey(char.ToLower(key)))
                this.dict[char.ToLower(key)]++;
            else if (ignoreCase && this.dict.ContainsKey(char.ToUpper(key)))
                this.dict[char.ToUpper(key)]++;
            else
                this.dict.Add(key, 1);
        }
    }
}