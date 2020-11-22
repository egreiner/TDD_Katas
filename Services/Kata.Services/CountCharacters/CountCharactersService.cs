namespace Kata.Services.CountCharacters
{
    using System.Collections.Generic;
    using System.Linq;

    public class CountCharactersService
    {
        private readonly Dictionary<char, int> dict = new Dictionary<char, int>();
        private bool ignoreCase;


        // sorry but i like this version most, even the metrics are worse...
        // i like it more because DRY...
        public Dictionary<char, int> CountCharacters(string text, bool ignoreCase = false)
        {
            this.ignoreCase = ignoreCase;

            text?.Replace(" ", "_").ToCharArray().ToList()
                .ForEach(this.UpdateDictionary);

            return this.dict;
        }

        private void UpdateDictionary(char key)
        {
            if (this.dict.ContainsKey(key))
                this.dict[key]++;
            else if (this.ignoreCase && this.dict.ContainsKey(char.ToLower(key)))
                this.dict[char.ToLower(key)]++;
            else if (this.ignoreCase && this.dict.ContainsKey(char.ToUpper(key)))
                this.dict[char.ToUpper(key)]++;
            else
                this.dict.Add(key, 1);
        }
    }
}