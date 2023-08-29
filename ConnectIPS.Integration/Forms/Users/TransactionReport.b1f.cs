using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration.Forms.Users
{
    [FormAttribute("ConnectIPS.Integration.Forms.Users.TransactionReport", "Forms/Users/TransactionReport.b1f")]
    class TransactionReport : UserFormBase
    {
        public TransactionReport()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_2").Specific));
            this.Option1 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Option1").Specific));
            this.Option2 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Option2").Specific));
            this.Option3 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Option3").Specific));
            this.TxBatch = ((SAPbouiCOM.EditText)(this.GetItem("TxBatch").Specific));
            this.TxIntro = ((SAPbouiCOM.EditText)(this.GetItem("TxIntro").Specific));
            this.TxFDate = ((SAPbouiCOM.EditText)(this.GetItem("TxFDate").Specific));
            this.TxTDate = ((SAPbouiCOM.EditText)(this.GetItem("TxToDate").Specific));
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
            Option2.GroupWith("Option1");
            Option3.GroupWith("Option1");
        }

        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.OptionBtn Option1;
        private SAPbouiCOM.OptionBtn Option2;
        private SAPbouiCOM.OptionBtn Option3;
        private SAPbouiCOM.EditText TxBatch;
        private SAPbouiCOM.EditText TxIntro;
        private SAPbouiCOM.EditText TxFDate;
        private SAPbouiCOM.EditText TxTDate;
    }
}
