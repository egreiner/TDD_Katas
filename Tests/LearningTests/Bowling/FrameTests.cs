namespace LearningTests.Bowling
{
    using Kata.Services.Bowling;
    using Xunit;

    public class FrameTests
    {
        [Fact]
        public void Frame_IsSpare_should_be_False()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(9);

            var actual = cut.IsSpare();

            Assert.False(actual);
        }

        [Fact]
        public void Frame_IsSpare_should_be_True()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(2);
            cut.AddPinsRolled(8);

            var actual = cut.IsSpare();

            Assert.True(actual);
        }

        [Fact]
        public void Frame_IsStrike_should_be_False()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(9);

            var actual = cut.IsStrike();

            Assert.False(actual);
        }


        [Fact]
        public void Frame_cant_be_strike_and_spare()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(10);

            var isStrike = cut.IsStrike();
            var isSpare = cut.IsSpare();

            Assert.True(isStrike);
            Assert.False(isSpare);
        }


        [Fact]
        public void Frame_IsOver_should_be_False()
        {
            var cut = GetFrame();

            var actual = cut.IsCompleted();

            Assert.False(actual);
        }

        [Fact]
        public void Frame_with_Strike_IsOver_should_be_True()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(10);

            var actual = cut.IsCompleted();

            Assert.True(actual);
        }


        [Fact]
        public void Frame_with_Spare_IsOver_should_be_True()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(1);
            cut.AddPinsRolled(9);

            var actual = cut.IsCompleted();

            Assert.True(actual);
        }

        [Fact]
        public void Open_Frame_IsOver_should_be_True()
        {
            var cut = GetFrame();
            cut.AddPinsRolled(1);
            cut.AddPinsRolled(1);

            var actual = cut.IsCompleted();

            Assert.True(actual);
        }


        private static Frame GetFrame()
        {
            var validator = new FrameValidator();
            var cut = new Frame(validator);
            return cut;
        }
    }
}