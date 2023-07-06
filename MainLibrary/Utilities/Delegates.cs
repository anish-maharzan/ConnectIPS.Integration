using MainLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Utilities
{
    public delegate void UserFormClosedDone(object sender);
    public delegate void SubFormCreated<T>(object sender, FormArgs formArgs) where T : new();
    public delegate void CFLValueSelected(object sender, SAPbouiCOM.SBOItemEventArg pv, string id, string[,] table, int rowSelected);
}
