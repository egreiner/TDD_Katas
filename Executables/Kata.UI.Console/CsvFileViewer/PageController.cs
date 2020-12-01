namespace Kata.UI.Console.CsvFileViewer
{
    using Services.Extensions;

    public class PageController
    {
        public PageController(int rowCount, int rowsOnPage) =>
            this.MaxPages = rowCount / rowsOnPage + 1;

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }


        public int GetFirstPage() => this.CurrentPage = 0;

        public int GetLastPage() => this.CurrentPage = this.MaxPages;


        public int GetPrevPage()
        {
            this.CurrentPage--;
            return this.CurrentPage = this.CurrentPage.LimitTo(0, this.MaxPages);
        }

        public int GetNextPage()
        {
            this.CurrentPage++;
            return this.CurrentPage = this.CurrentPage.LimitTo(0, this.MaxPages);
        }
    }
}