namespace UnitTests.Services.Bowling
{
    using Kata.Services.Bowling;

    public class TestTools
    {
        public Game GetGame()
        {
            return new Game();
        }

        public Frame GetStrikeFrame() =>
            this.GetFrame(10);

        public Frame GetFrame(int roll)
        {
            var validator = new FrameValidator();
            var frame = new Frame(validator);
            frame.AddPinsRolled(roll);
            return frame;
        }

        public Frame GetFrame(int roll1, int roll2)
        {
            var validator = new FrameValidator();
            var frame = new Frame(validator);
            frame.AddPinsRolled(roll1);
            frame.AddPinsRolled(roll2);
            return frame;
        }
    }
}