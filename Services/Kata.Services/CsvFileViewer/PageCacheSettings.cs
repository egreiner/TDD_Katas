namespace Kata.Services.CsvFileViewer
{
    public class PageCacheSettings
    {
        public PageCacheSettings(int pageLength, int maxCachedPages)
        {
            this.PageLength = pageLength;
            this.MaxCachedPages = maxCachedPages;
        }


        public bool DisableCache  { get; set; }

        public int PageLength     { get; set; }
        public int MaxCachedPages { get; set; }

        public int ReadAheadNextPages { get; set; } = 2;
        public int ReadAheadPrevPages { get; set; } = 2;
        
        // FEX
        ////public int BulkReadPages { get; }
    }
}