using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Database
{
    /// <summary>
    /// Message Class
    /// </summary>
    public class Message
    {
        #region Constants

        /// <summary>
        /// End character of a line
        /// <see cref="https://tools.ietf.org/html/rfc2822#section-2.1"/>
        /// </summary>
        const string END_LINE_CHARACTER = "\r\n";

        /// <summary>
        /// Numbers of characters of a line, including CRLF
        /// <see cref="https://tools.ietf.org/html/rfc2822#section-2.1.1"/> 
        /// </summary>
        const int MAX_LINE_CHARACTERS_LENGTH = 1000;

        #endregion

        #region Properties

        public MessageHeader Header { get; set; }

        public MessageBody Body { get; set; }

        #endregion
    }
}
