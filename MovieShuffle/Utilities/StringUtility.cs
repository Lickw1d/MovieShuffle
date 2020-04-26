using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShuffle.Utilities
{
    public static class StringUtility
    {
        public static string CapitalizeFirstLetters(this string str)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0 || char.IsWhiteSpace(str[i - 1]))
                {
                    sb.Append(char.ToUpper(str[i]));
                }
                else
                {
                    sb.Append(str[i]);
                }

            }

            return sb.ToString();
        }
    }
}
