using System;
using System.Collections.Generic;

namespace Maverick.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// .ForEach for IEnumerable. 
        /// this is bad but I did it anyway
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="Do"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForTrill<T> (this IEnumerable<T> Source, Action<T> Do)
        {
            foreach (var Item in Source)
            {
                Do(Item);
            }
            return Source;
        }
    }
}