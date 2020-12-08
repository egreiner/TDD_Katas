namespace IntegrationTests.Services.CsvFileViewer
{
    using Kata.Services.CsvFileViewer;
    using Kata.Services.Logger;
    using Xunit;

    [Collection("Sequential")]
    public class ReadAheadPagesTests
    {
        [Fact]
        public void Test_ReadAheadFirstPages()
        {
            var cut = GetCachedCsvFileService(10, 100);

            var actual = cut.GetPageAsync(1).Result;

            var log = Log.OrderedLogInfos;
            var cache = cut.Cache.Items;

            Assert.True(cache.Count >= cut.CacheSettings.ReadAheadPages);
        }

        [Fact]
        public void Test_ReadAheadLastPages()
        {
            var cut = GetCachedCsvFileService(10, 100);

            var actual = cut.GetPageAsync(100).Result;

            var log = Log.OrderedLogInfos;
            var cache = cut.Cache.Items;

            Assert.True(cache.Count >= cut.CacheSettings.ReadAheadPages);
        }


        private static CachedCsvFileService GetCachedCsvFileService(int rowsOnPage, int maxCachedPages)
        {
            var file = GetTestCsvFile();
            var settings = new PageCacheSettings(rowsOnPage, maxCachedPages);
            var pagination = new PaginationService(rowsOnPage);

            var service = new CachedCsvFileService(file, settings, pagination);
            _ = service.SetRealFileLength().Result;
            return service;
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