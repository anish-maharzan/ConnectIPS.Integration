using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConnectIPS.Integration.Helpers
{
    class GlobalMethods_Variables
    {
        public static DataTable ToRelease = new DataTable();
        public static DataTable oDTBU = new DataTable();
        public static DateTime dServerDate { get; private set; }
        public static string CreateUDF { get; private set; }
        public static string APGLAccount { get; private set; }
        public static string APSAC { get; private set; }
        public static string ImportTax { get; private set; }
        public static string LCParentAccount { get; private set; }
        public static string FromType { get; private set; }
        public static string FromUID { get; private set; }
        public static string grpowhs { get; private set; }

        public static string sPath { get; private set; }
        public static string sEmail { get; private set; }

        public static int formcount { get; private set; }
        public static string LCostFlag { get; private set; }
        public static string LcDoc { get; private set; }
        public static string LcEntry { get; private set; }
        public static string BaseType { get; private set; }
        public static List<string> DocEntry = new List<string>();
        public void DeAssign()
        {
            if (ToRelease.Rows.Count > 0)
            {
                int p = 0;
                foreach (System.Data.DataRow item in ToRelease.Rows)
                {
                    string Query = "Select * from OBIN where  \"BinCode\" = '" + item["BinName"].ToString() + "'";
                    resetRec();
                    rec.DoQuery(Query);
                    Double OccupiedQty = Convert.ToDouble(rec.Fields.Item("U_ITNOCCU").Value.ToString());
                    Double AvailableQty = Convert.ToDouble(rec.Fields.Item("U_ITNAVAIL").Value.ToString());
                    AvailableQty = AvailableQty + Convert.ToDouble(item["BinQty"].ToString().Split(',')[p]);
                    OccupiedQty = OccupiedQty - Convert.ToDouble(item["BinQty"].ToString().Split(',')[p]);
                    UpdateAllocated_AvailQty_IN_OBIN(OccupiedQty.ToString(), AvailableQty.ToString(), item["BinName"].ToString());
                    //Program.SBO_Application.StatusBar.SetText("Calliing 1 ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    p++;
                }
            }
        }
        private SAPbobsCOM.Recordset rec;
        private void resetRec()
        {
            if (rec != null)
                rec = null;
            if (rec == null)
                rec = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        }

        public static Int32 POType { get; private set; }

        private void UpdateAllocated_AvailQty_IN_OBIN(string Occupied, string AvailQty, string bincode)
        {
            try
            {
                resetRec();
                string Query = "Update OBIN Set \"U_ITNOCCU\" = '" + Occupied + "' , \"U_ITNAVAIL\" = '" + AvailQty + "' Where \"BinCode\" = '" + bincode + "' ";
                rec.DoQuery(Query);
            }
            catch
            {
            }
        }

        public static void Setsformcount(Int32 newInt)
        {
            formcount = newInt;
        }

        public static void SetssEmail(string newInt)
        {
            sEmail = newInt;
        }

        public static void SetssPath(string newInt)
        {
            sPath = newInt;
        }

        public static void Setsgrpowhs(string newInt)
        {
            grpowhs = newInt;
        }

        public static void SetsPOType(Int32 newInt)
        {
            POType = newInt;
        }

        public static void SetsdServerDate(DateTime newInt)
        {
            dServerDate = newInt;
        }

        public static void SetsCreateUDF(string newInt)
        {
            CreateUDF = newInt;
        }

        public static void SetsAPGLAccount(string newInt)
        {
            APGLAccount = newInt;
        }

        public static void SetsAPSAC(string newInt)
        {
            APSAC = newInt;
        }

        public static void SetsImportTax(string newInt)
        {
            ImportTax = newInt;
        }

        public static void SetsLCParentAccount(string newInt)
        {
            LCParentAccount = newInt;
        }

        public static void SetsFromType(string newInt)
        {
            FromType = newInt;
        }

        public static void SetsFromUID(string newInt)
        {
            FromUID = newInt;
        }

        public static void SetsLCostFlag(string newInt)
        {
            LCostFlag = newInt;
        }

        public static void SetsoDTBU(DataTable newInt)
        {
            oDTBU = newInt;
        }
        public static void SetsLcDoc(string newInt)
        {
            LcDoc = newInt;
        }
        public static void setsLcEntry(string newInt)
        {
            LcEntry = newInt;
        }

        public static void SetBaseType(string docType)
        {
            BaseType = docType;
        }
        public static void SetDocEntry(List<string> docEntry)
        {
            DocEntry = docEntry;
        }
        //if (ConfigurationSettings.AppSettings["UDF"].ToString() == "N")
    }
}
