namespace Kata.Services.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceUmlauts(this string value) =>
            value.Replace("Ä", "Ae")
                 .Replace("Ö", "Oe")
                 .Replace("Ü", "Ue")
                 .Replace("ä", "ae")
                 .Replace("ö", "oe")
                 .Replace("ü", "ue")
                 .Replace("ß", "ss");
    }
}