namespace Kata.Services.Logger
{
    using System;

    public class LogInfo
    {
        public LogInfo(string text)
        {
            this.Text = text;
            this.Created = DateTime.Now;
        }


        public string Text { get; set; }
        
        public DateTime Created { get; set; }


        public override string ToString() =>
            $"{this.Created:mm.ss ffff} {this.Text}";
    }
}