namespace Kata.Services.CsvFileViewer
{
    public class BulkInfo
    {
        public int BulkId  { get; set; }

        public int BulkStartPage { get; set; }
        public int BulkEndPage { get; set; }

        public int FileStartLine { get; set; }
        public int FileEndLine { get; set; }

        public int OffsetIndex { get; set; }
        public int OffsetStart { get; set; }

        public int LinesPerBulk { get; set; }


        /*
        * |<-------------------- file --------------------------->|
        *        |<----- bulk cache ----->|
        *           |<- page ->|
        */

        public static BulkInfo Create(in int pageNo, CsvFileViewerSettings settings)
        {
            var bulkPages    = settings.BulkReadPages;
            var pageLength   = settings.RecordsPerPage;
            var linesPerBulk = bulkPages * pageLength;
            var pageIdx      = pageNo - 1;

            var result           = new BulkInfo();
            result.BulkId        = pageIdx / bulkPages;
            result.BulkStartPage = result.BulkId * bulkPages + 1;
            result.BulkEndPage   = result.BulkStartPage + bulkPages - 1;
            result.LinesPerBulk  = pageLength * bulkPages;

            result.FileStartLine = result.BulkId * linesPerBulk + 1;
            result.FileEndLine   = result.FileStartLine + linesPerBulk;

            result.OffsetIndex = pageNo - result.BulkId * bulkPages - 1;
            result.OffsetStart = result.OffsetIndex * pageLength;
            
            return result;
        }
    }
}