namespace UnitTests.Services.Bowling
{
    using System.Linq;
    using Kata.Services.Bowling;
    using Xunit;

    public class GameIsOverTests
    {
        private TestTools Tools { get; } = new TestTools();


        [Fact]
        public void Should_be_False()
        {
            // 0 Frames, 1 Frame,... 9 Frames.
            var list = Enumerable.Range(0, 10).ToList();

            Assert.All(list, frame =>
            {
                var cut = this.CreateGameWithAllStrike(frame);

                var actual = cut.IsOver();

                Assert.False(actual);
            });
        }

        [Fact]
        public void AllStrike_Should_be_True()
        {
            var cut = this.CreateGameWithAllStrike(10);

            var actual = cut.IsOver();

            Assert.True(actual);
        }

        [Fact]
        public void AllSpare_should_be_True()
        {
            var cut = this.CreateGameWithAllSpare(10);

            var actual = cut.IsOver();

            Assert.True(actual);
        }

        [Fact]
        public void Last_frame_not_completed_should_be_False()
        {
            var cut = this.CreateGameWithAllStrike(9);

            var validator = new FrameValidator();
            var frame = new Frame(validator);
            frame.AddPinsRolled(1);
            cut.Frames.Add(frame);

            var actual = cut.IsOver();

            Assert.False(actual);
        }

        [Fact]
        public void Last_frame_completed_with_Spare_should_be_True()
        {
            var cut = this.CreateGameWithAllStrike(9);

            var frame = this.Tools.GetFrame(1, 9);
            cut.Frames.Add(frame);

            var actual = cut.IsOver();

            Assert.True(actual);
        }

        [Fact]
        public void Last_frame_completed_with_open_Frame_should_be_True()
        {
            var cut = this.CreateGameWithAllSpare(9);

            var frame = this.Tools.GetFrame(1, 8);
            cut.Frames.Add(frame);

            var actual = cut.IsOver();

            Assert.True(actual);
        }



        private Game CreateGameWithAllStrike(int frames)
        {
            var result = new Game();
            for (var i = 0; i < frames; i++)
            {
                var frame = this.Tools.GetStrikeFrame();
                result.Frames.Add(frame);
            }

            return result;
        }

        private Game CreateGameWithAllSpare(int frames)
        {
            var result = new Game();
            for (var i = 0; i < frames; i++)
            {
                var frame = this.Tools.GetFrame(1, 9);
                result.Frames.Add(frame);
            }

            return result;
        }
    }
}
