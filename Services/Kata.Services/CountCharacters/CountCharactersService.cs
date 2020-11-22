namespace Kata.Services.CountCharacters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountCharactersService
    {
        private readonly Dictionary<char, int> dict = new Dictionary<char, int>();

        // sorry but i like this version most, even the metrics are worse...
        public Dictionary<char, int> CountCharacters(string text, StringComparison stringComparison = StringComparison.Ordinal)
        {
            var ignoreCase = stringComparison == StringComparison.OrdinalIgnoreCase;

            text?.Replace(" ", "_").ToCharArray().ToList()
                .ForEach(c => this.UpdateDictionary(c, ignoreCase));

            return this.dict;
        }

        private void UpdateDictionary(char key, bool ignoreCase)
        {
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