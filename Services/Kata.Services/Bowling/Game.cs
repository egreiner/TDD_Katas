namespace Kata.Services.Bowling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private const int MaxFramesPerGame = 10;


        public List<Frame> Frames { get; } = new List<Frame>();

        public Frame CurrentFrame { get; private set; }


        public void AddRoll(int pins)
        {
            this.ValidateIsOver();

            var (frame , isNewFrame) = this.GetCurrentFrame();
            
            frame.AddPinsRolled(pins);
            
            if (isNewFrame) this.Frames.Add(frame);
        }

        public bool IsOver()
        {
            var allExistingFramesAreCompleted = this.Frames.All(x => x.IsCompleted());

            return this.Frames?.Count >= MaxFramesPerGame &&
                   allExistingFramesAreCompleted;
        }

        // TODO put this to an segregated component - ScoreCalculationService
        public int TotalScore() =>
            this.TotalScore(this.Frames.Count);

        
        public int TotalScore(int lastFrameThatShouldBeCalculated)
        {
            var sum = 0;

            for (var i = 0; i < lastFrameThatShouldBeCalculated; i++)
            {
                var frame = this.Frames[i];
                sum += frame.FrameScore;

                if (frame.IsStrike())
                    sum += this.GetBonusNextTwoRolls(i);
                else if (frame.IsSpare())
                    sum += this.GetBonusNextRoll(i);
            }

            return sum;
        }

        private int GetBonusNextRoll(in int i)
        {
            var idNextFrame = i + 1;
            
            if (this.Frames.ElementAtOrDefault(idNextFrame) == null)
                return 0;

            var frame = this.Frames[idNextFrame];

            return frame.PinsRolled.Count >= 1 
                ? frame.PinsRolled[0] 
                : 0;
        }

        private int GetBonusNextTwoRolls(in int i)
        {
            var idNextFrame = i + 1;

            if (this.Frames.ElementAtOrDefault(idNextFrame) == null)
                return 0;

            var frame = this.Frames[idNextFrame];

            return frame.PinsRolled.Count >= 2
                ? frame.PinsRolled[0] + frame.PinsRolled[1]
                : frame.PinsRolled[0] + this.GetBonusNextRoll(idNextFrame);
        }

        // Builder method to create/return
        // CurrentFrame / new StandardFrame / LastFrame
        private (Frame, bool) GetCurrentFrame()
        {
            if (this.CurrentFrame?.IsCompleted() == false)
                return (this.CurrentFrame, false);

            var isLastFrame = this.Frames.Count >= MaxFramesPerGame -1;
            this.CurrentFrame = new Frame(new FrameValidator(), isLastFrame);

            return (this.CurrentFrame, true);
        }

        private void ValidateIsOver()
        {
            if (this.IsOver())
                throw new InvalidOperationException("Game over! No rolls could be added!");
        }
    }
}