using System;
using System.Configuration;
using ConnectIPS.Integration.Helpers;
using MainLibrary.SAPB1;
using NepalPay.Library.Credentials;
using NepalPay.Library.Models.Authentication;
using NepalPay.Library.Services;
using SAPbobsCOM;
using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration
{
    class Program
    {
        public static SAPbobsCOM.Company oCompany;
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbouiCOM.Form oForm { get; set; }

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
                var addonName = "Nepal QR Integration";
                Application.SBO_Application.StatusBar.SetSystemMessage($"{addonName} Add-on installed successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private static void InitializeNCHL()
        {
            string query = $@"SELECT TOP 1 T0.""U_UA_USERNAME"", T0.""U_UA_PASSWORD"", T0.""U_BA_USERNAME"", T0.""U_BA_PASSWORD"",T0.""U_MERCHANTCODE"", T0.""U_CATEGORYCODE"", T0.""U_MERCHANTNAME"", T0.""U_ACQUIRERID"" FROM ""@NCHLQR"", ""U_FILENAME"" T0";
            Recordset Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Rec.DoQuery(query);
            if (Rec.RecordCount > 0)
            {
                DynamicQRCredential.QrBasicAuth = new BasicAuthentication()
                {
                    Username = Rec.Fields.Item(0).Value.ToString(),
                    Password = Rec.Fields.Item(1).Value.ToString()
                };

                DynamicQRCredential.QrUserAuth = new UserAuthentication()
                {
                    username = Rec.Fields.Item(2).Value.ToString(),
                    password = Rec.Fields.Item(3).Value.ToString(),
                    grant_type = "password"
                };

                DynamicQRCredential.MerchantCode = Rec.Fields.Item(4).Value.ToString();
                DynamicQRCredential.MerchantCategoryCode = Convert.ToInt32(Rec.Fields.Item(5).Value.ToString());
                DynamicQRCredential.MerchantName = Rec.Fields.Item(6).Value.ToString();
                DynamicQRCredential.AcquirerId = Rec.Fields.Item(7).Value.ToString();
                DynamicQRCredential.FileName = Rec.Fields.Item(8).Value.ToString();
                DynamicQRCredential.BaseUrl = Rec.Fields.Item(9).Value.ToString();
            }
            else
            {
                Program.SBO_Application.StatusBar.SetText("NCHL Configuration is missing!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

    }
}
