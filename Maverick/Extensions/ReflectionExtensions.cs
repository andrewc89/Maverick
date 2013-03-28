using System;
using System.Linq;
using System.Collections;

namespace Maverick.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// checks if Type is IEnumerable or inherits from IEnumerable
        /// </summary>
        /// <param name="Type">Type to check</param>
        /// <returns>whether or not Type is IEnumerable or inherits from IEnumerable</returns>
        public static bool IsIEnumerable (this Type Type)
        {
            return Type != typeof(string) && (Type == typeof(IEnumerable) || Type.GetInterfaces().Any(x => x == typeof(IEnumerable)));
        }
    }
}