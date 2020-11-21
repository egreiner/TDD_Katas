// ReSharper disable StringLiteralTypo
namespace LearningTests.RomanNumerals
{
    using System;
    using Kata.Services.RomanNumerals;
    using Xunit;

    public class RomanNumeralsSubtractionRuleTests
    {
        [Theory]
        [InlineData("IV", 4)]
        [InlineData("IX", 9)]
        [InlineData("CIX", 109)]
        [InlineData("CIXI", 110)]   // shouldn't work
        public void Test_Combined_Characters(string value, int expected)
        {
            var cut = new RomanNumeralsConverter();

            var actual = cut.ConvertSubtractionRule(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("IXC")]
        public void Should_raise_error_because_of_wrong_order(string value)
        {
            var cut = new RomanNumeralsConverter();

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.ConvertSubtractionRule(value));

            Assert.Contains("Wrong order", actual.Message);
            Assert.Equal("IXC", actual.ActualValue?.ToString());
        }
    }
}