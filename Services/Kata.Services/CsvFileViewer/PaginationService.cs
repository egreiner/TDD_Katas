namespace Kata.Services.CsvFileViewer
{
    using Extensions;

    public class PaginationService
    {
        public PaginationService(int rowCount, int rowsOnPage) =>
            this.InitializePageRange(rowCount, rowsOnPage);


        public (int Min, int Max) PageRange { get; private set; }

        public int CurrentPage { get; private set; } = 1;

        
        public string PageInfo
        {
            get
            {
                var max = this.PageRange.Max > 0
                    ? this.PageRange.Max.ToString()
                    : "?";
                return $"Page {this.CurrentPage} of {max}";
            }
        }


        public override string ToString() => this.PageInfo;


        public void InitializePageRange(int rowCount, int rowsOnPage)
        {
            var max = System.Math.Ceiling((decimal)rowCount / rowsOnPage);
            this.PageRange = (this.CurrentPage, (int)max);
        }


        public int GetFirstPage() =>
            this.GetPage(this.PageRange.Min);

        public int GetLastPage() =>
            this.GetPage(this.PageRange.Max);

        public int GetPrevPage() =>
            this.GetPage(--this.CurrentPage);

        public int GetNextPage() =>
            this.GetPage(++this.CurrentPage);

        public int GetPage(int page) =>
            this.CurrentPage = page.LimitTo(this.PageRange.Min, this.PageRange.Max);
    }
}