using System;
using System.Linq;

namespace GGRev2ComboRecipeManager.Lib.Extensions
{
    public static class ByteArrayExtensions
    {
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            var len = end - start;

            // Return new array.
            var res = new T[len];
            for (var i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        public static byte[] Combine(params byte[][] arrays)
        {
            var rv = new byte[arrays.Sum(a => a.Length)];
            var offset = 0;
            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
