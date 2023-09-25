using System;
using System.Collections.Generic;
using System.Linq;

namespace PetMeetApp.Common.Extensions
{
    public static class EnumerableExtension
    {

        #region Public methods

        public static IEnumerable<TResult> Cast<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> action)
        {
            if (source == null) return null;

            if (action == null) throw new ArgumentNullException(nameof(action));

            return CastIterator(source, action);
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>true if the source sequence contains any elements; otherwise, false. If source is null, System.ArgumentNullException is swollen and false is returned.</returns>
        public static bool SafeAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate = null)
        {
            if (source == null) return false;

            if(predicate != null)
            {
                return source.Any(predicate);
            }
            else
            {
                return source.Any();
            }
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function. If source is null, System.ArgumentNullException is swollen and null value is returned.</returns>
        public static int? SafeCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate = null)
        {
            if (source == null) return null;

            if (predicate != null)
            {
                return source.Count(predicate);
            }
            else
            {
                return source.Count();
            }
        }

        public static List<TSource> SafeToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) return null;

            return source.ToList();
        }

        #endregion

        #region Private methods

        private static IEnumerable<TResult> CastIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> action)
        {
            foreach (var item in source)
            {
                yield return action(item);
            }
        }

        #endregion
    }
}
