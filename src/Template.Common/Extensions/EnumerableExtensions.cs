using System.Collections.Generic;
using System.Linq;

namespace Template.Common.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Breaks down an IEnumerable<T> into smaller pieces of determined size and returns them as a IEnumerable<IEnumerable<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((item, index) => new { Index = index, Value = item })
                .GroupBy(item => item.Index / chunkSize)
                .Select(item => item.Select(newItem => newItem.Value));
        }
    }
}
