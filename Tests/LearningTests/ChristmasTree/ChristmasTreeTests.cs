namespace LearningTests.ChristmasTree
{
    using Kata.Services.ChristmasTree;
    using Xunit;

    public class ChristmasTreeTests
    {
        [Fact]
        public void Test_Create_ChristmasTree()
        {
            var cut = new ChristmasTree();

            var actual = cut.Draw(5);

            Assert.Equal(6, actual.Count);
        }

        [Fact]
        public void Test_Create_ChristmasTreeWithStar()
        {
            var cut = new ChristmasTree();

            var actual = cut.Draw(5, true);

            Assert.Equal(7, actual.Count);
        }
    }
}
