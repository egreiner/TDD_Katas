namespace LearningTests.Extensions
{
    using Kata.Services.Extensions;
    using Xunit;

    public class IntegerExtensionTests
    {
        [Theory]
        [InlineData(1, 100, 1, true)]
        [InlineData(1, 100, 2, true)]
        [InlineData(1, 100, 100, true)]
        [InlineData(1, 100, -2, false)]
        public void Test_IsBetween(int min, int max, int value, bool expected)
        {
            var actual = value.IsBetween(min, max);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 100, 1, 1)]
        [InlineData(1, 100, 2, 2)]
        [InlineData(1, 100, 100, 100)]
        [InlineData(1, 100, -2, 1)]
        [InlineData(1, 100, 200, 100)]
        public void Test_LimitTo(int min, int max, int value, int expected)
        {
            var actual = value.LimitTo(min, max);

            Assert.Equal(expected, actual);
        }



        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void Test_IsEven(int value, bool expected)
        {
            var actual = value.IsEven();

            Assert.Equal(expected, actual);
        }

        // DO NOT SKIP SUCH A SIMPLE TEST!!!
        [Theory]
        [InlineData(2, false)]
        [InlineData(3, true)]
        public void Test_IsOdd(int value, bool expected)
        {
            var actual = value.IsOdd();

            Assert.Equal(expected, actual);
        }
    }
}