using ConnectIPS.Integration.Services;
using NepalPay.Library.Models.Account;
using SAPbouiCOM.Framework;
using System;
using System.Text;

namespace ConnectIPS.Integration
{

    [FormAttribute("134", "Forms/SAP/Business Partner Master Data.b1f")]
    class Business_Partner_Master_Data : SystemFormBase
    {
        public Business_Partner_Master_Data()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.bVerify = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.bVerify.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bVerify_ClickAfter);
            this.bVerify.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bVerify_ClickBefore);
            this.chVerify = ((SAPbouiCOM.CheckBox)(this.GetItem("chVer").Specific));
            this.tBank = ((SAPbouiCOM.EditText)(this.GetItem("tBank").Specific));
            this.tBranch = ((SAPbouiCOM.EditText)(this.GetItem("tBran").Specific));
            this.tAcctNum = ((SAPbouiCOM.EditText)(this.GetItem("tAcctNum").Specific));
            this.tAcctName = ((SAPbouiCOM.EditText)(this.GetItem("tAcctName").Specific));
            this.bCancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("40").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataUpdateBefore += new SAPbouiCOM.Framework.FormBase.DataUpdateBeforeHandler(this.Form_DataUpdateBefore);
            this.DataAddBefore += new DataAddBeforeHandler(this.Form_DataAddBefore);

        }

        private SAPbouiCOM.Button bVerify;

        private void OnCustomInitialize()
        {
            //chVerify.Item.FromPane = 6;
            //chVerify.Item.ToPane = 6;

            chVerify.Item.Top = ComboBox0.Item.Top;
            chVerify.Item.Left = ComboBox0.Item.Left + ComboBox0.Item.Width + 7;

            //Button0.Item.FromPane = 6;
            //Button0.Item.ToPane = 6;

            bVerify.Item.Top = bCancel.Item.Top;
            bVerify.Item.Left = bCancel.Item.Left + bCancel.Item.Width + 7;
        }

        private SAPbouiCOM.CheckBox chVerify;
        private SAPbouiCOM.EditText tBank;
        private SAPbouiCOM.EditText tBranch;
        private SAPbouiCOM.EditText tAcctNum;
        private SAPbouiCOM.EditText tAcctName;
        private SAPbouiCOM.EditText tRegion;
        private SAPbouiCOM.Button bCancel;
        private SAPbouiCOM.ComboBox ComboBox0;

        private void bVerify_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!Validation())
                BubbleEvent = false;
        }

        private void bVerify_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                VerifyAccount();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void VerifyAccount()
        {
            Program.SBO_Application.StatusBar.SetText("Verifying account details.....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

            var bankAccount = new ValidateBankAccount()
            {
                bankId = tBank.Value,
                accountId = tAcctNum.Value,
                accountName = tAcctName.Value
            };
            ValidateBankAccountResponse result = NchlService.ValidateAccount(bankAccount);
            if (result.responseCode != "000")
            {
                if (result.matchPercentate < 80)
                {
                    Program.SBO_Application.MessageBox($"{result.responseMessage}");
                    chVerify.Item.Enabled = true;
                    chVerify.Checked = false;
                    chVerify.Item.Enabled = false;
                    return;
                }
                else
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Warning");
                    message.AppendLine($"{result.responseMessage}");
                    message.AppendLine("Do you want to continue?");

                    var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");
                    if (input == 2)
                    {
                        chVerify.Item.Enabled = true;
                        chVerify.Checked = false;
                        chVerify.Item.Enabled = false;
                        return;
                    }
                }
            }
            chVerify.Item.Enabled = true;
            chVerify.Checked = true;
            chVerify.Item.Enabled = false;

            Program.SBO_Application.MessageBox("*****Account details are verified successfully*****", 1, "Ok");
        }


        private bool Validation()
        {
            if (string.IsNullOrEmpty(tBank.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please enter Bank Code.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tAcctNum.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please enter account number.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tAcctName.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please enter account name.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private SAPbouiCOM.Button Button0;

        private void Form_DataUpdateBefore(ref SAPbouiCOM.BusinessObjectInfo pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_FIND_MODE && !chVerify.Checked)
                    VerifyAccount();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void Form_DataAddBefore(ref SAPbouiCOM.BusinessObjectInfo pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_FIND_MODE && !chVerify.Checked)
                    VerifyAccount();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
    }
}
