namespace UnitTests.Services.RomanNumerals
{
    using Kata.Services.RomanNumerals;
    using Xunit;

    public class RomanNumeralsSimpleTests
    {
        [Theory]
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        public void Test_Simple_Conversions(string value, int expected)
        {
            var cut = new RomanNumeralsConverter();

            var actual = cut.ConvertSubtractionRule(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("II", 2)]
        [InlineData("XX", 20)]
        [InlineData("VI", 6)]
        public void Test_Combined_Characters(string value, int expected)
        {
            var cut = new RomanNumeralsConverter();

            var actual = cut.ConvertSubtractionRule(value);

            Assert.Equal(expected, actual);
        }
    }
}