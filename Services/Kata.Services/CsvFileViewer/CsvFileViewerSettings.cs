namespace Kata.Services.CsvFileViewer
{
    public class CsvFileViewerSettings
    {
        public const int DefaultPageLength = 30;

        public string FileName { get; set; }
        public int PageLength  { get; set; }

        public int RecordsPerPage { get; set; } = 25;
    }
}