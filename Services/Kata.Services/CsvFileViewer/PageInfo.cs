namespace Kata.Services.CsvFileViewer
{
    public class PageInfo
    {
        private readonly int hashCode;


        public PageInfo(int pageNo)
        {
            this.PageNo   = pageNo;
            this.hashCode = pageNo.GetHashCode();
        }


        public int PageNo { get; }

        
        public override string ToString() =>
            this.PageNo.ToString();

        public override bool Equals(object obj) =>
            obj is PageInfo x && x.PageNo == this.PageNo;

        public override int GetHashCode() => this.hashCode;
    }
}