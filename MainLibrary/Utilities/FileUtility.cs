using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainLibrary.Utilities
{
    public class FileUtility
    {
        #region Members
        OpenFileDialog _oFileDialog;
        #endregion
        
        #region Properties
        public string Filter
        {
            get { return _oFileDialog.Filter; }
            set { _oFileDialog.Filter = value; }
        }
        public string FileName
        {
            get { return _oFileDialog.FileName; }
            set { _oFileDialog.FileName = value; }
        }
        public string InitialDirectory
        {
            get { return _oFileDialog.InitialDirectory; }
            set { _oFileDialog.InitialDirectory = value; }
        }
        #endregion

        #region Constructor
        public FileUtility()
        {
            _oFileDialog = new OpenFileDialog();
        }
        #endregion

        #region Methods
        public UploadFile GetFileName()
        {
            string fileSourcePath = string.Empty;
            string fileName = string.Empty;
            string extension = string.Empty;
            UploadFile file = new UploadFile();
            IntPtr ptr = GetForegroundWindow();
            WindowWrapper oWindow = new WindowWrapper(ptr);
            try
            {
                DialogResult result = _oFileDialog.ShowDialog(oWindow);
                if (result == DialogResult.OK)
                {
                    file.Path = System.IO.Path.GetDirectoryName(_oFileDialog.FileName);
                    file.Name = System.IO.Path.GetFileNameWithoutExtension(_oFileDialog.FileName);
                    file.Extention = System.IO.Path.GetExtension(_oFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);

            }

            oWindow = null;
            return file;
        }
        #endregion

        #region Internal Methods

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        #endregion
    }
}
