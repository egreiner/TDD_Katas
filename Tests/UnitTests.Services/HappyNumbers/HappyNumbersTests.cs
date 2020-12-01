namespace UnitTests.Services.HappyNumbers
{
    using Kata.Services.HappyNumbers;
    using Xunit;

    public class HappyNumbersTests
    {
        [Fact]
        public void Test_10_IsHappyNumber()
        {
            var value = 10;

            var cut = new HappyNumberService();
            var actual = cut.IsHappyNumber(value);

            // 10 -> 1^2 + 0^2 = 1
            Assert.True(actual);
        }

        [Fact]
        public void Test_13_IsHappyNumber()
        {
            var value = 13;

            var cut = new HappyNumberService();
            var actual = cut.IsHappyNumber(value);

            // 13 ->
            // 1 + 9 = 10 -> = 1
            Assert.True(actual);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(10, true)]
        [InlineData(13, true)]
        [InlineData(19, true)]
        [InlineData(11, false)]
        [InlineData(12, false)]
        [InlineData(14, false)]
        [InlineData(15, false)]
        [InlineData(16, false)]
        [InlineData(17, false)]
        [InlineData(18, false)]
        [InlineData(20, false)]
        public void Test_IsHappyNumber(int value, bool expected)
        {
            var cut = new HappyNumberService();
            var actual = cut.IsHappyNumber(value);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_11_IsHappyNumber()
        {
            var value = 11;

            var cut = new HappyNumberService();
            var actual = cut.IsHappyNumber(value);

            // 11 ->
            // 1^2 + 1^2 = 2 ->
            // 2^2 = 4 ->
            // 4^2 = 16 ->
            // 1^2 + 6^2 = 37 ->
            // 9 + 49 = 58 ->
            // 25 + 64 = 89
            // this is a endless story...
            // 64 + 81 = 145
            // how many loops do i need???
            // 1 + 16 + 25 = 42
            // 16 + 4 = 20
            // forget this kata...
            // lets make a test with 1000 iterations...
            // this is what we should learn from this kata...
            // “in the long run”...
            Assert.False(actual);
        }
    }
}
