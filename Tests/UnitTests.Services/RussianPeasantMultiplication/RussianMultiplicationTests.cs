namespace UnitTests.Services.RussianPeasantMultiplication
{
    using Kata.Services.RussianPeasantMultiplication;
    using Xunit;

    public class RussianMultiplicationTests
    {
        private readonly RussianPeasantMultiplicationService service = 
            new();

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(2, 1, 2)]   // Two lines? first shouldn't be counted -> 1*2
        [InlineData(3, 1, 3)]
        [InlineData(4, 1, 4)]   // more than one iteration
        [InlineData(47, 42, 1974)]
        public void Test_RussianPeasantMultiplication(int a, int b, int expected)
        {
            /*
             * 4 * 1 -> 0
             * 2 * 2 -> 0
             * 1 * 4 -> 4 -> 4
             */
            var actual = this.service.Mul(a, b);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(5, 2)]
        public void Test_divide_integer(int value, int expected)
        {
            var actual = value / 2;

            Assert.Equal(expected, actual);
        }
    }
}





