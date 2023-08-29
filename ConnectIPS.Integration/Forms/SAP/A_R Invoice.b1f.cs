
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectIPS.Integration.Forms.Users;
using NepalPay.Library.Models.QR;
using NepalPay.Library.Services.Implementation;
using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration
{

    [FormAttribute("133", "Forms/SAP/A_R Invoice.b1f")]
    class A_R_Invoice : SystemFormBase
    {
        public A_R_Invoice()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.bQR = ((SAPbouiCOM.Button)(this.GetItem("bQRC").Specific));
            this.bQR.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bQR_ClickBefore);
            this.bQR.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bQR_ClickAfter);
            this.bPay = ((SAPbouiCOM.Button)(this.GetItem("bPaymt").Specific));
            this.bPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bPay_ClickAfter);
            this.chQR = ((SAPbouiCOM.CheckBox)(this.GetItem("chQR").Specific));
            this.chQR.PressedAfter += new SAPbouiCOM._ICheckBoxEvents_PressedAfterEventHandler(this.chQR_PressedAfter);
            this.chPaid = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.tTraceID = ((SAPbouiCOM.EditText)(this.GetItem("tTrace").Specific));
            this.tQRAmt = ((SAPbouiCOM.EditText)(this.GetItem("tQrAmt").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.TxCASH = ((SAPbouiCOM.EditText)(this.GetItem("TxCH").Specific));
            this.bAdd = ((SAPbouiCOM.ButtonCombo)(this.GetItem("1").Specific));
            this.bAdd.ClickBefore += new SAPbouiCOM._IButtonComboEvents_ClickBeforeEventHandler(this.bAdd_ClickBefore);
            this.tCardCode = ((SAPbouiCOM.EditText)(this.GetItem("4").Specific));
            this.tDN = ((SAPbouiCOM.EditText)(this.GetItem("8").Specific));
            this.tTotalAmt = ((SAPbouiCOM.EditText)(this.GetItem("29").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("38").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("34").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("86").Specific));
            this.TxTaxDate = ((SAPbouiCOM.EditText)(this.GetItem("46").Specific));
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
            FormSetting();
        }

        private SAPbouiCOM.Button bQR;
        private SAPbouiCOM.Button bPay;
        private SAPbouiCOM.CheckBox chQR;
        private SAPbouiCOM.CheckBox chPaid;
        private SAPbouiCOM.EditText tTraceID;
        private SAPbouiCOM.EditText tQRAmt;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText TxCASH;

        private SAPbouiCOM.EditText tTotalAmt;
        private SAPbouiCOM.EditText tDN;
        private SAPbouiCOM.ButtonCombo bAdd;
        private SAPbouiCOM.EditText tCardCode;
        private SAPbouiCOM.EditText TxTaxDate;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText2;

        private void bQR_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bQrCodeValidation())
                BubbleEvent = false;
        }

        private void FormSetting()
        {
            bQR.Item.Visible = false;
            StaticText1.Item.Visible = false;
            StaticText0.Item.Visible = false;
            tQRAmt.Item.Visible = false;
            TxCASH.Item.Visible = false;

            chQR.Item.Top = TxTaxDate.Item.Top + TxTaxDate.Item.Height + 1;

            StaticText1.Item.Top = StaticText2.Item.Top + StaticText2.Item.Height + 1;
            TxCASH.Item.Top = StaticText2.Item.Top + StaticText2.Item.Height + 1;
            StaticText0.Item.Top = StaticText2.Item.Top + StaticText2.Item.Height * 2 + 3;
            tQRAmt.Item.Top = StaticText2.Item.Top + StaticText2.Item.Height * 2 + 3;

            bQR.Item.Top = bAdd.Item.Top;
        }

        private void bQR_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                GenerateQRCode().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chPaid.Checked = true;
            bAdd.Item.Click();
        }

        private void bAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bAddValidation())
                BubbleEvent = false;
        }

        private bool bQrCodeValidation()
        {
            var strtotalAmount = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(strtotalAmount, out double amount);
            if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Program.SBO_Application.StatusBar.SetText("Form is already Added.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tCardCode.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the Customer.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (Matrix0.VisualRowCount == 1)
            {
                Program.SBO_Application.StatusBar.SetText("Please add the items.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (Convert.ToDouble(tQRAmt.Value) == 0)
            {
                Program.SBO_Application.StatusBar.SetText("QR amount is zero.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task GenerateQRCode()
        {
            var QRAmount = tQRAmt.Value;
            var data = new QRGeneration()
            {
                transactionAmount = QRAmount,
                billNumber = tDN.Value
            };

            var service = new DynamicQRService();
            var response = await service.GenerateQRAsync(data);
            if (response.responseCode != "000")
            {
                Program.SBO_Application.StatusBar.SetText(response.responseMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }
            else
            {
                var traceID = response.data.validationTraceId;
                tTraceID.Value = traceID;
                string qrString = response.data.qrString;
                var form = new QRCodeScan(qrString, traceID, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                form.Show();
            }
        }

        private bool bAddValidation()
        {
            //if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            //{
            //    Program.SBO_Application.StatusBar.SetText("Nepal Pay QR Payment is not completed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            //    return false;
            //}
            //else 
            if (chQR.Checked && !chPaid.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please proceed QR payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void chQR_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            bQR.Item.Visible = chQR.Checked;
            StaticText1.Item.Visible = chQR.Checked;
            StaticText0.Item.Visible = chQR.Checked;
            tQRAmt.Item.Visible = chQR.Checked;
            TxCASH.Item.Visible = chQR.Checked;
        }
    }
}
