namespace Kata.Services.RussianPeasantMultiplication
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class RussianPeasantMultiplicationServiceV2
    {
        public int Mul(int a, int b)
        {
            var list = GetListItems(a, b);
            return list.Where(x => x.left.IsOdd()).Sum(x => x.right);
        }

        private static IEnumerable<(int left, int right)> GetListItems(int a, int b)
        {
            // again SRP
            yield return (a, b);
            do
            {
                a /= 2;
                b *= 2;
                if (a >= 1) yield return (a, b);
            } while (a > 1);
        }
    }
}