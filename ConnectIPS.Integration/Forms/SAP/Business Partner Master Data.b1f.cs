
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

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
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.chVerify = ((SAPbouiCOM.CheckBox)(this.GetItem("chVer").Specific));
            this.tBank = ((SAPbouiCOM.EditText)(this.GetItem("tBank").Specific));
            this.tBranch = ((SAPbouiCOM.EditText)(this.GetItem("tBranch").Specific));
            this.tAcctNum = ((SAPbouiCOM.EditText)(this.GetItem("tAcctNum").Specific));
            this.tAcctName = ((SAPbouiCOM.EditText)(this.GetItem("tAcctName").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            chVerify.Item.FromPane = 6;
            chVerify.Item.ToPane = 6;

            //chVerify.Item.Top = tRegion.Item.Top;
            //chVerify.Item.Left = tRegion.Item.Left + tRegion.Item.Width + 10;
        }

        private SAPbouiCOM.CheckBox chVerify;
        private SAPbouiCOM.EditText tBank;
        private SAPbouiCOM.EditText tBranch;
        private SAPbouiCOM.EditText tAcctNum;
        private SAPbouiCOM.EditText tAcctName;
        private SAPbouiCOM.EditText tRegion;
    }
}
