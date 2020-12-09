namespace Kata.UI.Console
{
    using Services.CsvFileViewer;

    public class Program
    {
        static void Main(string[] args)
        {
            var dir = @"C:\DataServer\Developer\In523EasySteps\TDD_Kata\SolutionItems\";
            ////args = new[] { $@"{dir}CSVViewer\leer.csv" };
            ////args = new[] { $@"{dir}CSVViewer\personen.csv" };
            ////args = new[] { $@"{dir}CSVViewer\besucher.csv" };             // 1.000
            args = new[] { $@"{dir}CSVViewer\besucherLarge.csv" };        // 10.000
            ////args = new[] { $@"{dir}LargeCsvFiles\besucherBig.csv" };          // 100.000
            ////args = new[] { $@"{dir}LargeCsvFiles\besucherHugh.csv" };         // 1.000.000
            ////args = new[] { $@"{dir}LargeCsvFiles\besucherMonster.csv" };      // 10.000.000
            ////args = new[] { "bla", "20" };

            var csvFileViewer = new CsvFileViewer.CsvFileViewer();
            csvFileViewer.Execute(GetCsvFileViewerSettings(args));
        }

        private static CsvFileViewerSettings GetCsvFileViewerSettings(string[] args)
        {
            var result = new CsvFileViewerSettings();
            result.FileName = args.Length >= 1 ? args[0] : "file-name not found";
            result.PageLength = args.Length >= 2
                ? GetPageLength(args[1])
                : CsvFileViewerSettings.DefaultPageLength;

            return result;
        }

        private static int GetPageLength(string pageLength)
        {
            var parsed = int.TryParse(pageLength, out var result);
            return parsed ? result : CsvFileViewerSettings.DefaultPageLength;
        }
    }
}