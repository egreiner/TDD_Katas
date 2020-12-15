namespace Kata.Services.CsvFileViewer
{
    public class CsvFileViewerSettings
    {
        public const int DefaultPageLength = 22;


        public CsvFileViewerSettings(int pageLength, int maxCachedPages)
        {
            this.PageLength     = pageLength;
            this.MaxCachedPages = maxCachedPages;
        }
        

        public string FileName { get; set; }

        public int PageLength  { get; set; }

        public int RecordsPerPage { get; set; } = 10;

        public int MaxCachedPages { get; set; }
        public int BulkReadPages  { get; set; } = 100;
    }
}