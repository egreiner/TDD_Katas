namespace Kata.Services.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}