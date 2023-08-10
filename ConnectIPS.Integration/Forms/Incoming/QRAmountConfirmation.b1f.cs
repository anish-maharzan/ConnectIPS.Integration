using SAPbouiCOM.Framework;
using System;

namespace ConnectIPS.Integration.Forms.Incoming
{
    [FormAttribute("ConnectIPS.Integration.Forms.Incoming.QRAmountConfirmation", "Forms/Incoming/QRAmountConfirmation.b1f")]
    class QRAmountConfirmation : UserFormBase
    {
        private readonly int _type;
        private readonly int _count;
        public QRAmountConfirmation()
        {
        }

        public QRAmountConfirmation(string amount, int type, int count)
        {
            tAmt.Value = amount;
            _type = type;
            _count = count;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.tAmt = ((SAPbouiCOM.EditText)(this.GetItem("tAmt").Specific));
            this.bConfirm = ((SAPbouiCOM.Button)(this.GetItem("bConfirm").Specific));
            this.bConfirm.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bConfirm_ClickAfter);
            this.bConfirm.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bConfirm_ClickBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            Program.SBO_Application.ItemEvent += SBO_Application_ItemEvent;
        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            UIAPIRawForm.Left = (Program.SBO_Application.Desktop.Width - UIAPIRawForm.Width) / 2;
            UIAPIRawForm.Top = Convert.ToInt32((Program.SBO_Application.Desktop.Height - UIAPIRawForm.Height) / 2.5);
        }

        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            //if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_KEY_DOWN && pVal.CharPressed == 13)
            //{
            //    bConfirm.Item.Click();
            //}
        }

        private SAPbouiCOM.EditText tAmt;
        private SAPbouiCOM.Button bConfirm;
        private SAPbouiCOM.Button Button1;

        private void bConfirm_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            double amount = Convert.ToDouble(tAmt.Value);
            if (amount == 0)
            {
                Program.SBO_Application.StatusBar.SetText("Please enter amount", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                BubbleEvent = true;
                return;
            }
        }

        private void bConfirm_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
            var chFlag = (SAPbouiCOM.CheckBox)form.Items.Item("ChFlag").Specific;
            chFlag.Checked = true;
            var tQRAmt = (SAPbouiCOM.EditText)form.Items.Item("tQrAmt").Specific;
            tQRAmt.Value = tAmt.Value;
            var addButtom = (SAPbouiCOM.Button)form.Items.Item("bQRC").Specific;
            addButtom.Item.Click();
            Button1.Item.Click();
        }
    }
}
