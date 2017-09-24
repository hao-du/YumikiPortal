using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yumiki.Commons.ExtensionMethods
{
    public static class StringExtension
    {
        /// <summary>
        /// Replace Unicode Characters such as Vietnamese to English chars
        /// </summary>
        public static string NormalizeUnicode(this string value)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");

            return regex
                .Replace(value.Normalize(NormalizationForm.FormD), String.Empty)
                .Replace('\u0111', 'd')
                .Replace('\u0110', 'D');
        }
    }
}
