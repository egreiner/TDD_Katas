namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.CsvTableizer;
    using Services.Extensions;

    public class CsvFileViewer
    {
        private const int DefaultPageLength = 24;

        private readonly CsvTableizerService csvService = new CsvTableizerService();
        private readonly ConsoleKey[] allowedKeys = new[] { ConsoleKey.A, ConsoleKey.X, ConsoleKey.F, ConsoleKey.L, ConsoleKey.N, ConsoleKey.P };
        private readonly string footer = "[N]ext page, [P]revious page, [F]irst page, [L]ast page, e[X]it";

        // FEX use record from .Net 5...
        // or... extract this to an ArgumentDto
        private (string fileName, int pageLength) arguments;
        private PageController pageController;

        public void Execute(string[] args)
        {
            ////args = new[] { "bla" };
            args = new[] { "bla", "14" };

            int lineCount;
            this.arguments = this.ReadArguments(args);
            var csvLines = ReadCsvFile(this.arguments.fileName);
            this.pageController = new PageController(csvLines.Count, this.arguments.pageLength -2);

            var key = new ConsoleKeyInfo('F', ConsoleKey.F, false, false, false);
            while (key.Key != ConsoleKey.X)
            {
                Console.Clear();

                lineCount = 0;
                var table = this.GetTable(csvLines, key.Key);

                writeLine($"Last key pressed: {key.Key}");
                writeLine(string.Empty);

                foreach (var line in table) 
                    writeLine(line);

                printFootLine();

                key = this.ReadKey(key);
            }

            void printFootLine()
            {
                while (lineCount <= this.arguments.pageLength) 
                    writeLine(string.Empty);
                writeLine(this.footer);
            }

            void writeLine(string line)
            {
                Console.WriteLine(line);
                lineCount++;
            }
        }


        private ConsoleKeyInfo ReadKey(ConsoleKeyInfo key)
        {
            var lastKey = key;
            key = Console.ReadKey();
            if (!this.allowedKeys.Contains(key.Key))
                key = lastKey;
            return key;
        }

        private List<string> GetTable(List<string> csvLines, ConsoleKey key)
        {
            var length = this.arguments.pageLength - 2;

            return key switch
            {
                ConsoleKey.F => this.csvService.ToTablePage(csvLines, this.pageController.GetFirstPage(), length).ToList(),
                ConsoleKey.P => this.csvService.ToTablePage(csvLines, this.pageController.GetPrevPage(), length).ToList(),
                ConsoleKey.N => this.csvService.ToTablePage(csvLines, this.pageController.GetNextPage(), length).ToList(),
                ConsoleKey.L => this.csvService.ToTablePage(csvLines, this.pageController.GetLastPage(), length).ToList(),
                _ => this.csvService.ToTable(csvLines).ToList()
            };
        }

        private static List<string> ReadCsvFile(string fileName)
        {
            return GetCsvLines().ToList();
        }

        // TODO extract this to an ArgumentService when it gets larger
        private (string fileName, int pageLength) ReadArguments(string[] args)
        {
            var file = args.Length >= 1 ? args[0] : "file-name not found";
            var pageLength = args.Length >= 2
                ? this.GetPageLength(args[1]) 
                : DefaultPageLength;

            return (file, pageLength);
        }


        private int GetPageLength(string pageLength)
        {
            var parsed = int.TryParse(pageLength, out var result);
            return parsed ? result : DefaultPageLength;
        }

        private static IEnumerable<string> GetCsvLines()
        {
            var line = 0;

            yield return "Name;Street;City;Age";
            yield return $"{line++} First row -- Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Last row -- Meier;Münchener Weg 1;87654 München;65";
        }
    }

    public class PageController
    {
        public PageController(int rowCount, int rowsOnPage) =>
            this.MaxPages = rowCount / rowsOnPage + 1;

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }


        public int GetFirstPage() => this.CurrentPage = 0;

        public int GetLastPage() => this.CurrentPage = this.MaxPages;


        public int GetPrevPage()
        {
            this.CurrentPage--;
            return this.CurrentPage = this.CurrentPage.LimitTo(0, this.MaxPages);
        }

        public int GetNextPage()
        {
            this.CurrentPage++;
            return this.CurrentPage = this.CurrentPage.LimitTo(0, this.MaxPages);
        }
    }
}