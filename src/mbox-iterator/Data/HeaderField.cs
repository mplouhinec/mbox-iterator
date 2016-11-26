using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Data
{
    public class HeaderField
    {

        #region Properties

        public string FieldName { get; set; }

        public object Value { get; set; }


        public static HeaderField GetHeaderFromLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            if (line.Length > Message.MAX_LINE_CHARACTERS_LENGTH)
                throw new ArgumentOutOfRangeException(string.Format("The line is too long. {0} characters detected ; {1} characters max.", line.Length, Message.MAX_LINE_CHARACTERS_LENGTH));

            var index = line.IndexOf(":");

            if (index == -1)
                throw new ArgumentException("The line does't respect the RFC Field definitions");


            return new HeaderField { FieldName = line.Substring(0, index).Trim(), Value = line.Substring(index + 1, line.Length - index - 1).Trim() };
        }

        #endregion

    }
}
