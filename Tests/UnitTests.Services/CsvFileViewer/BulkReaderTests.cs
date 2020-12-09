namespace UnitTests.Services.CsvFileViewer
{
    using Kata.Services.CsvFileViewer;
    using Xunit;

    public class BulkReaderTests
    {
        [Theory]
        [InlineData(10, 10, 1, 1)]
        [InlineData(10, 10, 9, 1)]
        [InlineData(10, 10, 10, 11)]
        [InlineData(10, 20, 1, 1)]
        [InlineData(10, 20, 20, 21)]
        [InlineData(10, 20, 39, 21)]
        [InlineData(10, 20, 40, 41)]
        public void Test_Start_of_PageBlock(int recordsOnPage, int bulkPages, int page, int expected)
        {
            var cut = new PageCacheSettings(recordsOnPage, 100);
            cut.BulkReadPages = bulkPages;

            var actual = cut.GetStartBulkBlockAtPage(page);

            Assert.Equal(expected, actual);
        }
    }
}
