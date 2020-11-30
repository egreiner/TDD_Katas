namespace Kata.UI.Console
{
    public class Program
    {

        private const int DefaultPageLength = 22;


        static void Main(string[] args)
        {
            args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\leer.csv" };
            ////args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\personen.csv" };
            ////args = new[] { @"C:\DataServer\Developer\In523EasySteps\TDD_Katas\SolutionItems\CSVViewer\besucher.csv" };
            ////args = new[] { "bla", "20" };

            var csvFileViewer = new CsvFileViewer.CsvFileViewer(CsvFileViewerSettings(args));
            csvFileViewer.Execute();
        }


        // TODO extract this to an ArgumentService when it gets larger
        private static (string fileName, int pageLength) CsvFileViewerSettings(string[] args)
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
    }
}