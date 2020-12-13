namespace Kata.Services.CsvFileViewer
{
    public class BulkInfo
    {
        /*
         * |<-------------------- file --------------------------->|
         *        |<----- bulk cache ----->|
         *           |<- page ->|
         */

        public int BulkId  { get; set; }

        public int StartPage  { get; set; }
        public int EndPage    { get; set; }

        public int OffsetIndex { get; set; }
        public int OffsetStart { get; set; }

        public int LinesPerBulk { get; set; }


        public static BulkInfo Create(in int pageNo, PageCacheSettings settings)
        {
            var bulkPages  = settings.BulkReadPages;
            var pageLength = settings.PageLength;
            var pageIdx    = pageNo - 1;

            var result          = new BulkInfo();
            result.BulkId       = pageIdx / bulkPages;
            result.StartPage    = result.BulkId * bulkPages;
            result.EndPage      = result.StartPage + bulkPages;
            result.LinesPerBulk = pageLength * bulkPages;

            ////result.StartPage++;

            // offset within the bulk-cached lines
            result.OffsetIndex = pageIdx - result.StartPage;
            result.OffsetStart = result.OffsetIndex * pageLength;
            
            return result;
        }
    }
}