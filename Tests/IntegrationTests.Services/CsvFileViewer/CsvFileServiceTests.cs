namespace IntegrationTests.Services.CsvFileViewer
{
    using System.Linq;
    using System.Threading.Tasks;
    using Kata.Services.CsvFileViewer;
    using Xunit;

    [Collection("Sequential")]
    public class CsvFileServiceTests
    {
        private readonly int expectedFileLength = 1_000_001;

        [Fact]
        public void Test_Read_file_length_async()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = cut.GetFileLengthAsync(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public async Task Test_Read_file_async()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var expected = 15;
            var actual = cut.ReadFileAsync(file, 900_000, expected).Result;

            Assert.Equal(expected, actual.Count());
        }


        private static string GetTestCsvFile()
        {
            var dir = @"C:\DataServer\Developer\In523EasySteps\TDD_Kata\SolutionItems\";
            //var file = $@"{dir}CSVViewer\besucherLarge.csv";        // 10_001
            //var file = $@"{dir}LargeCsvFiles\besucherBig.csv";      // 100_001
            var file = $@"{dir}LargeCsvFiles\besucherHugh.csv";     // 1_000_001
            ////var file = $@"{dir}LargeCsvFiles\besucherMonster.csv";  // 10_000_001

            return file;
        }
    }
}
