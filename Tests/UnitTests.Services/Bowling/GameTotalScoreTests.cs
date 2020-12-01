namespace UnitTests.Services.Bowling
{
    using Xunit;

    public class GameTotalScoreTests
    {
        private TestTools Tools { get; } = new TestTools();


        [Fact]
        public void Should_be_Zero()
        {
            var cut = this.Tools.GetGame();

            int actual = cut.TotalScore();

            Assert.Equal(0, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void Single_Roll_Should_return_PinsRolled(int pinsRolled)
        {
            var expected = pinsRolled;

            var cut = this.Tools.GetGame();
            var frame = this.Tools.GetFrame(pinsRolled);
            cut.Frames.Add(frame);

            int actual = cut.TotalScore();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(3, 3, 6)]
        public void OpenFrame_Should_return_FrameScore(int pinsRolled1, int pinsRolled2, int expected)
        {
            var cut = this.Tools.GetGame();
            var frame = this.Tools.GetFrame(pinsRolled1, pinsRolled2);
            cut.Frames.Add(frame);

            int actual = cut.TotalScore();

            Assert.Equal(expected, actual);
        }
    }
}
