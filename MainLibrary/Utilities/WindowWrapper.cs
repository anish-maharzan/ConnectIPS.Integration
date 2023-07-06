using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainLibrary.Utilities
{
    public class WindowWrapper : IWin32Window
    {
        #region Members
        private IntPtr _hwnd;
        #endregion

        #region Properties
        public virtual IntPtr Handle
        {
            get { return _hwnd; }
        }
        #endregion
        
        #region Constructor
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        } 
        #endregion
    }
}
