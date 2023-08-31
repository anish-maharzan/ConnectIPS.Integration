using ConnectIPS.Integration.Helpers;
using NepalPay.Library.Credentials;
using NepalPay.Library.Models.Authentication;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Configuration;

namespace ConnectIPS.Integration
{
    class Program
    {
        #region Variables

        public static SAPbobsCOM.Company oCompany;
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbouiCOM.Form oForm { get; set; }

        #endregion

        /// <summary_>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //Application.SBO_Application.StatusBar.SetSystemMessage("Connecting to the Add-on", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                Application oApp = null;
                if (args.Length < 1)
                    oApp = new Application();
                else
                    oApp = new Application(args[0]);

                SBO_Application = SAPbouiCOM.Framework.Application.SBO_Application;
                oCompany = (SAPbobsCOM.Company)SBO_Application.Company.GetDICompany();
                Menu MyMenu = new Menu();
                Menu.AddMenu();

                var UDF = ConfigurationManager.AppSettings["UDF"].ToString();
                if (UDF == "N")
                {
                    AddonInfoInfo.InstallUDOs();
                }
                InitializeNCHL();
                var applicationHandler = new ApplicationHandlers();
                var addonName = "NCHL-NPI Integration";
                Application.SBO_Application.StatusBar.SetSystemMessage($"{addonName} Add-on installed successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private static bool InitializeNCHL()
        {
            string query = $@"SELECT TOP 1 T0.""U_UA_USERNAME"", T0.""U_UA_PASSWORD"", T0.""U_BA_USERNAME"", T0.""U_BA_PASSWORD"", ""U_FILEPATH"", ""U_BASEURL"", ""U_ENV"", ""U_PFXPWD"" FROM ""@NCHL_NPI_CONFIG"" T0 WHERE T0.""Name"" = 'NPI' AND T0.""U_ISENABLE"" = 'True'";
            Recordset Rec = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Rec.DoQuery(query);
            if (Rec.RecordCount > 0)
            {
                NPICredential.UserAuth = new UserAuthentication()
                {
                    username = Rec.Fields.Item(0).Value.ToString(),
                    password = Rec.Fields.Item(1).Value.ToString(),
                    grant_type = "password"
                };

                NPICredential.BasicAuth = new BasicAuthentication()
                {
                    Username = Rec.Fields.Item(2).Value.ToString(),
                    Password = Rec.Fields.Item(3).Value.ToString()
                };

                NPICredential.FileName = Rec.Fields.Item(4).Value.ToString();
                NPICredential.BaseUrl = Rec.Fields.Item(5).Value.ToString();
                NPICredential.Environment = Rec.Fields.Item(6).Value.ToString();
                NPICredential.PFXPassword = Rec.Fields.Item(7).Value.ToString();
            }
            else
            {
                Program.SBO_Application.StatusBar.SetText("NCHL Bank Detail is missing!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            return true;
        }
    }
}
