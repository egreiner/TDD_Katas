namespace LearningTests.Extensions
{
    using Kata.Services.Extensions;
    using Xunit;

    public class IntegerExtensionTests
    {
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