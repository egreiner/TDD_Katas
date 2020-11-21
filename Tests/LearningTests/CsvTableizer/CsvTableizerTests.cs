namespace LearningTests.CsvTableizer
{
    using System.Collections.Generic;
    using System.Linq;
    using Kata.Services.CsvTableizer;
    using Xunit;

    public class CsvTableizerTests
    {
        [Fact]
        public void Test_SplitCsvLine()
        {
            var cut = new CsvTableizerService();
            var csvLine = "Name;Street;City;Age;  ;";

            List<string> actual = cut.SplitCsvLine(csvLine);

            Assert.Equal(6, actual.Count);
        }

        [Fact]
        public void Test_SplitCsvLines()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines();

            List<List<string>> actual = cut.SplitCsvLines(csvLines);

            Assert.Equal(4, actual.Count);
        }

        [Fact]
        public void Test_MaxColumnWidthCalculation()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetSimpleCsvLines();

            List<int> actual = cut.GetMaxColumnWidths(csvLines);

            var actualMaxColumn1 = actual[0];
            var actualMaxColumn2 = actual[1];

            Assert.Equal(5, actualMaxColumn1);
            Assert.Equal(3, actualMaxColumn2);
        }

        [Fact]
        public void Test_CreateTitleSeparator()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetSimpleCsvLines();

            List<int> widths = cut.GetMaxColumnWidths(csvLines);
            string actual = cut.CreateTitleSeparator(widths);

            Assert.Equal("-----+---+", actual);
        }

        [Fact]
        public void Test_CreateTableLine()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetSimpleCsvLines();
            var splitLines = cut.SplitCsvLines(csvLines);

            List<int> widths = cut.GetMaxColumnWidths(csvLines);
            string actual = cut.CreateTableRow(splitLines[0], widths);

            Assert.Equal("12   |123|", actual);
        }

        [Fact]
        public void Test_ToTable()
        {
            var cut = new CsvTableizerService();
            var csvLines = GetCsvLines().ToList();

            var actual = cut.ToTable(csvLines).ToList();

            Assert.Equal(5, actual.Count);
        }


        private static IEnumerable<string> GetCsvLines()
        {
            yield return "Name;Street;City;Age";
            yield return "Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return "Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return "Paul Meier;Münchener Weg 1;87654 München;65";
        }

        private static IEnumerable<string> GetSimpleCsvLines()
        {
            yield return "12;123";
            yield return "12345;1";
        }
    }
}
