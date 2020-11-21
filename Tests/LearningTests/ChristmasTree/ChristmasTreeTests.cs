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
            Assert.Contains(" X ", actual[0]);
            Assert.Equal("XXXXXXXXX", actual[4]);
            Assert.Contains(" X ", actual[5]);
        }

        [Fact]
        public void Test_Create_ChristmasTreeWithStar()
        {
            var cut = new ChristmasTree();

            var actual = cut.Draw(5, true);

            Assert.Equal(7, actual.Count);
            Assert.Contains(" * ", actual[0]);
            Assert.Contains(" X ", actual[1]);
            Assert.Equal("XXXXXXXXX", actual[5]);
            Assert.Contains(" X ", actual[6]);
        }
    }
}
