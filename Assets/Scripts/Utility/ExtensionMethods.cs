using System;
using System.Collections;

namespace Utility
{
    public static class MyExtensions
    {
        public static T First<T>(this T[,] inputArray, Func<T, bool> predicate) where T : class
        {
            foreach (T x in inputArray)
                if (predicate(x))
                    return x;
            return null;
        }

        public static IEnumerable Where<T>(this T[,] inputArray, Func<T, bool> predicate) where T : class
        {
            foreach (T x in inputArray)
                if (predicate(x))
                    yield return x;
        }

        public static int Count<T>(this T[,] inputArray, Func<T, bool> predicate) where T : class
        {
            int count = 0;
            foreach (T x in inputArray.Where(predicate))
                count += 1;
            return count;
        }
    }
}