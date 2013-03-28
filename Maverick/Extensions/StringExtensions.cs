
namespace Maverick.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// adds a space between words in strings in PascalCase
        /// </summary>
        /// <param name="Input">string to split</param>
        /// <returns>split string</returns>
        public static string SplitByWord (this string Input)
        {
            var Chars = Input.ToCharArray();
            var Words = new List<string>();
            int Index = 0;
            while (Index != Chars.Length)
            {
                if (char.IsUpper(Chars[Index]))
                {
                    int NextIndex = Input.IndexOfNextUpperChar(Index + 1);                    
                    // abbreviation
                    int i = Index;
                    while (NextIndex == i + 1)
                    {
                        NextIndex = Input.IndexOfNextUpperChar(NextIndex + 1);
                        i++;
                    }
                    // no more upper case chars
                    if (NextIndex == -1)
                    {
                        NextIndex = Chars.Length;
                    }
                    Words.Add(Input.Substring(Index, NextIndex - Index));
                    Index = NextIndex;
                }
                else
                {
                    Index++;
                }
            }
            return string.Join(" ", Words);
        }

        /// <summary>
        /// finds the index of the next upper case character in a string
        /// </summary>
        /// <param name="Input">input string</param>
        /// <param name="Index">starting index, default is 0</param>
        /// <returns>index of next upper case character, -1 if none found</returns>
        public static int IndexOfNextUpperChar (this string Input, int Index = 0)
        {
            var Chars = Input.ToCharArray();
            while (Index < Input.Length)
            {
                if (char.IsUpper(Chars[Index]))
                {
                    return Index;
                }
                Index++;
            }
            return -1;
        }
    }
}