﻿namespace Kata.UI.Console.CsvFileViewer
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
        private const string Footer = "[F]irst, [L]ast, [N]ext, [P]revious, [G][J]ump to page, e[X]it";

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
                
                writeLine($"{this.pagination.PageInfo} {this.csvFileService.ReadLocation}, {this.csvFileService.GetCachedPages()} pages cached");
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
            this.pagination     = new PaginationService(this.Settings.RecordsPerPage);
            this.csvFileService = new BulkCachedCsvFileService(this.Settings, this.pagination);
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

            if (GetJumpToPageKeys().Contains(key.Key))
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

            var csvLines = this.csvFileService.GetPageAsync(page).Result;

            return this.csvService.ToTablePage(csvLines, page, this.Settings.RecordsPerPage).ToList();
        }

        private static IEnumerable<ConsoleKey> GetAllowedKeys()
        {
            var result = new List<ConsoleKey>(GetNavigationKeys());
            result.AddRange(GetExitKeys());
            result.AddRange(GetJumpToPageKeys());
            return result;
        }

        private static IEnumerable<ConsoleKey> GetNavigationKeys() =>
            new[] { ConsoleKey.F, ConsoleKey.L, ConsoleKey.P, ConsoleKey.N };

        private static IEnumerable<ConsoleKey> GetExitKeys() =>
            new[] { ConsoleKey.X, ConsoleKey.Q, ConsoleKey.Escape };

        private static IEnumerable<ConsoleKey> GetJumpToPageKeys() =>
            new[] { ConsoleKey.J, ConsoleKey.G };
    }
}