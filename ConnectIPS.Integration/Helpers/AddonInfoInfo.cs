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

                B1Helper.AddField("NCHLQR", "NCHL QR", "OINV", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tYES, BoFldSubTypes.st_None, false, "N", srt);
                B1Helper.AddField("NCHLSTATUS", "NCHL Status", "OINV", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tYES, BoFldSubTypes.st_None, false, "N", srt);
                B1Helper.AddField("NCHLTRACEID", "NCHL ValidationTraceId", "OINV", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false);
                B1Helper.AddField("QRAMT", "NCHL QR Payment Amount", "OINV", BoFieldTypes.db_Float, 7, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, false);
                B1Helper.AddField("CASHAMT", "Cash Payment Amount", "OINV", BoFieldTypes.db_Float, 7, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, false);

                B1Helper.AddTable("NCHL_QR", "NCHL QR Configuration", BoUTBTableType.bott_NoObjectAutoIncrement);
                B1Helper.AddField("UA_USERNAME", "User Auth Username", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("UA_PASSWORD", "User Auth Password", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("BA_USERNAME", "Basic Auth Username", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("BA_PASSWORD", "Basic Auth Password", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("MERCHANTCODE", "MerchantCode", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CATEGORYCODE", "MerchantCategoryCode", "NCHL_QR", BoFieldTypes.db_Numeric, 4, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("MERCHANTNAME", "MerchantName", "NCHL_QR", BoFieldTypes.db_Alpha, 25, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ACQUIRERID", "AcquirerId", "NCHL_QR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("FILEPATH", "NPI File Path", "NCHL_QR", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("BASEURL", "Base Url", "NCHL_QR", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                return UDOAdded;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                //B1Helper.DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                return false;
            }
        }

    }
}

