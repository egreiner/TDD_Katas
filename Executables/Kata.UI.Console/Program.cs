namespace Kata.UI.Console
{
    using Services.CsvFileViewer;

    public class Program
    {
        static void Main(string[] args)
        {
            ////args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\leer.csv" };
            ////args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\personen.csv" };
            args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\besucher.csv" };
            ////args = new[] { "bla", "20" };

            var csvFileViewer = new CsvFileViewer.CsvFileViewer();
            csvFileViewer.Settings = GetCsvFileViewerSettings(args);
            csvFileViewer.Execute();
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