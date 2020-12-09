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

        public int BulkReadPages { get; set; } = 10;


        // TODO wrong location ...
        public (int start, int end, int offset, int length) GetBulkBlockInfo(in int pageNo)
        {
            var start = ((pageNo - 1) / this.BulkReadPages) * this.BulkReadPages;
            var end = start +  this.BulkReadPages;
            var offset = pageNo - start;
            start++;
            var length = this.PageLength * this.BulkReadPages;
            return (start, end, offset, length);
        }
    }
}