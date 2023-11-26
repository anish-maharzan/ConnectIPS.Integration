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

                //UDT
                B1Helper.AddTable("NCHL_BANK", "NCHL Bank Detail", BoUTBTableType.bott_NoObject);
                B1Helper.AddField("ISENABLE", "Is Enable", "NCHL_BANK", BoFieldTypes.db_Alpha, 5, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "False", new string[,] { { "False", "False" }, { "True", "True" } });
                //B1Helper.AddField("ACCOUNTNUM", "Account Number", "NCHL_BANK", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                //B1Helper.AddField("ACCOUNTNAME", "Account Name", "NCHL_BANK", BoFieldTypes.db_Alpha, 140, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BANKNAME", "Bank Name", "NCHL_BANK", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BANKCODE", "Bank Code", "NCHL_BANK", BoFieldTypes.db_Alpha, 4, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BRANCHCODE", "Branch Code", "NCHL_BANK", BoFieldTypes.db_Alpha, 4, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ACCTCODE", "G/L Account", "NCHL_BANK", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "", linktoEntities: "1");

                //string query = $@"SELECT T0.""BPLId"", T0.""BPLName"" FROM OBPL T0";
                //Recordset Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                //Rec.DoQuery(query);
                //if (Rec.RecordCount > 0)
                //{

                //}

                B1Helper.AddField("BPLId", "SAP Branch ID", "NCHL_BANK", BoFieldTypes.db_Alpha, 11, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                //B1Helper.AddField("BPLName", "SAP Branch Name", "NCHL_BANK", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                B1Helper.AddTable("NCHL_NPI_CONFIG", "NCHL-NPI Configuration", BoUTBTableType.bott_NoObjectAutoIncrement);
                B1Helper.AddField("ENV", "Environment", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 5, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "Test", new string[,] { { "Test", "Test" }, { "Live", "Live" } });
                B1Helper.AddField("ISENABLE", "Is Enable", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 5, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "False", new string[,] { { "False", "False" }, { "True", "True" } });
                B1Helper.AddField("BASEURL", "Base Url", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("UA_USERNAME", "User Auth Username", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("UA_PASSWORD", "User Auth Password", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BA_USERNAME", "Basic Auth Username", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BA_PASSWORD", "Basic Auth Password", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("FILEPATH", "NPI File Path", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PFXPWD", "pfx Password", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BATCH", "Batch Prefix", "NCHL_NPI_CONFIG", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                //System Table
                B1Helper.AddField("NCHLINT", "NCHL-NPI (Bank Integration)", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLTRANTYPE", "NHCL Transaction Type", "OVPM", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLCATEGORY", "NCHL Category Purpose", "OVPM", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLVALIDATION", "NCHL Validation", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLBATCH", "NHCL Batch", "OVPM", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLPAYMENT", "NHCL Payment", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLID", "NHCL ID", "OVPM", BoFieldTypes.db_Numeric, 11, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLCODE", "NHCL Response Code", "OVPM", BoFieldTypes.db_Alpha, 5, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLMSG", "NHCL Response Message", "OVPM", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("GLAccount", "GL Account", "OVPM", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("TOTAL", "Total Amount", "OVPM", BoFieldTypes.db_Float, 11, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, false, "");
                B1Helper.AddField("DRAFTKEY", "Draft Key", "OVPM", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                //B1Helper.AddField("NCHLACCOUNTNUM", "NCHL Account Number", "RCT4", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "", linktoTable: "NCHL_BANK");
                B1Helper.AddField("NCHLACCOUNTNUM", "NCHL Account Number", "RCT4", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLACCOUNTNAME", "NCHL Account Name", "RCT4", BoFieldTypes.db_Alpha, 140, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                B1Helper.AddField("NCHLVERIFIED", "NHCL Verified", "OCRD", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLBANKCODE", "NHCL BANK CODE", "OCRD", BoFieldTypes.db_Alpha, 4, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLBRANCHCODE", "NHCL BRANCH CODE", "OCRD", BoFieldTypes.db_Alpha, 4, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLACCOUNTNUM", "NHCL ACCOUNT NUMBER", "OCRD", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("NCHLACCOUNTNAME", "NHCL ACCOUNT NAME", "OCRD", BoFieldTypes.db_Alpha, 140, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                return UDOAdded;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

