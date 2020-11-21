namespace LearningTests.WordWrap
{
    using Kata.Services.WordWrap;
    using Xunit;

    public class WordWrapTests
    {
        [Theory]
        [InlineData("bla blub", "bla\r\nblub")]
        [InlineData("bla  blub", "bla\r\nblub")]
        [InlineData("bla   blub", "bla\r\nblub")]
        [InlineData("bla    blub", "bla\r\nblub")]
        public void Test_insert_linefeed_instead_of_space(string text, string expected)
        {
            var cut = new WordWrapService();

            var actual = cut.WordWrap(text, 1);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("bla", 1, "bla")]
        [InlineData("bla blub", 5, "bla\r\nblub")]
        [InlineData("bla blub", 1, "bla\r\nblub")]
        [InlineData("bla blub", 50, "bla blub")]
        [InlineData("bla blub bla blub", 5, "bla\r\nblub\r\nbla\r\nblub")]
        [InlineData("bla blub bla blub", 8, "bla blub\r\nbla blub")]
        [InlineData("012 4567 901 3lub", 12, "012 4567 901\r\n3lub")]
        public void Test_insert_linefeed_if_line_is_longer_than(string text, int lineLength, string expected)
        {
            var cut = new WordWrapService();

            var actual = cut.WordWrap(text, lineLength);

            Assert.Equal(expected, actual);
        }
    }
}
