#pragma warning disable S1172 // Unused method parameters should be removed
namespace LearningTests.Example
{
    using Xunit;

    public class ValidateHeightTypifyTests
    {
        [Theory]
        [InlineData("mio km", false)]
        [InlineData("100 km", false)]
        [InlineData("100 cm", false)]
        [InlineData("160 cm", true)]
        [InlineData("50 in", false)]
        [InlineData("60 in", true)]
        public void Test_ValidateHeightV2(string heightString, bool expected)
        {
            var cut = HeightType.TryParse(heightString);
            var actual = ValidateHeight(cut);

            Assert.Equal(expected, actual);
        }
        
        private static bool ValidateHeight(HeightType heightType) =>
            (heightType?.Unit, heightType?.Value) switch
            {
                ("cm", >= 150 and <= 193) => true,
                ("in", >= 59  and <= 76)  => true,
                _ => false
            };

        // C# 8.0 SemanticType
        public sealed class HeightType
        {
            private HeightType(float value, string unit)
            {
                this.Unit = unit;
                this.Value = value;
            }

            public float Value { get; }
            public string Unit { get; }

            public static HeightType TryParse(string height) =>
                int.TryParse(height[..^2], out var value)
                    ? new HeightType(value, height[^2..])
                    : null;
        }
    }
}