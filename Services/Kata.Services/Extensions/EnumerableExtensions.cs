namespace Kata.Services.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// The IEnumerable can be accessed at the specified index.
        /// </summary>
        /// <param name="enumerable">The IEnumerable.</param>
        /// <param name="index">The index.</param>
        public static bool CanAccess(this IEnumerable<string> enumerable, int index) =>
            enumerable?.Count() > index;
    }
}