namespace LearningTests.Example
{
    using Xunit;

    public class ValidateHeightStringifyTests
    {
        [Theory]
        [InlineData("mio km", false)]
        [InlineData("100 km", false)]
        [InlineData("100 cm", false)]
        [InlineData("160 cm", true)]
        [InlineData("50 in", false)]
        [InlineData("60 in", true)]
        public void Test_ValidateHeight(string heightString, bool expected)
        {
            var actual = ValidateHeight(heightString);

            Assert.Equal(expected, actual);
        }


        private static bool ValidateHeight(string height)
        {
            if (!int.TryParse(height[..^2], out var value))
                return false;

            var unit = height[^2..];
            return (unit, value) switch
            {
                ("cm", >= 150 and <= 193) => true,
                ("in", >= 59 and  <= 76) => true,
                _ => false
            };
        }
    }
}