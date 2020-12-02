namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.CsvFileViewer;
    using Services.CsvTableizer;


    public class CsvFileViewer
    {
        private const string Footer = "[A]ll, [N]ext, [P]revious, [F]irst, [L]ast, [J]ump to page, e[X]it";

        private readonly CsvTableizerService csvService = new CsvTableizerService(true);
        private readonly CsvFileService csvFileService = new CsvFileService();

        private PaginationService pagination;
        private int gotoPage;

        
        public CsvFileViewerSettings Settings { get; set; }
        

        public void Execute()
        {
            int lineCount;
            var csvLines = this.csvFileService.ReadSmallFile(this.Settings.FileName);

            // assume that the first line is the column-labels
            this.pagination = new PaginationService(csvLines.Count - 1, this.Settings.RecordsPerPage);

            var key = new ConsoleKeyInfo('F', ConsoleKey.F, false, false, false);
            while (!GetExitKeys().Contains(key.Key))
            {
                Console.Clear();

                lineCount = 0;
                var table = this.GetTable(csvLines, key.Key);

                foreach (var line in table) 
                    writeLine(line);

                printFootLine();

                key = this.ReadConsoleInput(key);
            }

            void printFootLine()
            {
                while (lineCount < this.Settings.PageLength-1) 
                    writeLine(string.Empty);
                
                writeLine(this.pagination.PageInfo);
                writeLine(Footer);
                writeLine($"Last key pressed: {key.Key}");
            }

            void writeLine(string line)
            {
                Console.WriteLine(line);
                lineCount++;
            }
        }


        private ConsoleKeyInfo ReadConsoleInput(ConsoleKeyInfo key)
        {
            var lastKey = key;
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.J)
                this.gotoPage = this.GetGotoPage(Console.ReadLine());

            if (!GetAllowedKeys().Contains(key.Key))
                key = lastKey;
            
            return key;
        }

        private int GetGotoPage(string page)
        {
            var parsed = int.TryParse(page, out var result);
            return parsed ? result : this.pagination.CurrentPage;
        }


        private IEnumerable<string> GetTable(IList<string> csvLines, ConsoleKey key)
        {
            var page = key switch
            {
                ConsoleKey.F => this.pagination.GetFirstPage(),
                ConsoleKey.P => this.pagination.GetPrevPage(),
                ConsoleKey.N => this.pagination.GetNextPage(),
                ConsoleKey.L => this.pagination.GetLastPage(),
                ConsoleKey.J => this.pagination.GetPage(this.gotoPage),
                _ => -1
            };

            return page > 0 
                ? this.csvService.ToTablePage(csvLines, page, this.Settings.RecordsPerPage).ToList() 
                : this.csvService.ToTable(csvLines).ToList();
        }

        private static IEnumerable<ConsoleKey> GetAllowedKeys()
        {
            var result = new List<ConsoleKey>
            {
                ConsoleKey.A,
                ConsoleKey.F,
                ConsoleKey.L,
                ConsoleKey.N,
                ConsoleKey.P,
                ConsoleKey.J,
            };
            result.AddRange(GetExitKeys());
            return result;
        }

        private static IEnumerable<ConsoleKey> GetExitKeys() =>
            new[] { ConsoleKey.X, ConsoleKey.Escape };
    }
}