namespace Kata.Services.CsvFileViewer
{
    using System;

    public class LogInfo
    {
        public LogInfo(string text)
        {
            this.Text = text;
            this.Inserted = DateTime.Now;
        }

        public string Text { get; set; }
        public DateTime Inserted { get; set; }

        public override string ToString()
        {
            return $"{this.Inserted:mm.ss ffff} {this.Text}";
        }
    }
}