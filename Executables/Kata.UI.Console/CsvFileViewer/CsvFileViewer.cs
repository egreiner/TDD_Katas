namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.CsvTableizer;

    public class CsvFileViewer
    {
        // FEX use record from .Net 5...
        // or... extract this to an ArgumentDto
        private readonly (string FileName, int PageLength) settings;
        private readonly CsvTableizerService csvService = new CsvTableizerService();
        private readonly ConsoleKey[] allowedKeys = new[] { ConsoleKey.A, ConsoleKey.X, ConsoleKey.F, ConsoleKey.L, ConsoleKey.N, ConsoleKey.P };
        private readonly string footer = "[N]ext page, [P]revious page, [F]irst page, [L]ast page, e[X]it";
        private  readonly CsvFileService csvFileService = new CsvFileService();

        private PageController pageController;


        public CsvFileViewer((string fileName, int pageLength) settings) =>
            this.settings = settings;


        public void Execute()
        {
            int lineCount;
            var csvLines = this.csvFileService.ReadFile(this.settings.FileName);
            this.pageController = new PageController(csvLines.Count, this.settings.PageLength -2);

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
                while (lineCount <= this.settings.PageLength) 
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

        private IEnumerable<string> GetTable(IList<string> csvLines, ConsoleKey key)
        {
            var length = this.settings.PageLength - 2;

            return key switch
            {
                ConsoleKey.F => this.csvService.ToTablePage(csvLines, this.pageController.GetFirstPage(), length).ToList(),
                ConsoleKey.P => this.csvService.ToTablePage(csvLines, this.pageController.GetPrevPage(), length).ToList(),
                ConsoleKey.N => this.csvService.ToTablePage(csvLines, this.pageController.GetNextPage(), length).ToList(),
                ConsoleKey.L => this.csvService.ToTablePage(csvLines, this.pageController.GetLastPage(), length).ToList(),
                _ => this.csvService.ToTable(csvLines).ToList()
            };
        }
    }
}