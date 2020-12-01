namespace UnitTests.Services.Rot13
{
    using Kata.Services.Rot13;
    using Xunit;

    public class Rot13Tests
    {
        [Theory]
        [InlineData("AB", "BC")]
        [InlineData("ABC", "BCD")]
        public void Test_Convert_with_rot1(string value, string expected)
        {
            var cut = new Rot13Converter(1);

            var actual = cut.Convert(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("A", "N")]
        [InlineData("a", "N")]
        [InlineData("O", "B")]
        [InlineData("Y", "L")]
        [InlineData("Z", "M")]
        [InlineData(",", ",")]
        [InlineData("!", "!")]
        [InlineData("Hello, World", "URYYB, JBEYQ")]
        public void Test_Convert_complete_string(string value, string expected)
        {
            var cut = new Rot13Converter();

            var actual = cut.Convert(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0", "D")]
        [InlineData("Z", "C")]
        public void Test_encode_digits(string value, string expected)
        {
            var cut = new Rot13Converter(13, true);

            var actual = cut.Convert(value);

            Assert.Equal(expected, actual);
        }
    }
}