using MainLibrary.SAPB1;
using SAPbobsCOM;
using System;

namespace ConnectIPS.Integration.Helpers
{
    public class AddonInfoInfo
    {
        public int Index { get; set; }
        public bool isHana { get; set; }
        private static int RetCode = 0;
        private static string ErrMsg = null;

        public AddonInfoInfo()
        {
        }

        public static bool InstallUDOs()
        {
            try
            {
                bool UDOAdded = true;

                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];
                string[,] srt = new string[,] { { "N", "No" }, { "Y", "Yes" } };

                B1Helper.AddField("NCHLINT", "NCHL-NPI (Bank Integration)", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("TRANTYPE", "NHCL Transaction Type", "OVPM", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CATEGORY", "NCHL Category Purpose", "OVPM", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ISVALIDATE", "Is Account Verified", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLSTATUS", "NHCL Payment Status", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLBATCH", "NHCL Batch", "OVPM", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                return UDOAdded;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

