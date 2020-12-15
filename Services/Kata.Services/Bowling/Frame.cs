namespace Kata.Services.Bowling
{
    using System.Collections.Generic;

    public class Frame
    {
        public const int MaxPins   = 10;
        private const int MaxRolls = 2;

        private readonly IFrameValidator validator;
        private readonly bool isLastFrame;
        private int bonusRoll = 0;


        public Frame(IFrameValidator validator, bool isLastFrame = false)
        {
            this.validator = validator;
            this.isLastFrame = isLastFrame;
        }

        public int FrameScore { get; private set; }

        public List<int> PinsRolled { get; } = new();

        public int MaxRollsPerFrame => MaxRolls + this.bonusRoll;

        public int MaxScorePerFrame => this.isLastFrame 
            ?  3 * MaxPins 
            : MaxPins;


        ////public override string ToString()
        ////{
        ////    var rolls = string.Join(", ", this.PinsRolled);
        ////    return $"FrameScore: {this.FrameScore} - ({rolls})";
        ////}


        public void AddPinsRolled(int pins)
        {
            this.validator.Validate(this, pins);

            this.PinsRolled.Add(pins);

            this.FrameScore += pins;
            
            if (this.isLastFrame && (this.IsStrike() || this.IsSpare()))
                this.bonusRoll = 1;
        }
        
        public bool IsStrike()
        {
            return this.PinsRolled.Count == 1 && this.FrameScore >= MaxPins;
        }

        public bool IsSpare()
        {
            return !this.IsStrike() && this.FrameScore >= MaxPins;
        }

        // TODO will be different in LastFrame
        public bool IsCompleted()
        {
            return !this.isLastFrame && 
                   (this.IsStrike() || this.IsSpare()) || 
                   this.PinsRolled.Count >= this.MaxRollsPerFrame;
        }
    }
}