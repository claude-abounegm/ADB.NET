/*
 * Extensions\BytesHelper.cs
 * Written by Claude Abounegm
 */

using System.Text;

namespace Android.Extensions
{
    public static class BytesHelper
    {
        /// <summary>
        /// array from: https://code.google.com/p/project-fourchips/source/browse/trunk/Source+Files/strtoul.c?spec=svn12&r=12
        /// </summary>
        static readonly int[] cvtIn = new int[] {
	        0, 1, 2, 3, 4, 5, 6, 7, 8, 9,               /* '0' - '9' */
	        100, 100, 100, 100, 100, 100, 100,          /* punctuation */
	        10, 11, 12, 13, 14, 15, 16, 17, 18, 19,     /* 'A' - 'Z' */
	        20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
	        30, 31, 32, 33, 34, 35,
	        100, 100, 100, 100, 100, 100,               /* punctuation */
	        10, 11, 12, 13, 14, 15, 16, 17, 18, 19,     /* 'a' - 'z' */
	        20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
	        30, 31, 32, 33, 34, 35
        };

        /// <summary>
        /// Converts the ASCII string representation of a HEX number to an equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="buffer">The bytes representation of the ASCII string. If the array is bigger than 8 bytes, the number is truncated.</param>
        /// <returns>The equivalent 32-bit signed integer.</returns>
        public static int FromHexToInt32(this byte[] buffer)
        {
            int value = 0;
            for (int i = 0; i < buffer.Length && i < 8; ++i)
                value = (value << 4) + cvtIn[buffer[i] - '0'];
            return value;
        }

        public static string ToString(this byte[] buffer, Encoding encoding)
        {
            return encoding.GetString(buffer);
        }
    }
}
