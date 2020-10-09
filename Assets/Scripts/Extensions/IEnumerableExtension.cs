using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class IEnumerableExtension
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            int idx;
            return Random<T>(enumerable, out idx);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, out int idx)
        {
            return Random<T>(enumerable, out idx, null);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, T elementToExclude)
        {
            return Random<T>(enumerable, new T[] { elementToExclude });
        }

        public static T Random<T>(this IEnumerable<T> enumerable, out int idx, T elementToExclude)
        {
            return Random<T>(enumerable, out idx, new T[] { elementToExclude });
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<T> elementsToExclude)
        {
            int idx;
            return Random<T>(enumerable, out idx, elementsToExclude);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, out int idx, IEnumerable<T> elementsToExclude)
        {
            idx = -1;

            var enumerableAfterExclude = elementsToExclude != null ? enumerable.Except(elementsToExclude) : enumerable;

            if (!enumerableAfterExclude.Any())
            {
                return default;
            }

            idx = UnityEngine.Random.Range(0, enumerableAfterExclude.Count());
            return enumerableAfterExclude.ElementAtOrDefault(idx);
        }
    }
}
