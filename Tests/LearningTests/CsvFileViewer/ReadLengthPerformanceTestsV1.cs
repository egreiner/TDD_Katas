namespace LearningTests.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("Sequential")]
    public class ReadLengthPerformanceTestsV1
    {
        private readonly int expectedFileLength = 10_001;


        [Fact]
        public void Test_Read_file_length_async1()
        {
            var file = GetTestCsvFile();

            var actual = this.GetFileLengthAsync(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }

        [Fact]
        public async Task Test_Read_file_async()
        {
            var file = GetTestCsvFile();

            var expected = 15;
            var actual = 0;
            // TIP async enumerable consumer
            await foreach (var line in this.ReadFileAsync(file, 9_000, expected))
                actual++;

            Assert.Equal(expected, actual);
        }


        // this is slower than V2...
        private async Task<int> GetFileLengthAsync(string fileName)
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


        // this version is 10 times slower than V2...
        private async IAsyncEnumerable<string> ReadFileAsync(string fileName, int start, int length)
        {
            // TIP async enumerable stream
            var counter = 0;
            using var reader = new StreamReader(File.OpenRead(fileName));
            while (counter < (start + length) && !reader.EndOfStream)
            {
                counter++;
                var line = await reader.ReadLineAsync();
                if (counter > start && line?.Trim().Length > 0)
                    yield return line;
            }
        }


        private static string GetTestCsvFile()
        {
            var dir = @"C:\DataServer\Developer\In523EasySteps\TDD_Kata\SolutionItems\";
            ////return $@"{dir}CSVViewer\besucher.csv";        // 1_001
            return $@"{dir}CSVViewer\besucherLarge.csv";        // 10_001
            ////return $@"{dir}LargeCsvFiles\besucherBig.csv";      // 100_001
            ////return $@"{dir}LargeCsvFiles\besucherHugh.csv";     // 1_000_001
            ////return $@"{dir}LargeCsvFiles\besucherMonster.csv";  // 10_000_001
        }
    }
}