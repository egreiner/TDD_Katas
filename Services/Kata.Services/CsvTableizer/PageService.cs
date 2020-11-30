namespace Kata.Services.CsvTableizer
{
    using Extensions;

    public class PageService
    {
        public PageService(int rowCount, int rowsOnPage)
        {
            var max = System.Math.Ceiling((decimal) rowCount / rowsOnPage);
            this.PageRange = (1, (int)max);
        }

        public (int Min, int Max) PageRange { get; }

        public int CurrentPage { get; private set; }

        
        public string PageInfo => $"Page {this.CurrentPage} of {this.PageRange.Max}";

        
        public override string ToString() => this.PageInfo;


        public int GetFirstPage() => this.GetPage(this.PageRange.Min);

        public int GetLastPage() => this.GetPage(this.PageRange.Max);

        public int GetPage(int page) => this.CurrentPage = page.LimitTo(this.PageRange.Min, this.PageRange.Max);


        public int GetPrevPage()
        {
            this.CurrentPage--;
            return this.CurrentPage = this.CurrentPage.LimitTo(this.PageRange.Min, this.PageRange.Max);
        }

        public int GetNextPage()
        {
            this.CurrentPage++;
            return this.CurrentPage = this.CurrentPage.LimitTo(this.PageRange.Min, this.PageRange.Max);
        }
    }
}