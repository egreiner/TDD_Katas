namespace UnitTests.Services.Extensions
{
    using Kata.Services.Extensions;
    using Xunit;

    public class ComparableExtensionTests
    {
        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, 1, 100, true)]
        [InlineData(2, 1, 100, true)]
        [InlineData(100, 1, 100, true)]
        [InlineData(-2, 1, 100, false)]
        public void Test_IsBetween_int(int value, int min, int max, bool expected)
        {
            var actual = value.IsBetween(min, max);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, 1, 100, true)]
        [InlineData(2, 1, 100, true)]
        [InlineData(100, 1, 100, true)]
        [InlineData(-2, 1, 100, false)]
        public void Test_IsBetween_tuple_int(int value, int min, int max, bool expected)
        {
            var actual = value.IsBetween((min, max));

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData("C", "A", "Z", true)]
        [InlineData("a", "A", "Z", false)]
        [InlineData("-", "A", "Z", false)]
        [InlineData("0", "A", "Z", false)]
        public void Test_IsBetween_string(string value, string min, string max, bool expected)
        {
            var actual = value.IsBetween(min, max);

            Assert.Equal(expected, actual);
        }
        
        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData("C", "A", "Z", true)]
        [InlineData("a", "A", "Z", false)]
        [InlineData("-", "A", "Z", false)]
        [InlineData("0", "A", "Z", false)]
        public void Test_IsBetween_tuple_string(string value, string min, string max, bool expected)
        {
            var actual = value.IsBetween((min, max));

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(-1, 1, 1)]
        [InlineData(2, 1, 2)]
        public void Test_LimitToMin_int(int value, int min, int expected)
        {
            var actual = value.LimitToMin(min);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(-1.0, 1.0, 1.0)]
        [InlineData(2.0, 1.0, 2.0)]
        [InlineData(-2.0, 0.9, 0.9)]
        public void Test_LimitToMin_float(float value, float min, float expected)
        {
            var actual = value.LimitToMin(min);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(9, 100, 9)]
        [InlineData(2, 1, 1)]
        public void Test_LimitToMax_int(int value, int max, int expected)
        {
            var actual = value.LimitToMax(max);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(9.0, 100.0, 9.0)]
        [InlineData(2.0, 1.0, 1.0)]
        [InlineData(2.0, 0.9, 0.9)]
        public void Test_LimitToMax_float(float value, float max, float expected)
        {
            var actual = value.LimitToMax(max);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, 1, 100, 1)]
        [InlineData(2, 1, 100, 2)]
        [InlineData(100, 1, 100, 100)]
        [InlineData(-2, 1, 100, 1)]
        [InlineData(200, 1, 100, 100)]
        public void Test_LimitTo_int(int value, int min, int max, int expected)
        {
            var actual = value.LimitTo(min, max);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1.0, 1.0, 100.0, 1.0)]
        [InlineData(2.0, 1.0, 100.0, 2.0)]
        [InlineData(100.0, 1.0, 100.0, 100.0)]
        [InlineData(0.97, 1.0, 100.0, 1.0)]
        [InlineData(-2.0, 1.0, 100.0, 1.0)]
        [InlineData(100.1, 1.0, 99.9, 99.9)]
        public void Test_LimitTo_float(float value, float min, float max, float expected)
        {
            var actual = value.LimitTo(min, max);

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData("1", "1", "100", "1")]
        [InlineData("C", "A", "Z", "C")]
        [InlineData("-", "A", "Z", "A")]
        public void Test_LimitTo_string(string value, string min, string max, string expected)
        {
            var actual = value.LimitTo(min, max);

            Assert.Equal(expected, actual);
        }
    }
}