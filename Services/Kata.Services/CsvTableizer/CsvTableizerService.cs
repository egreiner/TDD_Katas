﻿namespace Kata.Services.CsvTableizer
{
    using System.Collections.Generic;
    using System.Linq;

    public class CsvTableizerService
    {
        public IEnumerable<string> ToTable(IList<string> csvLines) =>
            this.ToTable(csvLines, 1, csvLines.Count);

        public IEnumerable<string> ToTablePage(IList<string> csvLines, int page, int length)
        {
            var rowsOnPage = length - 2;
            var start = page * rowsOnPage + 1;
            return this.ToTable(csvLines, start, start + rowsOnPage);
        }

        private IEnumerable<string> ToTable(IList<string> csvLines, int start, int end)
        {
            var widths = this.GetMaxColumnWidths(csvLines).ToList();
            if (widths.Count == 0) yield break;

            yield return this.CreateTableRow(this.SplitCsvLine(csvLines[0]), widths);
            yield return this.CreateTitleSeparator(widths);

            for (var i = start; i < end && i < csvLines.Count; i++)
                yield return this.CreateTableRow(this.SplitCsvLine(csvLines[i]), widths);
        }


        public List<List<string>> SplitCsvLines(IEnumerable<string> csvLines)
        {
            var result = new List<List<string>>();

            foreach (var csvLine in csvLines) 
                result.Add(this.SplitCsvLine(csvLine));

            return result;
        }

        public List<string> SplitCsvLine(string csvLine) =>
            csvLine.Split(';').ToList();

        public List<int> GetMaxColumnWidths(IEnumerable<string> csvLines)
        {
            var result = new List<int>();

            var splitLines = this.SplitCsvLines(csvLines);
            if (splitLines?.Count == 0) return result;

            var columnsQuantity = splitLines[0].Count;

            for (var i = 0; i < columnsQuantity; i++) 
                result.Add(splitLines.Max(x => x[i].Length));

            return result;
        }

        public string CreateTitleSeparator(IEnumerable<int> widths)
        {
            var result = string.Empty;

            foreach (var width in widths) 
                result += new string('-', width) + "+";

            return result;
        }

        public string CreateTableRow(IList<string> csvLine, List<int> widths)
        {
            var result = string.Empty;

            for (int i = 0; i < widths.Count; i++)
            {
                var text = csvLine[i];
                var spaces = new string(' ', widths[i] - text.Length);
                result += $"{text}{spaces}|";
            }

            return result;
        }
    }
}