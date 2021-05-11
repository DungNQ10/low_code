using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Framework.Utils
{
    public static class StringExt
    {
        public static string ToCamelCase(this string the_string)
        {
            if (the_string == null || the_string.Length < 2)
                return the_string;

            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string result = words[0].Substring(0, 1).ToLower() + words[0].Substring(1);
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
    }
}
