namespace IntegrationTests.Services.CsvFileViewer
{
    using Kata.Services.CsvFileViewer;
    using Kata.Services.Logger;
    using Xunit;

    [Collection("Sequential")]
    public class BulkCachedCsvFileServiceTests
    {
        private readonly int expectedFileLength = 1_001;

        [Fact]
        public void Test_Read_file_length_async()
        {
            var cut = GetCachedCsvFileService(10, 100);

            var actual = cut.GetFileLengthAsync().Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public void Test_Read_title()
        {
            var cut = GetCachedCsvFileService(10, 100);

            var list = cut.GetPageAsync(1).Result;

            var actual = list[0];
            Assert.StartsWith("Name", actual);

            Assert.NotEmpty(actual);
        }
        
        [Fact]
        public void Test_GetFirstPage()
        {
            var cut = GetCachedCsvFileService(10, 100);

            var actual = cut.GetPageAsync(1).Result;

            var log = Log.OrderedLogInfos;
            var cache = cut.Cache.Items;

            Assert.Equal(11, actual.Count);
        }
        
        [Theory]
        [InlineData(1, "1 ")]
        [InlineData(2, "11 ")]
        public void Test_GetFirstRecordOnPage(int page, string expected)
        {
            var cut = GetCachedCsvFileService(10, 100);

            var list = cut.GetPageAsync(page).Result;

            var actual = list[1];
            Assert.StartsWith(expected, actual);
        }


        [Theory]
        [InlineData(1, "10 ")]
        [InlineData(2, "20 ")]
        public void Test_GetLastRecordOnPage(int page, string expected)
        {
            var cut = GetCachedCsvFileService(10, 100);

            var list = cut.GetPageAsync(page).Result;

            var actual = list[10];
            Assert.StartsWith(expected, actual);
        }


        private static BulkCachedCsvFileService GetCachedCsvFileService(int rowsOnPage, int maxCachedPages)
        {
            var file = GetTestCsvFile();
            var settings = new PageCacheSettings(rowsOnPage, maxCachedPages);
            var pagination = new PaginationService(rowsOnPage);
            return new BulkCachedCsvFileService(file, settings, pagination);
        }
        
        private static string GetTestCsvFile()
        {
            var dir = @"C:\DataServer\Developer\In523EasySteps\TDD_Kata\SolutionItems\";
            return $@"{dir}CSVViewer\besucher.csv";        // 1_001
            ////return $@"{dir}CSVViewer\besucherLarge.csv";        // 10_001
            ////return $@"{dir}LargeCsvFiles\besucherBig.csv";      // 100_001
            ////return $@"{dir}LargeCsvFiles\besucherHugh.csv";     // 1_000_001
            ////return $@"{dir}LargeCsvFiles\besucherMonster.csv";  // 10_000_001
        }
    }
}