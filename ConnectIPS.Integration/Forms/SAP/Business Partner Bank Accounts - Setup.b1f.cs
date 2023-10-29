
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainLibrary.SAPB1;
using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration
{

    [FormAttribute("65052", "Forms/SAP/Business Partner Bank Accounts - Setup.b1f")]
    class Business_Partner_Bank_Accounts___Setup : SystemFormBase
    {
        public Business_Partner_Bank_Accounts___Setup()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("3").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.CloseAfter += new CloseAfterHandler(this.Form_CloseAfter);

        }

        private int TypeEx;
        private int TypeCount;

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (TypeEx == 134)
            {
                var businessPartner = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(TypeEx, TypeCount);

                //var type = ((SAPbouiCOM.ComboBox)businessPartner.Items.Item("chVer").Specific).Selected;




                ((SAPbouiCOM.EditText)businessPartner.Items.Item("tBank").Specific).Value = Matrix0.GetCellValue("MandateID", 1).ToString();
                ((SAPbouiCOM.EditText)businessPartner.Items.Item("tBranch").Specific).Value = Matrix0.GetCellValue("Branch", 1).ToString();
                ((SAPbouiCOM.EditText)businessPartner.Items.Item("tAcctName").Specific).Value = Matrix0.GetCellValue("AcctName", 1).ToString();
                ((SAPbouiCOM.EditText)businessPartner.Items.Item("tAcctNum").Specific).Value = Matrix0.GetCellValue("Account", 1).ToString();
            }
        }

        private void OnCustomInitialize()
        {
            var oForm = Program.SBO_Application.Forms.ActiveForm;
            TypeEx = Convert.ToInt32(oForm.TypeEx);
            TypeCount = oForm.TypeCount;
        }

        private SAPbouiCOM.Matrix Matrix0;
    }
}
