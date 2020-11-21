namespace Kata.Services.ChristmasTree
{
    using System.Collections.Generic;

    public class ChristmasTree
    {
        private readonly List<string> tree = new List<string>();


        public List<string> Draw(int height, bool withStarOnTop = false)
        {
            this.AddBranches(height);
            this.AddTrunk();
            this.AddStar(withStarOnTop);
            return this.tree;
        }

        private void AddStar(bool withStarOnTop)
        {
            if (withStarOnTop)
                this.tree.Insert(0, this.tree[0].Replace("X", "*"));
        }

        private void AddBranches(int height)
        {
            for (var i = 0; i < height; i++)
            {
                var spaces = new string(' ', 4 - i);
                var x      = new string('X', i * 2 + 1);
                this.tree.Add(spaces + x + spaces);
            }
        }

        private void AddTrunk() =>
            this.tree.Add(this.tree[0]);
    }
}