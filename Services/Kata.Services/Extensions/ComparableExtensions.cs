namespace Kata.Services.Extensions
{
    using System;

    public static class ComparableExtensions
    {
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T> =>
            value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;

        public static bool IsBetween<T>(this T value, (T min, T max) range) where T : IComparable<T> =>
            value.IsBetween(range.min, range.max);


        public static T LimitTo<T>(this T value, T min, T max) where T : IComparable<T> =>
            value.LimitToMin(min).LimitToMax(max);

        public static T LimitToMin<T>(this T value, T min) where T : IComparable<T> =>
            value.CompareTo(min) > 0 ? value : min;

        public static T LimitToMax<T>(this T value, T max) where T : IComparable<T> =>
            value.CompareTo(max) > 0 ? max : value;
    }
}