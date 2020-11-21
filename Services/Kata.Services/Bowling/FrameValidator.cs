namespace Kata.Services.Bowling
{
    using System;

    public class FrameValidator: IFrameValidator
    {
        public void Validate(Frame frame, int pins)
        {
            if (pins > Frame.MaxPins)
            {
                var msg =
                    $"Max pins per roll exceeded! There could only be added {Frame.MaxPins} pins in one roll!";
                throw new ArgumentOutOfRangeException(nameof(pins), pins, msg);
            }

            if (pins < 0)
            {
                var msg = $"There couldn't be rolled a negative ({pins}) pin-count!";
                throw new ArgumentOutOfRangeException(nameof(pins), pins, msg);
            }

            if (frame.FrameScore + pins > frame.MaxScorePerFrame)
            {
                var msg =
                    $"Max pins per frame exceeded! There could only be added {frame.MaxScorePerFrame} pins in one frame!";
                throw new ArgumentOutOfRangeException(nameof(pins), pins, msg);
            }
        }
    }
}