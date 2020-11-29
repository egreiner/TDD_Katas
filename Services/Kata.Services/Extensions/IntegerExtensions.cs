namespace Kata.Services.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static int LimitTo(this int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}