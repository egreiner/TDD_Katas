namespace Kata.Services.Extensions
{
    public static class IntegerExtensions
    {
        // TODO not tested
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}