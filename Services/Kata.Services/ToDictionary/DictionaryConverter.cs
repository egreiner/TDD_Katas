namespace Kata.Services.ToDictionary
{
    using System;
    using System.Collections.Generic;

    public class DictionaryConverter
    {
        private const string Separator = "=";


        public Dictionary<string, string> ToDictionary(string dictionaryString) =>
            string.IsNullOrEmpty(dictionaryString?.Trim()) 
                ? new Dictionary<string, string>() 
                : ToDictionary(dictionaryString.Split(';', StringSplitOptions.RemoveEmptyEntries));


        private static Dictionary<string, string> ToDictionary(string[] keyValues)
        {
            var result = new Dictionary<string, string>();
            foreach (var keyValue in keyValues) 
                AddKeyValue(result, keyValue);

            return result;
        }

        private static void AddKeyValue(IDictionary<string, string> result, string keyValue)
        {
            // some of you may find this ridiculous
            // this is called SRP...
            // and IOSP (with SLA)
            // look it up
            ValidateKeyValuePair(keyValue);

            var (key, value) = ExtractKeyValue(keyValue);
            UpdateDictionary(result, key, value);
        }

        private static void UpdateDictionary(IDictionary<string, string> result, string key, string value)
        {
            if (result.ContainsKey(key))
                result[key] = value;
            else
                result.Add(key, value);
        }

        private static (string key, string value) ExtractKeyValue(string keyValuePair)
        {
            var index = keyValuePair.IndexOf(Separator, StringComparison.Ordinal);
            var key   = keyValuePair[..index];
            var value = keyValuePair[(index + 1)..];
            return (key, value);
        }

        private static void ValidateKeyValuePair(string keyValuePair)
        {
            if (!keyValuePair.StartsWith(Separator)) return;

            var message = $"The key-value-pair {keyValuePair} is not allowed!";
            throw new ArgumentOutOfRangeException(nameof(keyValuePair), keyValuePair, message);
        }
    }
}