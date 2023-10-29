using MainLibrary.SAPB1;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;

namespace ConnectIPS.Integration
{

    [FormAttribute("196", "Forms/SAP/Payment Means.b1f")]
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
            this.TxtGLAccount = ((SAPbouiCOM.EditText)(this.GetItem("26").Specific));
            this.TxtTotal = ((SAPbouiCOM.EditText)(this.GetItem("34").Specific));
            this.TxtGLAccount.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.TxtGLAccount_ChooseFromListBefore);
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("28").Specific));
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

        private SAPbouiCOM.EditText TxtGLAccount;
        private SAPbouiCOM.EditText TxtTotal;
        private SAPbouiCOM.Matrix Matrix0;

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (TypeEx == 426)
            {
                var outgoing = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(TypeEx, TypeCount);

                ((SAPbouiCOM.EditText)outgoing.Items.Item("TxtGLA").Specific).Value = TxtGLAccount.Value;
                ((SAPbouiCOM.EditText)outgoing.Items.Item("txtTotal").Specific).Value = TxtTotal.Value;
            }
        }

        private void TxtGLAccount_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (TypeEx == 426)
                {
                    var outgoing = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(TypeEx, TypeCount);
                    var bankCheck = ((SAPbouiCOM.CheckBox)outgoing.Items.Item("chNCHL").Specific).Checked;

                    if (bankCheck)
                    {
                        Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                        string query = $@"SELECT T0.""U_ACCTCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND IFNULL(T0.""U_ACCTCODE"",'') <> ''";
                        rec.DoQuery(query);
                        if (rec.RecordCount > 0)
                        {
                            try
                            {
                                int index = 1;
                                SAPbouiCOM.ChooseFromList oCFLEvento = default(SAPbouiCOM.ChooseFromList);
                                SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                                SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);

                                var cflList = UIAPIRawForm.ChooseFromLists;

                                oCFLEvento = this.UIAPIRawForm.ChooseFromLists.Item("1");

                                oCFLEvento.SetConditions(null);
                                oCons = oCFLEvento.GetConditions();
                                while (!rec.EoF)
                                {
                                    oCon = oCons.Add();
                                    oCon.Alias = "AcctCode";
                                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                    oCon.CondVal = rec.Fields.Item(0).Value.ToString();
                                    if (index != rec.RecordCount)
                                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                                    rec.MoveNext();
                                    index++;
                                }
                                oCFLEvento.SetConditions(oCons);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
    }
}
