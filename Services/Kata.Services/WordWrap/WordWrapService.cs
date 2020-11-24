namespace Kata.Services.WordWrap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Extensions;

    public class WordWrapService
    {
        private const string LineFeedReplacer = " .~-{LiNeFeeEd}-~. ";

        private readonly StringBuilder builder = new StringBuilder();
        

        public string WordWrap(string text, int lineLengthLimit)
        {
            text = ReplaceLineFeeds(text);
            var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            return this.WordWrap(words, lineLengthLimit);
        }


        private static string ReplaceLineFeeds(string text) =>
            text.Replace(System.Environment.NewLine, LineFeedReplacer);


        private string WordWrap(IReadOnlyList<string> words, int limit)
        {
            this.builder.Clear();
            for (var i = 0; i < words.Count; i++) 
                this.builder.AppendLine(ConcatenateWords(words, limit, ref i));

            return FinalizeText(this.builder.ToString());
        }

        private static string ConcatenateWords(IReadOnlyList<string> words, int limit, ref int i)
        {
            var line = words[i];
            if (line.IsLonger(limit) || wordIsLineFeed(i + 1)) return line;

            while (words.CanAccess(i + 1) && 
                   lineWithWordAdded(i + 1).FitsInto(limit) && 
                   !wordIsLineFeed(i + 1))
                line = lineWithWordAdded(++i);

            line = line.Replace(LineFeedReplacer.Trim(), "");

            return line.Trim();

            string lineWithWordAdded(int index) => $"{line} {words[index].Trim()}";
            bool wordIsLineFeed(int index) => words.CanAccess(index) && words[index].Trim() == LineFeedReplacer.Trim();
        }

        private static string FinalizeText(string result)
        {
            result = result.Replace(LineFeedReplacer.Trim(), "");
            return result.EndsWith("\r\n")
                ? result[..^2]
                : result;
        }
    }


    // local extensions
    // this is too specific, don't put this to the public Extensions
    public static class StringExtensions
    {
        /// <summary>
        /// The text length is shorter or equal than the specified length limit + offset.
        /// Linefeed breaks correct wrapping...
        /// Have to take a break...
        /// </summary>
        /// <param name="text">The text that should be verified.</param>
        /// <param name="lengthLimit">The length limit.</param>
        /// <param name="offset">The offset added to the length limit.</param>
        public static bool FitsInto(this string text, int lengthLimit, int offset = 0) =>
            text?.Length <= lengthLimit + offset;

        /// <summary>
        /// The text length is longer than the specified length limit + offset.
        /// </summary>
        /// <param name="text">The text that should be verified.</param>
        /// <param name="lengthLimit">The length limit.</param>
        /// <param name="offset">The offset added to the length limit.</param>
        public static bool IsLonger(this string text, int lengthLimit, int offset = 0) =>
            text?.Length > lengthLimit + offset;
    }
}