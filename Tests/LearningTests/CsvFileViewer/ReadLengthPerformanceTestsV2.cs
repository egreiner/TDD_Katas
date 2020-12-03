namespace LearningTests.CsvFileViewer
{
    using System.Collections.Generic;
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

        [Fact]
        public async Task Test_Read_file_async()
        {
            var file = GetTestCsvFile();

            var expected = 15;
            var actual = this.ReadFileAsync(file, 9_000, expected).Result;

            Assert.Equal(expected, actual.Count());
        }
        

        private void ReadFileLength()
        {
            var file = GetTestCsvFile();

            var actual = this.GetFileLengthAsyncV2(file).Result;

            Assert.Equal(this.expectedFileLength, actual);
        }


        private async Task<int> GetFileLengthAsyncV2(string fileName) =>
            await Task.Run(() =>
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);

        private async Task<IEnumerable<string>> ReadFileAsync(string fileName, int start, int length)
        {
            return await Task.Run(() =>
                File.ReadLines(fileName).Skip(start).Take(length)
            ).ConfigureAwait(false);
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
