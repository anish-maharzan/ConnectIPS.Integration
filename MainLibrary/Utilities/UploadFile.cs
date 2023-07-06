using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Utilities
{
    public class UploadFile
    {
        #region Properties
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        #endregion

        #region Constructor
        public UploadFile()
        {
            Path = string.Empty;
            Name = string.Empty;
            Extention = string.Empty;

        }
        public UploadFile(string path, string name, string extention)
        {
            Path = path;
            Name = name;
            Extention = extention;
        } 
        #endregion
    }
}
