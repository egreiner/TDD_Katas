namespace LearningTests.Extensions
{
    using Kata.Services.Extensions;
    using Xunit;

    public class StringExtensionTests
    {
        [Theory]
        [InlineData("Ä", "Ae")]
        [InlineData("Ö", "Oe")]
        [InlineData("Ü", "Ue")]
        [InlineData("ä", "ae")]
        [InlineData("ö", "oe")]
        [InlineData("ü", "ue")]
        [InlineData("ß", "ss")]
        public void Test_ReplaceUmlauts(string value, string expected)
        {
            var actual = value.ReplaceUmlauts();

            Assert.Equal(expected, actual);
        }
    }
}
