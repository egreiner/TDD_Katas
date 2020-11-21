namespace LearningTests.ChristmasTree
{
    using Kata.Services.ChristmasTree;
    using Xunit;

    public class ChristmasTreeTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void Test_Create_ChristmasTree(int height)
        {
            var cut = new ChristmasTree();

            var actual = cut.Draw(height);

            Assert.Equal(height + 1, actual.Count);
            Assert.Contains(" X ", actual[0]);
            Assert.Equal(new string('X', (height - 1) * 2 + 1), actual[height - 1]);
            Assert.Contains(" X ", actual[height]);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void Test_Create_ChristmasTreeWithStar(int height)
        {
            var cut = new ChristmasTree();

            var actual = cut.Draw(height, true);

            Assert.Equal(height + 2, actual.Count);
            Assert.Contains(" * ", actual[0]);
            Assert.Contains(" X ", actual[1]);
            Assert.Equal(new string('X', (height - 1) * 2 + 1), actual[height]);
            Assert.Contains(" X ", actual[height + 1]);
        }
    }
}
