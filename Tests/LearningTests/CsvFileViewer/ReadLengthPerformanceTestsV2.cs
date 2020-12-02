namespace LearningTests.CsvFileViewer
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;


    [Collection("Sequential")]
    public class ReadLengthPerformanceTestsV2
    {
        private readonly int expectedFileLength = 10_001;


        [Fact]
        public void Test_Read_file_length_async1() =>
            this.ReadFileLength();


        [Fact]
        public void Test_Read_file_length_async2() =>
            this.ReadFileLength();

        private void ReadFileLength()
        {
            var file = GetTestCsvFile();

            var actual = this.GetFileLengthAsyncV2(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }


        private static string GetTestCsvFile()
        {
            var dir = System.IO.Directory.GetCurrentDirectory();
            var file = System.IO.Path.Combine(dir, "CsvFileViewer", "besucherLarge.csv");
            return file;
        }


        private async Task<int> GetFileLengthAsyncV2(string fileName) =>
            await Task.Run(() =>
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);
    }
}
