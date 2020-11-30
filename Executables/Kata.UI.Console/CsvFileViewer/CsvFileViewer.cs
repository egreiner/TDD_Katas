namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.CsvTableizer;

    public class CsvFileViewer
    {
        private readonly string footer = "[A]ll, [N]ext, [P]revious, [F]irst, [L]ast, [J]ump to page, e[X]it";

        // FEX use record from .Net 5...
        // or... extract this to an ArgumentDto
        private readonly (string FileName, int PageLength) settings;
        
        private readonly CsvTableizerService csvService = new CsvTableizerService();
        private  readonly CsvFileService csvFileService = new CsvFileService();

        private PageService pageController;


        public CsvFileViewer((string fileName, int pageLength) settings) =>
            this.settings = settings;


        public void Execute()
        {
            int lineCount;
            var csvLines = this.csvFileService.ReadFile(this.settings.FileName);
            this.pageController = new PageService(csvLines.Count, this.settings.PageLength -2);

            var key = new ConsoleKeyInfo('F', ConsoleKey.F, false, false, false);
            while (key.Key != ConsoleKey.X && key.Key != ConsoleKey.Escape)
            {
                Console.Clear();

                lineCount = 0;
                var table = this.GetTable(csvLines, key.Key);

                foreach (var line in table) 
                    writeLine(line);

                printFootLine();

                key = this.ReadKey(key);
            }

            void printFootLine()
            {
                while (lineCount < this.settings.PageLength-1) 
                    writeLine(string.Empty);
                
                writeLine(this.pageController.PageInfo);
                writeLine(this.footer);
                writeLine($"Last key pressed: {key.Key}");
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
            if (!GetAllowedKeys().Contains(key.Key))
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
                ConsoleKey.J => this.csvService.ToTablePage(csvLines, this.pageController.GetPage(3), length).ToList(),
                _ => this.csvService.ToTable(csvLines).ToList()
            };
        }
        
        private static ConsoleKey[] GetAllowedKeys() =>
            new[]
            {
                ConsoleKey.A,
                ConsoleKey.F,
                ConsoleKey.L,
                ConsoleKey.N,
                ConsoleKey.P,
                ConsoleKey.J,
                ConsoleKey.X, ConsoleKey.Escape
            };
    }
}