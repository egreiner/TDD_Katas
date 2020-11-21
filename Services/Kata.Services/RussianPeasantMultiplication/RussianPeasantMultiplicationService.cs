namespace Kata.Services.RussianPeasantMultiplication
{
    using Extensions;

    public class RussianPeasantMultiplicationService
    {
        public int Mul(int a, int b)
        {
            var left  = a;
            var right = b;
            var sum = left.IsEven() ? 0 : right;

            while (left > 1)
            {
                left /= 2;
                right *= 2;
                sum += left.IsEven() ? 0 : right;
            }
            return sum;
        }
    }
}