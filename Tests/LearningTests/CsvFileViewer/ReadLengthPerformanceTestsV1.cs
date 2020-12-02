namespace LearningTests.CsvFileViewer
{
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("Sequential")]
    public class ReadLengthPerformanceTestsV1
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

            var actual = this.GetFileLengthAsyncV1(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        private static string GetTestCsvFile()
        {
            var dir = System.IO.Directory.GetCurrentDirectory();
            var file = System.IO.Path.Combine(dir, "CsvFileViewer", "besucherLarge.csv");
            return file;
        }


        // this is slower than V2...
        private async Task<int> GetFileLengthAsyncV1(string fileName)
        {
            using var reader = new StreamReader(File.OpenRead(fileName));
            var result = 0;
            while (!reader.EndOfStream)
            {
                result++;
                _ = await reader.ReadLineAsync();
            }

            return result;
        }
    }
}