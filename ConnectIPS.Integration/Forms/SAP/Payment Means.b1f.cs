
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration
{

    [FormAttribute("146", "Forms/SAP/Payment Means.b1f")]
    class Payment_Means : SystemFormBase
    {
        public Payment_Means()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.BtnOk = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.BankAmount = ((SAPbouiCOM.EditText)(this.GetItem("34").Specific));
            this.CashAmount = ((SAPbouiCOM.EditText)(this.GetItem("38").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.CloseAfter += new CloseAfterHandler(this.Form_CloseAfter);
        }


        private void OnCustomInitialize()
        {
            var oForm = Program.SBO_Application.Forms.ActiveForm;
            TypeEx = Convert.ToInt32(oForm.TypeEx);
            TypeCount = oForm.TypeCount;

        }

        
        private int TypeEx;
        private int TypeCount;
        private SAPbouiCOM.Button BtnOk;
        private SAPbouiCOM.EditText BankAmount;
        private SAPbouiCOM.EditText CashAmount;

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (TypeEx == 133)
            {
                var arInvoice = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(TypeEx, TypeCount);

                var strBankAmt = BankAmount.Value;
                if (!string.IsNullOrEmpty(strBankAmt))
                {
                    strBankAmt = strBankAmt.Split(' ')[1];

                    //double.TryParse(strBankAmt, out double amount);

                    var txtQRAmt = ((SAPbouiCOM.EditText)arInvoice.Items.Item("tQrAmt").Specific);
                    txtQRAmt.Value = strBankAmt;
                }

                var strCashAmt = CashAmount.Value;
                if (!string.IsNullOrEmpty(strCashAmt))
                {
                    strCashAmt = strCashAmt.Split(' ')[1];

                    //double.TryParse(stCashAmt, out double amount);

                    var txtCH = ((SAPbouiCOM.EditText)arInvoice.Items.Item("TxCH").Specific);
                    txtCH.Value = strCashAmt;
                }
            }
        }
    }
}
