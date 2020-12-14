namespace UnitTests.Services.Cache
{
    using Kata.Services.Cache;
    using Xunit;

    public class LFUKeyWeightedCacheTests
    {
        [Fact]
        public void Test_CacheLength_does_not_exceed_maximum()
        {
            var maxLength = 10;
            var expected = maxLength;
            var cut = GetCache(maxLength);

            for (int i = 0; i <= 20; i++)
                cut.Set(i, i);

            var actual = cut.Items.Count;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, true)]
        [InlineData(2, false)]
        [InlineData(1, true)]
        public void Test_lower_keys_get_removed(int value, bool expected)
        {
            var maxLength = 3;
            var cut = GetCache(maxLength);

            for (int i = 4; i > 0; i--)
                cut.Set(i, i);

            var actual = cut.Contains(value);

            Assert.Equal(expected, actual);
        }


        private static LFUKeyWeightedCache<int, int> GetCache(int maxLength) =>
            new LFUKeyWeightedCache<int, int>(maxLength);
    }
}
