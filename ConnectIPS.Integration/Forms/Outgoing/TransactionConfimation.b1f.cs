using SAPbouiCOM.Framework;
using System;

namespace ConnectIPS.Integration.Forms.Outgoing
{
    [FormAttribute("ConnectIPS.Integration.Forms.Outgoing.TransactionConfimation", "Forms/Outgoing/TransactionConfimation.b1f")]
    class TransactionConfimation : UserFormBase
    {
        public TransactionConfimation()
        {
        }

        public TransactionConfimation(int type, int count, TransactionConfirmationModel model)
        {
            _type = type;
            _count = count;

            tSendBank.Value = model.SenderBank;
            tSendAcct.Value = model.SenderAcctNumber;
            tSendAN.Value = model.SenderAcctName;
            tTransDate.Value = model.TransactionDate;
            tReference.Value = model.ReferenceID;
            tCredBank.Value = model.CreditorBank;
            tAcctName.Value = model.CreditorAcctName;
            tBActNum.Value = model.CreditorAcctNum;
            tBTranAmt.Value = model.TransactionAmt;
            tCharge.Value = model.ChargeAmt;
            tTotal.Value = model.TotalAmt;
            tTransDet.Value = model.TransactionDetail;
        }

        private int _type;
        private int _count;

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.tSendBank = ((SAPbouiCOM.EditText)(this.GetItem("tSendBank").Specific));
            this.tSendAcct = ((SAPbouiCOM.EditText)(this.GetItem("tSendAcct").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.tTransDate = ((SAPbouiCOM.EditText)(this.GetItem("tTransDate").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.tReference = ((SAPbouiCOM.EditText)(this.GetItem("tReference").Specific));
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
            this.btnCancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.btnCancel.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.btnCancel_ClickAfter);
            this.btnPay = ((SAPbouiCOM.Button)(this.GetItem("bPay").Specific));
            this.btnPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.btnPay_ClickAfter);
            this.fldSender = ((SAPbouiCOM.Folder)(this.GetItem("Item_26").Specific));
            this.fldBeneficiary = ((SAPbouiCOM.Folder)(this.GetItem("Item_28").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_30").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_31").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_32").Specific));
            this.tSendAN = ((SAPbouiCOM.EditText)(this.GetItem("tSendAN").Specific));
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
            //fldSender.Select();
            //fldBeneficiary.Select();
            UIAPIRawForm.Left = (Program.SBO_Application.Desktop.Width - UIAPIRawForm.Width) / 2;
            UIAPIRawForm.Top = Convert.ToInt32((Program.SBO_Application.Desktop.Height - UIAPIRawForm.Height) / 2.5);
        }

        private SAPbouiCOM.EditText tSendBank;
        private SAPbouiCOM.EditText tSendAcct;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText tTransDate;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText tReference;
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
        private SAPbouiCOM.Button btnCancel;
        private SAPbouiCOM.Button btnPay;
        private SAPbouiCOM.Folder fldSender;
        private SAPbouiCOM.Folder fldBeneficiary;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.StaticText StaticText14;
        private SAPbouiCOM.EditText tSendAN;

        private void btnPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
            var processButton = (SAPbouiCOM.Button)form.Items.Item("bProc").Specific;
            processButton.Item.Click();
            btnCancel.Item.Click();
        }

        private void btnCancel_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
        }

    }

    public class TransactionConfirmationModel
    {
        public string SenderBank { get; set; }
        public string SenderAcctNumber { get; set; }
        public string SenderAcctName { get; set; }
        public string TransactionDate { get; set; }
        public string ReferenceID { get; set; }
        public string TransactionDetail { get; set; }
        public string CreditorBank { get; set; }
        public string CreditorAcctName { get; set; }
        public string CreditorAcctNum { get; set; }
        public string TransactionAmt { get; set; }
        public string ChargeAmt { get; set; }
        public string TotalAmt { get; set; }
    }
}
