namespace LearningTests.CsvTableizer
{
    using System.Collections.Generic;
    using System.Linq;
    using Kata.Services.CsvTableizer;
    using Xunit;

    public class CsvTableizerEmptyListTests
    {
        [Fact]
        public void Test_SplitCsvLines()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines();

            List<List<string>> actual = cut.SplitCsvLines(csvLines);

            Assert.Empty(actual);
        }

        [Fact]
        public void Test_MaxColumnWidthCalculation()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines();

            List<int> actual = cut.GetMaxColumnWidths(csvLines);

            Assert.Empty(actual);
        }

        [Fact]
        public void Test_CreateTitleSeparator()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines();

            List<int> widths = cut.GetMaxColumnWidths(csvLines);
            string actual = cut.CreateTitleSeparator(widths);

            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void Test_ToTable()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines().ToList();

            var actual = cut.ToTable(csvLines).ToList();

            Assert.Empty(actual);
        }


        private static IEnumerable<string> GetCsvLines() =>
            new List<string>();
    }
}
