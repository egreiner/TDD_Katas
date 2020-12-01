namespace Kata.Services.CsvFileViewer
{
    using Extensions;

    public class PaginationService
    {
        public PaginationService(int rowCount, int rowsOnPage)
        {
            var max = System.Math.Ceiling((decimal) rowCount / rowsOnPage);
            this.PageRange = (1, (int)max);
        }

        public (int Min, int Max) PageRange { get; }

        public int CurrentPage { get; private set; }

        
        public string PageInfo => $"Page {this.CurrentPage} of {this.PageRange.Max}";

        
        public override string ToString() => this.PageInfo;


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