namespace Kata.Services.CsvFileViewer
{
    using System;

    public class PageInfo
    {
        private readonly int hashCode;


        public PageInfo(int pageNo, int priority = 0)
        {
            this.PageNo   = pageNo;
            this.Priority = priority;
            this.hashCode = pageNo.GetHashCode();
            
            this.Created = DateTime.Now;
        }


        public int PageNo { get; }

        public int Priority { get; }

        public int FetchCount { get; set; }

        public DateTime Fetched { get; set; }
        public DateTime Created { get; }


        public override string ToString() =>
            this.PageNo.ToString();

        public override bool Equals(object obj) =>
            obj is PageInfo x && x.PageNo == this.PageNo;

        public override int GetHashCode() => this.hashCode;
    }
}