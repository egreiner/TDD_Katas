namespace Kata.Services.CsvFileViewer
{
    using Extensions;

    public class PaginationService
    {
        private bool maxPageEstimated;

        /// <summary>
        /// In case of large files, the length of the file
        /// is not known at the start.
        /// </summary>
        /// <param name="rowsOnPage">How many records should be displayed on one screen page.</param>
        public PaginationService(int rowsOnPage) =>
            this.SetPageRangeEstimated(0, rowsOnPage);

        /// <summary>
        /// In case of the length of the data source is known at the start.
        /// </summary>
        /// <param name="rowCount">Line quantity of the data source.</param>
        /// <param name="rowsOnPage">How many records should be displayed on one screen page.</param>
        public PaginationService(long rowCount, int rowsOnPage) =>
            this.SetPageRangeEstimated(rowCount, rowsOnPage);


        public (int Min, int Max) PageRange { get; private set; }

        public int CurrentPage { get; private set; } = 1;

        
        public string PageInfo
        {
            get
            {
                var max = this.maxPageEstimated ? "?" : this.PageRange.Max.ToString();
                return $"Page {this.CurrentPage} of {max}";
            }
        }


        public override string ToString() => this.PageInfo;


        public void SetPageRangeEstimated(long rowCount, int rowsOnPage) =>
            this.SetPageRange(rowCount, rowsOnPage, true);

        public void SetRealPageRange(long rowCount, int rowsOnPage) =>
            this.SetPageRange(rowCount, rowsOnPage, false);


        public int GetFirstPage() => this.GetPage(this.PageRange.Min);
        public int GetLastPage()  => this.GetPage(this.PageRange.Max);
        public int GetPrevPage()  => this.GetPage(--this.CurrentPage);
        public int GetNextPage()  => this.GetPage(++this.CurrentPage);

        public int GetPage(int page) =>
            this.CurrentPage = page.LimitTo(this.PageRange.Min, this.PageRange.Max);


        private void SetPageRange(long rowCount, int rowsOnPage, bool rowCountEstimated)
        {
            this.maxPageEstimated = rowCountEstimated;
            var max = System.Math.Ceiling((decimal)rowCount / rowsOnPage);
            this.PageRange = (this.CurrentPage, (int)max);
        }
    }
}