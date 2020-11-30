namespace LearningTests.CsvTableizer
{
    using Kata.Services.CsvTableizer;
    using Xunit;

    public class PageServiceTests
    {
        [Theory]
        [InlineData(100, 10, 10)]
        [InlineData(99, 10, 10)]
        [InlineData(1, 10, 1)]
        public void Test_SplitCsvLine(int records, int recordsPerPage, int expected)
        {
            var cut = new PageService(records, recordsPerPage);

            var actual = cut.MaxPage;

            Assert.Equal(expected, actual);
        }
    }
}
