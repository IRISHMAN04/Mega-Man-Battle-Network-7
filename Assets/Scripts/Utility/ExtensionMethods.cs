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
    }
}