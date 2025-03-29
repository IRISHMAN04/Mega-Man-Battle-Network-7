using System;
using System.Collections;
using UnityEngine;

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

        public static void ReverseChildren(this Transform transform)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount / 2; i++)
            {
                Transform first = transform.GetChild(i);
                Transform last = transform.GetChild(childCount - 1 - i);

                int firstIndex = first.GetSiblingIndex();
                int lastIndex = last.GetSiblingIndex();

                first.SetSiblingIndex(lastIndex);
                last.SetSiblingIndex(firstIndex);
            }
        }
    }
}