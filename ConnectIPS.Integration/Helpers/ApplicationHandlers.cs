using GlobalVariable;
using MainLibrary.SAPB1;
using MainLibrary.Utilities;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;

namespace ConnectIPS.Integration.Helpers
{
    public class ApplicationHandlers
    {
        public static List<B1FormBase> FrmInstances = new List<B1FormBase>();
        private List<string> UDForms = new List<string> { "frm_FILL", "frm_FILL" };
        private static string sCredentials, sSenderEmail, Body, sSubject, sdocentry, squeryS, squeryE, CC;

        public ApplicationHandlers()
        {
            try
            {
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                Application.SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SBO_Application_MenuEvent);
                Application.SBO_Application.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SBO_Application_RightClickEvent);
                Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
                Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
            }
        }

        #region Events
        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
        private void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.BeforeAction)
                {
                    SBO_Application_MenuEventBefore(ref pVal, out BubbleEvent);
                }
                else
                {
                    switch (pVal.MenuUID)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }
        private void SBO_Application_MenuEventBefore(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
            }
            catch (Exception ex)
            {
            }
        }
        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.Matrix oMatrix;
            SAPbouiCOM.Application oApp = Application.SBO_Application;
        }
        private void SBO_Application_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (eventInfo.BeforeAction)
            {
                SAPbouiCOM.Application oApp = Application.SBO_Application;
                var oform = oApp.Forms.Item(eventInfo.FormUID);
            }

        }
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            switch (BusinessObjectInfo.FormTypeEx)
            {
                ////Sales order
                //case "139":
                //    if (BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD && BusinessObjectInfo.ActionSuccess == true)
                //    {
                //        SAPbouiCOM.Application oApp = Application.SBO_Application;
                //        var UDFForm = oApp.Forms.Item(BusinessObjectInfo.FormUID);
                //        string docEntry = ((SAPbouiCOM.EditText)UDFForm.Items.Item("8").Specific).Value;
                //    }
                //    break;
                //default:
                //    break;

            }
        }
        #endregion

        #region Methods
        // public long SendEmailNotification(string sCredentials, string sSenderEmail, string sBody, string sSubject, ref string sErrDesc)
        public static void SendEmailNotification()
        {
            string sFuncName = String.Empty;
            SmtpClient oSmtpServer = new SmtpClient();
            MailMessage oMail = new MailMessage();
            string[] split;
            string[] Email;
            string p_SyncDateTime = string.Empty;
            Email = CC.Split(',');
            SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            split = sCredentials.Split(';');  // 0-port 1-server 2-username 3-password 4-emailform 5- ssl
            try
            {

                oSmtpServer.Credentials = new System.Net.NetworkCredential(split[2], split[3]);
                oSmtpServer.Port = Convert.ToInt32(split[0]);
                oSmtpServer.Host = split[1];

                if (split[5] == "Y")
                { oSmtpServer.EnableSsl = true; }
                else { oSmtpServer.EnableSsl = false; }

                oMail.From = new MailAddress(split[4]);
                if (Email.Length > 0)
                { sSenderEmail += "," + CC; }

                oMail.To.Add(sSenderEmail);

                oMail.Subject = sSubject;
                // oMail.Body = Mail_Body(sBody)
                oMail.Body = Body;
                oMail.IsBodyHtml = true;

                oSmtpServer.Send(oMail);
                oMail.Dispose();
                rs.DoQuery(string.Format(squeryS, sdocentry));

                // return 1;
            }
            catch (Exception ex)
            {
                rs.DoQuery(string.Format(squeryE, sdocentry, ex.Message));
                //sErrDesc = ("Please check the email Address." + ("Fail to send email : "
                //            + (sSenderEmail + (":: " + ex.Message))));

                // return 0;
            }
        }

        public static void CallToChildThread(string sCredentials, string sSenderEmail, string sBody, string sSubject, ref string sErrDesc)
        {
            Console.WriteLine("Child thread starts");

            // the thread is paused for 5000 milliseconds
            int sleepfor = 5000;

            Console.WriteLine("Child Thread Paused for {0} seconds", sleepfor / 1000);
            Thread.Sleep(sleepfor);
            Console.WriteLine("Child thread resumes");
        }
        #endregion

    }
}
