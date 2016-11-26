using mbox_iterator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Data
{
    public class MessageHeader
    {

        #region Properties
        //[FieldName("From")]
        //public string Sender { get; set; }


        public IEnumerable<HeaderField> Fields { get; set; }


        #endregion

    }
}
