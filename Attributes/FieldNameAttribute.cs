using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator.Attributes
{
    public class FieldNameAttribute : Attribute
    {

        #region Attributes

        private string _name;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
        }

        #endregion

        #region Constructors

        public FieldNameAttribute(string name)
        {
            this._name = name;
        }

        #endregion

    }
}
