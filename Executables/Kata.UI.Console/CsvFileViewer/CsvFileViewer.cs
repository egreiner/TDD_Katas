namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.CsvTableizer;

    public class CsvFileViewer
    {
        private const int DefaultPageLength = 24;

        private readonly CsvTableizerService csvService = new CsvTableizerService();
        private readonly string footer = "[N]ext page, [P]revious page, [F]irst page, [L]ast page, e[X]it";

        // FEX use record from .Net 5...
        // or... extract this to an ArgumentDto
        private (string fileName, int pageLength) arguments;

        public void Execute(string[] args)
        {
            args = new[] { "bla" };
            ////args = new[] { "bla", "20" };

            this.arguments = this.ReadArguments(args);
            var csvLines = ReadCsvFile(this.arguments.fileName);

            var key = new ConsoleKeyInfo('F', ConsoleKey.F, false, false, false);
            ConsoleKey[] keys = new[] {ConsoleKey.X, ConsoleKey.F, ConsoleKey.L};
            while (keys.Contains(key.Key))
            {
                // TODO paging...

                if (key.Key == ConsoleKey.X) break;

                var lineCount = 0;
                var table = this.GetTable(csvLines, key.Key);

                foreach (var line in table)
                {
                    Console.WriteLine(line);
                    lineCount++;
                }

                this.PrintFootLine(lineCount);

                key = Console.ReadKey();
                Console.Clear();
            }
        }

        private List<string> GetTable(List<string> csvLines, ConsoleKey key)
        {
            var length = this.arguments.pageLength - 2;

            return key switch
            {
                ConsoleKey.F => this.csvService.ToTableFirstPage(csvLines, length).ToList(),
                ConsoleKey.L => this.csvService.ToTableLastPage(csvLines, length).ToList(),
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

        private void PrintFootLine(int lineCount)
        {
            while (lineCount < this.arguments.pageLength -1)
            {
                lineCount++;
                Console.WriteLine(string.Empty);
            }
            Console.WriteLine(this.footer);
        }

        private static IEnumerable<string> GetCsvLines()
        {
            var line = 0;

            yield return "Name;Street;City;Age";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Name;Street;City;Age";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Name;Street;City;Age";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Name;Street;City;Age";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Name;Street;City;Age";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
            yield return $"{line++} Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return $"{line++} Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return $"{line++} Paul Meier;Münchener Weg 1;87654 München;65";
        }
    }
}