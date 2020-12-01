namespace UnitTests.Services.Bowling
{
    using Xunit;

    public class GameBonusCalculationTests
    {
        private TestTools Tools { get; } = new TestTools();


        [Fact]
        public void Add_Three_Strikes()
        {
            var cut = this.Tools.GetGame();
            cut.AddRoll(10);
            cut.AddRoll(10);
            cut.AddRoll(10);

            var actual = cut.TotalScore();

            Assert.Equal(60, actual);
        }

        [Fact]
        public void Add_Three_Spares()
        {
            var cut = this.Tools.GetGame();
            cut.AddRoll(1);
            cut.AddRoll(9);
            cut.AddRoll(1);
            cut.AddRoll(9);
            cut.AddRoll(1);
            cut.AddRoll(9);

            var actual = cut.TotalScore();

            Assert.Equal(32, actual);
        }


        [Fact]
        //[Fact(Skip = "The service must be completed before this test makes sense")]
        public void Test_UntilFrame4_Uncle_Bobs_Example()
        {
            // frame 3 should have 29 as TotalScore
            var cut = this.Tools.GetGame();
            // 1. open frame
            cut.AddRoll(1);
            cut.AddRoll(4);

            // 2. open frame
            cut.AddRoll(4);
            cut.AddRoll(5);

            // 3. spare
            cut.AddRoll(6);
            cut.AddRoll(4);

            // 4. spare
            cut.AddRoll(5);
            cut.AddRoll(5);

            var actual = cut.TotalScore(3);

            Assert.Equal(29, actual);
        }

        [Fact]
        //[Fact(Skip = "The service must be completed before this test makes sense")]
        public void FinalTest_Uncle_Bobs_Example()
        {
            var cut = this.Tools.GetGame();
            // 1. open frame
            cut.AddRoll(1);
            cut.AddRoll(4);

            // 2. open frame
            cut.AddRoll(4);
            cut.AddRoll(5);

            // 3. spare
            cut.AddRoll(6);
            cut.AddRoll(4);

            // 4. spare
            cut.AddRoll(5);
            cut.AddRoll(5);

            // 5. strike
            cut.AddRoll(10);

            // 6.open frame
            cut.AddRoll(0);
            cut.AddRoll(1);

            // 7. spare
            cut.AddRoll(7);
            cut.AddRoll(3);

            // 8. spare
            cut.AddRoll(6);
            cut.AddRoll(4);

            // 9. strike
            cut.AddRoll(10);

            // 10. one spare and third roll
            cut.AddRoll(2);
            cut.AddRoll(8);
            cut.AddRoll(6);

            var actual = cut.TotalScore(1);
            Assert.Equal(5, actual);

            actual = cut.TotalScore(2);
            Assert.Equal(14, actual);

            actual = cut.TotalScore(3);
            Assert.Equal(29, actual);

            actual = cut.TotalScore(4);
            Assert.Equal(49, actual);

            actual = cut.TotalScore(5);
            Assert.Equal(60, actual);

            actual = cut.TotalScore(6);
            Assert.Equal(61, actual);

            actual = cut.TotalScore(7);
            Assert.Equal(77, actual);

            actual = cut.TotalScore(8);
            Assert.Equal(97, actual);

            actual = cut.TotalScore(9);
            Assert.Equal(117, actual);

            actual = cut.TotalScore(10);
            Assert.Equal(133, actual);
        }
    }
}










