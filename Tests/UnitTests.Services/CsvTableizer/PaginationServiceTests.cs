namespace UnitTests.Services.CsvTableizer
{
    using Kata.Services.CsvFileViewer;
    using Xunit;

    public class PaginationServiceTests
    {
        [Theory]
        [InlineData(100, 10, 10)]
        [InlineData(99, 10, 10)]
        [InlineData(1, 10, 1)]
        [InlineData(0, 10, 0)]  // count of records not known at the beginning for very large files
        public void Test_PageRange(int records, int recordsPerPage, int expected)
        {
            var cut = new PaginationService(records, recordsPerPage);

            var actual = cut.PageRange.Max;

            Assert.Equal(expected, actual);
            Assert.Equal(1, cut.PageRange.Min);
        }

        [Fact]
        public void Test_PageRange_unknown()
        {
            var cut = new PaginationService(0, 100);

            var actual = cut.PageInfo;

            Assert.EndsWith("1 of ?", actual);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(0, 1)]
        [InlineData(200, 10)]
        public void Test_GetPage(int page, int expected)
        {
            var cut = new PaginationService(100, 10);

            var actual = cut.GetPage(page);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(1, 2)]
        [InlineData(9, 10)]
        public void Test_GetNextPage(int startPage, int expected)
        {
            var cut = new PaginationService(100, 10);

            cut.GetPage(startPage);
            var actual = cut.GetNextPage();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 9)]
        [InlineData(1, 1)]
        [InlineData(9, 8)]
        public void Test_GetPrevPage(int startPage, int expected)
        {
            var cut = new PaginationService(100, 10);

            cut.GetPage(startPage);
            var actual = cut.GetPrevPage();

            Assert.Equal(expected, actual);
        }

        // ICYMI i do not repair the know bug now...
        [Theory]
        [InlineData(10, 1)]
        [InlineData(1, 1)]
        [InlineData(9, 1)]
        public void Test_GetFirstPage(int startPage, int expected)
        {
            var cut = new PaginationService(100, 10);

            cut.GetPage(startPage);
            var actual = cut.GetFirstPage();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(1, 10)]
        [InlineData(5, 10)]
        public void Test_GetLastPage(int startPage, int expected)
        {
            var cut = new PaginationService(100, 10);

            cut.GetPage(startPage);
            var actual = cut.GetLastPage();

            Assert.Equal(expected, actual);
        }
    }
}