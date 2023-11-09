using ConnectIPS.Integration.Services;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Services.Implementation;
using SAPbouiCOM.Framework;
using System;
using System.Text;

namespace ConnectIPS.Integration.Forms.Users
{
    [FormAttribute("ConnectIPS.Integration.Forms.Users.NCHL_Details", "Forms/Users/NCHL Details.b1f")]
    class NCHL_Details : UserFormBase
    {
        public NCHL_Details()
        {
        }

        public NCHL_Details(BankAccount sender, BankAccount receiver, double amount, string transactionType, string remarks, int type, int count)
        {
            _type = type;
            _count = count;

            _sender = sender;
            _receiver = receiver;
            _amount = amount;
            _remarks = remarks;
            _transactionType = transactionType;
            LoadForm(sender, receiver, amount, remarks);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.tSendBank = ((SAPbouiCOM.EditText)(this.GetItem("tSendBank").Specific));
            this.tSendAcct = ((SAPbouiCOM.EditText)(this.GetItem("tSendAcct").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.tTransDate = ((SAPbouiCOM.EditText)(this.GetItem("tTransDate").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.tTransDet = ((SAPbouiCOM.EditText)(this.GetItem("tTransDet").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.tCredBank = ((SAPbouiCOM.EditText)(this.GetItem("tCredBank").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.tAcctName = ((SAPbouiCOM.EditText)(this.GetItem("tAcctName").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_15").Specific));
            this.tBActNum = ((SAPbouiCOM.EditText)(this.GetItem("tBActNum").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.tBTranAmt = ((SAPbouiCOM.EditText)(this.GetItem("tBTranAmt").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.tCharge = ((SAPbouiCOM.EditText)(this.GetItem("tCharge").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.tTotal = ((SAPbouiCOM.EditText)(this.GetItem("tTotal").Specific));
            this.bVerify = ((SAPbouiCOM.Button)(this.GetItem("bVer").Specific));
            this.bVerify.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bVerify_ClickBefore);
            this.bVerify.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bVerify_ClickAfter);
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_30").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_31").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_32").Specific));
            this.tSendAN = ((SAPbouiCOM.EditText)(this.GetItem("tSendAN").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.chVerify = ((SAPbouiCOM.CheckBox)(this.GetItem("chVerify").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private void OnCustomInitialize()
        {
            //BtnOK.Item.Visible = false;

            UIAPIRawForm.Left = (Program.SBO_Application.Desktop.Width - UIAPIRawForm.Width) / 2;
            UIAPIRawForm.Top = Convert.ToInt32((Program.SBO_Application.Desktop.Height - UIAPIRawForm.Height) / 2.5);
        }

        private SAPbouiCOM.EditText tSendBank;
        private SAPbouiCOM.EditText tSendAcct;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText tTransDate;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.EditText tTransDet;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText tCredBank;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText tAcctName;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText tBActNum;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.EditText tBTranAmt;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.EditText tCharge;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.EditText tTotal;
        private SAPbouiCOM.Button bVerify;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.StaticText StaticText14;
        private SAPbouiCOM.EditText tSendAN;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.CheckBox chVerify;

        private BankAccount _sender { get; set; }
        private BankAccount _receiver { get; set; }
        private double _amount { get; set; }
        private string _remarks { get; set; }
        private string _transactionType { get; set; }
        private int _type;
        private int _count;


        private void bVerify_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Verification();
        }

        private void bVerify_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (bVerify.Caption == "OK")
                UIAPIRawForm.Close();
        }


        private void LoadForm(BankAccount sender, BankAccount receiver, double amount, string remarks)
        {
            var realTimeService = new CIpsService();
            var chargeAmount = realTimeService.GetChargeAmount(amount, sender.BankName == receiver.BankName);

            tSendBank.Value = sender.BankName;
            tSendAcct.Value = sender.AccountNo;
            tSendAN.Value = sender.AccountName;
            tTransDate.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
            //tReference.Value = $"ACCOUNTFT:{sender.AccountName}";
            tCredBank.Value = receiver.BankName;
            tAcctName.Value = receiver.AccountName;
            tBActNum.Value = receiver.AccountNo;
            tBTranAmt.Value = amount.ToString();
            tCharge.Value = chargeAmount.ToString();
            tTotal.Value = (amount + chargeAmount).ToString();
            tTransDet.Value = remarks;
        }

        private void Verification()
        {

            var outgoingPayment = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
            var chValid = (SAPbouiCOM.CheckBox)outgoingPayment.Items.Item("chVal").Specific;
            try
            {
                //Verifying Sender Details
                Program.SBO_Application.StatusBar.SetText("Verifying sender account details.....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                var bankAccount = new ValidateBankAccount()
                {
                    bankId = _sender.BankCode,
                    accountId = _sender.AccountNo,
                    accountName = _sender.AccountName
                };
                ValidateBankAccountResponse result = NchlService.ValidateAccount(bankAccount);
                //var chSender = (SAPbouiCOM.CheckBox)outgoingPayment.Items.Item("chSender").Specific;
                if (result.responseCode != "000")
                {
                    if (result.matchPercentate < 80)
                    {
                        Program.SBO_Application.MessageBox($"{result.responseMessage}");
                        chValid.Checked = false;
                        return;
                    }
                    else
                    {
                        StringBuilder message = new StringBuilder();
                        message.AppendLine("*****Sender Account*****");
                        message.AppendLine("Warning");
                        message.AppendLine($"{result.responseMessage}");
                        message.AppendLine("Do you want to continue?");

                        var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");
                        if (input == 2)
                        {
                            //chSender.Checked = false;
                            chValid.Checked = false;
                            return;
                        }
                    }
                }

                Program.SBO_Application.MessageBox("*****Sender account details are verified successfully*****", 1, "Ok");
                //chSender.Checked = true;

                //Verifying Receiver
                Program.SBO_Application.StatusBar.SetText("Verifying beneficiary account details.....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                //var chReceiver = (SAPbouiCOM.CheckBox)outgoingPayment.Items.Item("chReceiver").Specific;

                bankAccount = new ValidateBankAccount()
                {
                    bankId = _receiver.BankCode,
                    accountId = _receiver.AccountNo,
                    accountName = _receiver.AccountName
                };
                result = NchlService.ValidateAccount(bankAccount);
                if (result.responseCode != "000")
                {
                    if (result.matchPercentate < 80)
                    {
                        Program.SBO_Application.MessageBox($"{result.responseMessage}");
                        chValid.Checked = false;
                        return;
                    }
                    else
                    {
                        StringBuilder message = new StringBuilder();
                        message.AppendLine("*****Beneficiary Account*****");
                        message.AppendLine("Warning");
                        message.AppendLine($"{result.responseMessage}");
                        message.AppendLine("Do you want to continue?");
                        var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");

                        if (input == 2)
                        {
                            //chReceiver.Checked = false;
                            chValid.Checked = false;
                            return;
                        }
                    }
                }
                Program.SBO_Application.MessageBox("*****Beneficiary account details are verified successfully*****", 1, "Ok");
                //chReceiver.Checked = true;

                //verifying amount
                Program.SBO_Application.StatusBar.SetText("Verifying transaction amount.....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                //var chAmt = (SAPbouiCOM.CheckBox)outgoingPayment.Items.Item("chAmt").Specific;

                bool isSameBank = _sender.BankCode == _receiver.BankCode;

                if (!NchlService.ValidateTransferAmount(_transactionType, _amount, isSameBank, out string errMessage))
                {
                    Program.SBO_Application.StatusBar.SetText(errMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    //chAmt.Checked = false;
                    chValid.Checked = false;
                    return;
                }

                Program.SBO_Application.MessageBox("*****Transaction Amount is verified successfullyl*****", 1, "Ok");
                //chAmt.Checked = true;

                chValid.Checked = true;
                chVerify.Item.Enabled = true;
                chVerify.Checked = true;
                chVerify.Item.Enabled = false;

                bVerify.Caption = "OK";
                //bVerify.Item.Visible = false;
                //BtnOK.Item.Visible = true;
                ////UIAPIRawForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                //BtnOK.Item.Left = UIAPIRawForm.Width / 2 - BtnOK.Item.Width / 2;
                //BtnOK.Item.Height = tTotal.Item.Top + 10;

                Program.SBO_Application.StatusBar.SetText("NCHL details are verified", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                //UIAPIRawForm.Close();
            }
            catch (Exception ex)
            {
                chValid.Checked = false;
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
    }
}
