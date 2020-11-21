namespace Kata.Services.RomanNumerals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RomanNumeralsConverter
    {
        private readonly Dictionary<int, int> singleCharsList = new Dictionary<int, int>();

        public int ConvertSubtractionRule(string value)
        {
            var combinedChars = GetEnumNamesWithLength(2);
            var singleChars   = GetEnumNamesWithLength(1);

            var chk1 = this.IterateEnum(combinedChars, value);
            var chk2 = this.IterateEnum(singleChars, chk1.value);
            var sum = chk1.sum + chk2.sum;

            ValidateOrder(value, this.singleCharsList);
            return sum;
        }

        private static IEnumerable<string> GetEnumNamesWithLength(int length) =>
            Enum.GetNames(typeof(RomanNumerals)).Where(x => x.Length == length);

        private (string value, int sum) IterateEnum(IEnumerable<string> enumNames, string value)
        {
            var sum = 0;
            foreach (string theEnum in enumNames)
            {
                while (value.Contains(theEnum))
                {
                    this.AddToList(value, theEnum);
                    sum += GetEnumAsInt(theEnum);
                    value = ReplaceFirst(value, theEnum, "_");
                }
            }

            return (value, sum);
        }

        private void AddToList(string value, string enumAsString) =>
            this.singleCharsList.Add(value.IndexOf(enumAsString), GetEnumAsInt(enumAsString));


        private static void ValidateOrder(string originalValue, Dictionary<int, int> dic)
        {
            var orderByValue = dic.OrderByDescending(x => x.Value).Select(x => x.Value);
            var orderByKey   = dic.OrderBy(x => x.Key).Select(x => x.Value);
            if (orderByKey.SequenceEqual(orderByValue)) return;

            const string message = "Wrong order of roman numerals";
            throw new ArgumentOutOfRangeException(nameof(originalValue), originalValue, message);
        }

        private static string ReplaceFirst(string text, string search, string replace)
        {
            var pos = text.IndexOf(search);
            return pos < 0 
                ? text 
                : text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        private static int GetEnumAsInt(string theEnum) =>
            (int)Enum.Parse(typeof(RomanNumerals), theEnum);
    }
}