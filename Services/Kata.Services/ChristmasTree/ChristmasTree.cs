namespace Kata.Services.ChristmasTree
{
    using System.Collections.Generic;

    public class ChristmasTree
    {
        public List<string> Draw(int height, bool withStarOnTop = false)
        {
            var result = DrawBranches(height);
            
            // add trunk
            result.Add(result[0]);

            // add star
            if (withStarOnTop)
                result.Insert(0, result[0].Replace("X","*"));

            return result;
        }

        private static List<string> DrawBranches(int height)
        {
            var result = new List<string>();

            for (var i = 0; i < height; i++)
            {
                var spaces = new string(' ', 4 - i);
                var x = new string('X', i * 2 + 1);
                result.Add(spaces + x + spaces);
            }

            return result;
        }
    }
}