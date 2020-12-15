namespace UnitTests.Services.Cache
{
    using Kata.Services.Cache;
    using Xunit;

    public class LRUCacheTests
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
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        [InlineData(11, true)]
        [InlineData(12, false)]
        public void Test_First_added_item_get_removed(int value, bool expected)
        {
            var maxLength = 10;
            var cut = GetCache(maxLength);

            for (int i = 1; i <= 11; i++)
                cut.Set(i, i);

            var actual = cut.Contains(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(10, true)]
        [InlineData(11, true)]
        [InlineData(12, false)]
        public void Test_First_added_item_get_not_removed(int value, bool expected)
        {
            var maxLength = 10;
            var cut = GetCache(maxLength);

            for (int i = 1; i <= 10; i++)
                cut.Set(i, i);

            cut.Get(1);
            cut.Set(11, 11);

            var actual = cut.Contains(value);

            Assert.Equal(expected, actual);
        }


        private static LRUCache<int, int> GetCache(int maxLength) =>
            new(maxLength);
    }
}
