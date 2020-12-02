namespace IntegrationTests.Services.CsvFileViewer
{
    using System.Threading.Tasks;
    using Kata.Services.CsvFileViewer;
    using Xunit;

    [Collection("Sequential")]
    public class CsvFileServiceTests
    {
        private int expectedFileLength = 10_001;

        [Fact]
        public void Test_Read_file_length_asyncV1()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = cut.GetFileLengthAsync(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public void Test_Read_file_length_asyncV2()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = cut.GetFileLengthAsyncV2(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public void Test_Read_file_length_asyncV12()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = cut.GetFileLengthAsync(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public void Test_Read_file_length_asyncV22()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = cut.GetFileLengthAsyncV2(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public async Task Test_Read_file_async()
        {
            var cut = new CsvFileService();
            var file = GetTestCsvFile();

            var actual = 0;
            // TIP async enumerable consumer
            await foreach (var line in cut.ReadFileAsync(file, 0, 2000))
                actual++;

            Assert.Equal(this.expectedFileLength, actual);
        }


        private static string GetTestCsvFile()
        {
            var dir = System.IO.Directory.GetCurrentDirectory();
            var file = System.IO.Path.Combine(dir, "CsvFileViewer", "besucherLarge.csv");
            return file;
        }

    }
}
