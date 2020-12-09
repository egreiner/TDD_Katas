namespace Kata.UI.Console.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Services.CsvFileViewer;
    using Services.CsvTableizer;
    using Services.Logger;


    public class CsvFileViewer
    {
        private const string Footer = "[A]ll, [N]ext, [P]revious, [F]irst, [L]ast, [G][J]ump to page, e[X]it";

        private readonly CsvTableizerService csvService = new CsvTableizerService(true);

        private BulkCachedCsvFileService csvFileService;
        private PaginationService pagination;
        private int gotoPage = 1;
        private bool initializedImportantPages;


        public CsvFileViewerSettings Settings { get; set; }
        

        public void Execute(CsvFileViewerSettings settings)
        {
            this.Settings = settings;
            this.InitializeServices();

            int lineCount;
            var key = new ConsoleKeyInfo('F', ConsoleKey.F, false, false, false);
            while (!GetExitKeys().Contains(key.Key))
            {
                Console.Clear();

                lineCount = 0;
                var table = this.GetTable(key.Key);

                foreach (var line in table) 
                    writeLine(line);

                printFootLine();

                key = this.ReadConsoleInput(key);

                if (!this.initializedImportantPages) 
                    Task.Run(this.ReadAheadImportantPages);
            }

            this.PrintLog();
            Console.ReadKey();

            void printFootLine()
            {
                while (lineCount < this.Settings.PageLength-1) 
                    writeLine(string.Empty);
                
                writeLine($"{this.pagination.PageInfo} {this.csvFileService.ReadLocation}");
                writeLine(Footer);
                writeLine($"Last key pressed: {key.Key}");
            }

            void writeLine(string line)
            {
                Console.WriteLine(line);
                lineCount++;
            }
        }

        private void PrintLog()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Logged events:");
            foreach (var logInfo in Log.OrderedLogInfos) 
                Console.WriteLine(logInfo);
        }

        private void InitializeServices()
        {
            var cacheSettings   = new PageCacheSettings(this.Settings.RecordsPerPage, 100);
            this.pagination     = new PaginationService(this.Settings.RecordsPerPage);
            this.csvFileService = new BulkCachedCsvFileService(this.Settings.FileName, cacheSettings, this.pagination);
        }

        private void ReadAheadImportantPages() =>
            Task.Run(() =>
            {
                Log.Add("ReadAheadImportantPages");
                _ = this.csvFileService.SetRealFileLength();
                this.initializedImportantPages = true;
            });


        private ConsoleKeyInfo ReadConsoleInput(ConsoleKeyInfo key)
        {
            var lastKey = key;
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.J || key.Key == ConsoleKey.G)
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


        private IEnumerable<string> GetTable(ConsoleKey key)
        {
            var page = key switch
            {
                ConsoleKey.F => this.pagination.GetFirstPage(),
                ConsoleKey.P => this.pagination.GetPrevPage(),
                ConsoleKey.N => this.pagination.GetNextPage(),
                ConsoleKey.L => this.pagination.GetLastPage(),
                ConsoleKey.G => this.pagination.GetPage(this.gotoPage),
                ConsoleKey.J => this.pagination.GetPage(this.gotoPage),
                _ => -1
            };

            var pageNo = this.pagination.CurrentPage;
            var csvLines = this.csvFileService.GetPageAsync(pageNo).Result;

            return this.csvService.ToTable(csvLines).ToList();
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
                ConsoleKey.G,
            };
            result.AddRange(GetExitKeys());
            return result;
        }

        private static IEnumerable<ConsoleKey> GetExitKeys() =>
            new[] { ConsoleKey.X, ConsoleKey.Escape };
    }
}