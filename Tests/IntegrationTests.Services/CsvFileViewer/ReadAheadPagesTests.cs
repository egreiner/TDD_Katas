namespace IntegrationTests.Services.CsvFileViewer
{
    using Kata.Services.CsvFileViewer;
    using Xunit;

    [Collection("Sequential")]
    public class ReadAheadPagesTests
    {
        // TODO not working at the moment

        ////[Fact]
        ////public void Test_ReadAheadFirstPages()
        ////{
        ////    var cut = GetCachedCsvFileService(10, 100);

        ////    var actual = cut.ReadAheadFirstPagesAsync().Result;

        ////    var log = cut.Log;
        ////    var cache = cut.Items.Items;

        ////    Assert.Equal(cut.CacheSettings.ReadAheadNextPages, cache.Count);
        ////}


        ////[Fact]
        ////public void Test_ReadAheadLastPages()
        ////{
        ////    var cut = GetCachedCsvFileService(10, 100);
        ////    var initialized = cut.SetRealFileLength().Result;

        ////    var actual = cut.ReadAheadLastPagesAsync().Result;

        ////    var log = cut.Log;
        ////    var cache = cut.Items.Items;

        ////    Assert.Equal(cut.CacheSettings.ReadAheadPrevPages, cache.Count);
        ////}



        private static CachedCsvFileService GetCachedCsvFileService(int rowsOnPage, int maxCachedPages)
        {
            var file = GetTestCsvFile();
            var settings = new PageCacheSettings(rowsOnPage, maxCachedPages);
            var pagination = new PaginationService(rowsOnPage);
            return new CachedCsvFileService(file, settings, pagination);
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