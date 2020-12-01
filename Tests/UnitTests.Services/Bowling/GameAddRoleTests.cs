namespace UnitTests.Services.Bowling
{
    using System;
    using Xunit;

    public class GameAddRoleTests
    {
        private TestTools Tools { get; } = new TestTools();


        [Fact]
        public void One_Roll_should_add_one_Frame_To_Frames()
        {
            var cut = this.Tools.GetGame();
            cut.AddRoll(1);

            var actual = cut.Frames.Count;

            Assert.Equal(1, actual);
        }


        [Fact]
        public void Two_Rolls_should_add_one_Frame_To_Frames()
        {
            var cut = this.Tools.GetGame();

            cut.AddRoll(1);
            cut.AddRoll(5);
            var actual = cut.Frames.Count;

            Assert.Equal(1, actual);
        }

        [Fact]
        public void Two_Strike_Rolls_should_add_two_Frames_To_Frames()
        {
            var cut = this.Tools.GetGame();

            cut.AddRoll(10);
            cut.AddRoll(10);
            var actual = cut.Frames.Count;

            Assert.Equal(2, actual);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 2)]
        [InlineData(0, 0, 0, 0, 2)]
        [InlineData(10, 10, 10, 10, 4)]
        [InlineData(1, 9, 2, 8, 2)]
        [InlineData(10, 1, 2, 6, 3)]
        [InlineData(10, 1, 9, 5, 3)]
        public void AddRolls_should_add_correct_Number_of_Frames_To_Frames(int pr1, int pr2, int pr3, int pr4, int expected)
        {
            var cut = this.Tools.GetGame();

            cut.AddRoll(pr1);
            cut.AddRoll(pr2);
            cut.AddRoll(pr3);
            cut.AddRoll(pr4);
            var actual = cut.Frames.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Expect_Exception_On_AddRoll_After_Game_IsOver()
        {
            var cut = this.Tools.GetGame();

            for (var i = 1; i <= 12; i++)
            {
                cut.AddRoll(10);
            }

            var actual = Assert.Throws<InvalidOperationException>(
                () => cut.AddRoll(10));

            Assert.Contains("Game over", actual.Message);
        }

        [Fact]
        public void Expect_Score_Zero_on_GutterGame()
        {
            var cut = this.Tools.GetGame();

            for (var i = 1; i <= 20; i++)
            {
                cut.AddRoll(0);
            }

            var actual = cut.TotalScore();

            Assert.Equal(0, actual);
        }


        [Fact]
        public void Expect_Score_300_on_AllStrikesGame()
        {
            var cut = this.Tools.GetGame();

            for (var i = 1; i <= 12; i++)
            {
                cut.AddRoll(10);
            }

            var actual = cut.TotalScore();

            Assert.Equal(300, actual);
        }

        [Fact]
        public void Expect_Exception_On_AddRoll_if_try_to_add_more_than_MaxPins_in_one_roll()
        {
            var cut = this.Tools.GetGame();

            var actual = Assert.Throws<ArgumentOutOfRangeException>(
                () => cut.AddRoll(100));

            Assert.Contains("Max pins per roll exceeded!", actual.Message);
        }

        [Fact]
        public void Expect_Exception_On_AddRoll_with_negativ_pins_rolled()
        {
            var cut = this.Tools.GetGame();

            var actual = Assert.Throws<ArgumentOutOfRangeException>(
                () => cut.AddRoll(-1));

            Assert.Contains("There couldn't be rolled a negative", actual.Message);
        }

        [Fact]
        public void Expect_Exception_On_AddRoll_if_try_to_add_more_than_MaxPins_in_one_frame()
        {
            var cut = this.Tools.GetGame();
            cut.AddRoll(2);

            var actual = Assert.Throws<ArgumentOutOfRangeException>(
                () => cut.AddRoll(9));

            Assert.Contains("Max pins per frame exceeded!", actual.Message);
        }
    }
}
