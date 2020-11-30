namespace Kata.Services.CsvTableizer
{
    using Extensions;

    public class PageController
    {
        public PageController(int rowCount, int rowsOnPage) =>
            this.MaxPage = rowCount / rowsOnPage + 1;

        public int MinPage { get; }

        public int MaxPage { get; }

        public int CurrentPage { get; set; }

        
        public string PageInfo => $"Page {this.CurrentPage} of {this.MaxPage}";

        
        public override string ToString() => this.PageInfo;


        public int GetFirstPage() => this.GetPage(0);

        public int GetLastPage() => this.GetPage(this.MaxPage);

        public int GetPage(int page) => this.CurrentPage = page.LimitTo(this.MinPage, this.MaxPage);


        public int GetPrevPage()
        {
            this.CurrentPage--;
            return this.CurrentPage = this.CurrentPage.LimitTo(this.MinPage, this.MaxPage);
        }

        public int GetNextPage()
        {
            this.CurrentPage++;
            return this.CurrentPage = this.CurrentPage.LimitTo(this.MinPage, this.MaxPage);
        }
    }
}