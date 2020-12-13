namespace Kata.Services.CsvFileViewer
{
    public class PageCacheSettings
    {
        public PageCacheSettings(int pageLength, int maxCachedPages)
        {
            this.PageLength = pageLength;
            this.MaxCachedPages = maxCachedPages;
        }


        public int PageLength     { get; set; }
        public int MaxCachedPages { get; set; }

        public int ReadAheadPages { get; set; } = 5;

        public int BulkReadPages { get; set; } = 100;
    }
}