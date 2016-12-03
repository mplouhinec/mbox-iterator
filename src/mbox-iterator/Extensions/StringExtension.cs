using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Extensions
{
    public static class StringExtension
    {

        public static IEnumerable<string> GetLines(this string str, bool removeEmptyLines = false)
        {
            return str.Split(new[] { "\r\n", "\r", "\n" },
                removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

    }
}
