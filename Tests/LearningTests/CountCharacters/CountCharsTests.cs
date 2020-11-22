namespace LearningTests.CountCharacters
{
    using System;
    using System.Collections.Generic;
    using Kata.Services.CountCharacters;
    using Xunit;

    public class CountCharsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_Create_empty_Dictionary(string text)
        {
            var cut = new CountCharactersService();

            var actual = cut.CountCharacters(text);

            Assert.Equal(new Dictionary<char, int>(), actual);
        }

        [Theory]
        [InlineData("A", 'A', 1)]
        [InlineData("AA", 'A', 2)]
        [InlineData("Aa", 'A', 1)]
        [InlineData("Aa", 'a', 1)]
        [InlineData("Ba", 'A', 0)]
        [InlineData(" ", '_', 1)]
        [InlineData("_ ", '_', 2)]
        [InlineData("HelLo wOrld", 'H', 1)]
        [InlineData("HelLo wOrld", 'e', 1)]
        [InlineData("HelLo wOrld", 'l', 2)]
        [InlineData("HelLo wOrld", 'L', 1)]
        [InlineData("HelLo wOrld", 'o', 1)]
        [InlineData("HelLo wOrld", 'w', 1)]
        [InlineData("HelLo wOrld", 'O', 1)]
        [InlineData("HelLo wOrld", 'r', 1)]
        [InlineData("HelLo wOrld", 'd', 1)]
        [InlineData("HelLo wOrld", '_', 1)]
        public void Test_count_not_ignoring_case(string text, char search, int expected)
        {
            var cut = new CountCharactersService();

            var dict = cut.CountCharacters(text);
            var actual = dict.GetValueOrDefault(search);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("AA", 'A', 2)]
        [InlineData("Aa", 'A', 2)]
        [InlineData("Aa", 'a', 0)]  // that's ok but not logical
        [InlineData("HelLo wOrld", 'H', 1)]
        [InlineData("HelLo wOrld", 'h', 0)]
        [InlineData("HelLo wOrld", 'e', 1)]
        [InlineData("HelLo wOrld", 'l', 3)]
        [InlineData("HelLo wOrld", 'o', 2)]
        [InlineData("HelLo wOrld", 'w', 1)]
        [InlineData("HelLo wOrld", 'r', 1)]
        [InlineData("HelLo wOrld", 'd', 1)]
        [InlineData("HelLo wOrld", '_', 1)]
        public void Test_count_ignoring_case(string text, char search, int expected)
        {
            var cut = new CountCharactersService();

            var dict = cut.CountCharacters(text, StringComparison.OrdinalIgnoreCase);
            var actual = dict.GetValueOrDefault(search);

            Assert.Equal(expected, actual);
        }
    }
}
