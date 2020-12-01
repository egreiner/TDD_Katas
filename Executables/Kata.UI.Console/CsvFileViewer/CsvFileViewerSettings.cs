namespace Kata.UI.Console.CsvFileViewer
{
    public class CsvFileViewerSettings
    {
        public const int DefaultPageLength = 22;

        public string FileName { get; set; }
        public int PageLength  { get; set; }

        public int TableHeaderLength { get; set; } = 2;
        public int TableFooterLength { get; set; } = 2;


        public int RecordsPerPage =>
              this.PageLength
            - this.TableHeaderLength
            - this.TableFooterLength;
    }
}