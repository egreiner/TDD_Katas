namespace LearningTests.ToDictionary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Kata.Services.ToDictionary;
    using Xunit;

    public class ToDictionaryTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Test_Create_empty_Dictionary(string dictionaryString)
        {
            var cut = new DictionaryConverter();

            var actual = cut.ToDictionary(dictionaryString);

            Assert.Equal(new Dictionary<string, string>(), actual);
        }

        [Theory]
        // normal cases
        [InlineData("a=1", "1")]
        [InlineData("b=2", "2")]
        [InlineData("Fizz=Buzz", "Buzz")]
        // special cases
        [InlineData("a=", "")]
        [InlineData("test=", "")]
        [InlineData("b==2", "=2")]
        [InlineData("bla===3", "==3")]
        public void Test_Create_Dictionary_with_one_entry(string dictionaryString, string expected)
        {
            var cut = new DictionaryConverter();

            var dic = cut.ToDictionary(dictionaryString);

            var length = dictionaryString.IndexOf("=", StringComparison.Ordinal);
            var key = dictionaryString.Substring(0, length);

            var actual = dic[key];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("a=1;b=2", "1", "2")]
        [InlineData("a=;test=;b==2", "", "", "=2")]
        // ignore empty
        [InlineData("a=1;;b=2", "1", "2")]
        // update same entries
        [InlineData("a=1;a=2", "2")]
        public void Test_Create_Dictionary_with_two_entries(string dictionaryString, params string[] expected)
        {
            // a very flexible test...
            var cut = new DictionaryConverter();

            var dic = cut.ToDictionary(dictionaryString);

            var keys = dic.Keys.ToList();

            Assert.All(keys, key =>
                {
                    var index = keys.IndexOf(key);
                    var actual = dic[key];

                    Assert.Equal(expected[index], actual);
                }
            );
        }

        [Theory]
        [InlineData("=1")]
        public void Test_throw_exception(string dictionaryString)
        {
            var cut = new DictionaryConverter();

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.ToDictionary(dictionaryString));

            Assert.Contains(dictionaryString, actual.Message);
        }
    }
}
