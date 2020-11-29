namespace Kata.UI.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            ////args = new[] { "bla" };
            ////args = new[] { "bla", "20" };

            var csvFileViewer = new CsvFileViewer.CsvFileViewer();
            csvFileViewer.Execute(args);
        }
    }
}