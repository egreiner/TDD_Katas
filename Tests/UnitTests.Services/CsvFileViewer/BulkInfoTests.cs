namespace UnitTests.Services.CsvFileViewer
{
    using Kata.Services.CsvFileViewer;
    using Xunit;

    public class BulkInfoTests
    {
        [Theory]
        [InlineData(10, 10, 1, 0)]
        [InlineData(10, 10, 10, 0)]
        [InlineData(10, 10, 11, 1)]
        [InlineData(10, 20, 20, 0)]
        [InlineData(10, 20, 21, 1)]
        [InlineData(10, 20, 40, 1)]
        [InlineData(10, 20, 41, 2)]
        [InlineData(10, 100, 1, 0)]
        [InlineData(10, 100, 100, 0)]
        [InlineData(10, 100, 101, 1)]
        [InlineData(10, 100, 210, 2)]
        public void Test_BulkInfo_Index(
            int recordsOnPage,
            int bulkPages,
            int page,
            int expected)
        {
            var settings = new PageCacheSettings(recordsOnPage, 100);
            settings.BulkReadPages = bulkPages;

            var cut = BulkInfo.Create(page, settings);
            var actual = cut.BulkId;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10, 1, 0)]
        [InlineData(10, 10, 9, 0)]
        [InlineData(10, 10, 10, 0)]
        [InlineData(10, 10, 11, 10)]
        [InlineData(10, 20, 1, 0)]
        [InlineData(10, 20, 20, 0)]
        [InlineData(10, 20, 21, 20)]
        [InlineData(10, 20, 40, 20)]
        public void Test_BulkInfo_Start(
            int recordsOnPage,
            int bulkPages,
            int page,
            int expected)
        {
            var settings = new PageCacheSettings(recordsOnPage, 100)
            {
                BulkReadPages = bulkPages
            };

            var cut = BulkInfo.Create(page, settings);
            var actual = cut.StartPage;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10, 1, 0)]
        [InlineData(10, 10, 2, 1)]
        [InlineData(10, 10, 9, 8)]
        [InlineData(10, 10, 10, 9)]
        [InlineData(10, 10, 11, 0)]
        [InlineData(10, 20, 1, 0)]
        [InlineData(10, 20, 20, 19)]
        [InlineData(10, 20, 21, 0)]
        [InlineData(10, 20, 40, 19)]
        [InlineData(10, 20, 41, 0)]
        [InlineData(10, 20, 42, 1)]
        public void Test_BulkInfo_OffsetIndex(int recordsOnPage, int bulkPages, int page, int expected)
        {
            var settings = new PageCacheSettings(recordsOnPage, 100)
            {
                BulkReadPages = bulkPages
            };

            var cut = BulkInfo.Create(page, settings);
            var actual = cut.OffsetIndex;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10, 1, 0)]
        [InlineData(10, 10, 2, 10)]
        [InlineData(10, 10, 9, 80)]
        [InlineData(10, 10, 10, 90)]
        [InlineData(10, 10, 11, 0)]
        [InlineData(10, 20, 1, 0)]
        [InlineData(10, 20, 20, 190)]
        [InlineData(10, 20, 21, 0)]
        [InlineData(10, 20, 40, 190)]
        [InlineData(10, 20, 41, 0)]
        [InlineData(10, 20, 42, 10)]
        public void Test_BulkInfo_OffsetStart(int recordsOnPage, int bulkPages, int page, int expected)
        {
            var settings = new PageCacheSettings(recordsOnPage, 100)
            {
                BulkReadPages = bulkPages
            };

            var cut = BulkInfo.Create(page, settings);
            var actual = cut.OffsetStart;

            Assert.Equal(expected, actual);
        }
    }
}
