using mbox_iterator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbox_iterator.Extensions;

namespace mbox_iterator.Data
{
    public class MessageHeader
    {

        #region Privates elements

        private string _sender;

        private DateTime _date;

        private IList<HeaderField> _fields = new List<HeaderField>();


        #endregion

        #region Properties

        /// <summary>
        /// Sender extracted from the FROM line
        /// </summary>
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        /// <summary>
        /// Date extracted from the FROM line
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


        public IList<HeaderField> Fields
        {
            get { return _fields; }
        }


        #endregion

        #region Methods

        public static MessageHeader FromString(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("data");

            MessageHeader result = new MessageHeader();

            var lines = data.GetLines().ToList();

            for(int i = 1; i < lines.Count(); i++)
            {
                try
                {
                    result.Fields.Add(HeaderField.GetHeaderFromLine(lines[i]));
                }
                catch { }
            }

            return result;
        }

        #endregion

    }
}
