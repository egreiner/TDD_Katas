﻿namespace Kata.Services.CsvFileViewer
{
    using System.Globalization;
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
            this.SetRealPageRange(rowCount, rowsOnPage);


        public (int Min, int Max) PageRange { get; private set; }

        public int CurrentPage { get; private set; } = 1;

        
        public string PageInfo
        {
            get
            {
                var culture = CultureInfo.GetCultureInfo("de-de");
                var max = this.maxPageEstimated 
                    ? "?" 
                    : this.PageRange.Max.ToString("N0", culture);
                return $"Page {this.CurrentPage.ToString("N0", culture)} of {max}";
            }
        }


        public override string ToString() => this.PageInfo;


        public (int min, int max) GetLimitedPageRange(in int min, in int max) =>
            (min.LimitToMin(this.PageRange.Min), max.LimitToMax(this.PageRange.Max));

        public int GetLimitedPageNo(int pageNo) =>
            pageNo.LimitTo(this.PageRange.Min, this.PageRange.Max);


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
            this.PageRange = (1, (int)max);
        }
    }
}