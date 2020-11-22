namespace Kata.Services.CountCharacters
{
    using System.Collections.Generic;
    using System.Linq;

    public class CountCharactersService
    {
        private readonly Dictionary<char, int> dict = new Dictionary<char, int>();


        public Dictionary<char, int> CountCharacters(string text)
        {
            text?.Replace(" ", "_").ToCharArray().ToList()
                .ForEach(this.UpdateDictionary);

            return this.dict;
        }

        private void UpdateDictionary(char key)
        {
            if (this.dict.ContainsKey(key))
                this.dict[key] += 1;
            else
                this.dict.Add(key, 1);
        }
    }
}