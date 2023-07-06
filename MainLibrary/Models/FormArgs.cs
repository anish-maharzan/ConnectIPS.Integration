using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Models
{
    public class FormArgs : EventArgs
    {
        #region Members
        private Dictionary<object, object> _parameters = new Dictionary<object, object>();
        #endregion

        #region Properties
        public Dictionary<object, object> Parameters
        {
            get { return _parameters; }
        }
        #endregion

        #region Constructor
        public FormArgs(Dictionary<object, object> parameters)
        {
            _parameters = parameters;
        }
        #endregion
    }
}
