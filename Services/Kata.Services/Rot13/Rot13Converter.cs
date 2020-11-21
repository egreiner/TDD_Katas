namespace Kata.Services.Rot13
{
    using System;
    using System.Linq;

    public class Rot13Converter
    {
        public Rot13Converter(int rotFactor = 13, bool encodeDigits = false)
        {
            if (encodeDigits)
                this.SourcePattern = $"0123456789{this.SourcePattern}";
            this.TargetPattern = this.SourcePattern[rotFactor..] + this.SourcePattern[..rotFactor];
        }


        // ReSharper disable once StringLiteralTypo
        public string SourcePattern { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string TargetPattern { get; }


        public string Convert(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            return string.Concat(
                    text.ToUpper().ToCharArray()
                        .Select(c => this.ConvertChar(c.ToString())));
        }


        private string ConvertChar(string c) =>
            this.SourcePattern.Contains(c)
                ? this.GetRotated(c)
                : c;

        private string GetRotated(string c)
        {
            var index = this.SourcePattern.IndexOf(c, StringComparison.InvariantCultureIgnoreCase);
            return this.TargetPattern.Substring(index, 1);
        }
    }
}