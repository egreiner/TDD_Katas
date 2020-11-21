namespace LearningTests.RussianPeasantMultiplication
{
    using Kata.Services.RussianPeasantMultiplication;
    using Xunit;

    public class RussianMultiplicationTestsV2
    {
        private readonly RussianPeasantMultiplicationServiceV2 service = 
            new RussianPeasantMultiplicationServiceV2();


        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 1, 3)]
        [InlineData(4, 1, 4)]
        [InlineData(47, 42, 1974)]
        public void Test_result(int a, int b, int expected)
        {
            var actual = this.service.Mul(a,b);

            Assert.Equal(expected, actual);
        }
    }
}





