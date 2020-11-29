﻿namespace Kata.UI.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private const int DefaultPageLength = 24;

        private static string footer = "[N]ext page, [P]revious page, [F]irst page, [L]ast page, e[X]it";

        // FEX use record from .Net 5...
        // or... extract this to an ArgumentDto
        private static (string fileName, int pageLength) arguments;

        static void Main(string[] args)
        {
            args = new[] { "bla" };
            ////args = new[] { "bla", "20" };

            arguments = ReadArguments(args);

            var csvLines = ReadCsvFile(arguments.fileName);
            var csvService = new Kata.Services.CsvTableizer.CsvTableizerService();
            var key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.X)
            {
                var lineCount = 0;
                var table     = csvService.ToTable(csvLines).ToList();

                foreach (var line in table)
                {
                    Console.WriteLine(line);
                    lineCount++;
                }

                PrintFootLine(lineCount);

                key = Console.ReadKey();
                Console.Clear();
            }
        }

        private static List<string> ReadCsvFile(string fileName)
        {
            return GetCsvLines().ToList();
        }

        // TODO extract this to an ArgumentService when it gets larger
        private static (string fileName, int pageLength) ReadArguments(string[] args)
        {
            var file = args.Length >= 1 ? args[0] : "file-name not found";
            var pageLength = args.Length >= 2
                ? GetPageLength(args[1]) 
                : DefaultPageLength;

            return (file, pageLength);
        }


        private static int GetPageLength(string pageLength)
        {
            var parsed = int.TryParse(pageLength, out var result);
            return parsed ? result : DefaultPageLength;
        }

        private static void PrintFootLine(int lineCount)
        {
            while (lineCount < arguments.pageLength -1)
            {
                lineCount++;
                Console.WriteLine(string.Empty);
            }
            Console.WriteLine(footer);
        }

        private static IEnumerable<string> GetCsvLines()
        {
            yield return "Name;Street;City;Age";
            yield return "Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return "Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return "Paul Meier;Münchener Weg 1;87654 München;65";
            yield return "Peter Pan;Am Hang 5;12345 Einsam;42";
            yield return "Maria Müller;Kölner Straße 45;50123 Köln;43";
            yield return "Paul Meier;Münchener Weg 1;87654 München;65";
        }
    }
}