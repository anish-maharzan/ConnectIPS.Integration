
using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Services.ConnectIps;
using MainLibrary.SAPB1;
using Newtonsoft.Json;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ConnectIPS.Integration
{

    [FormAttribute("426", "Forms/Outgoing Payments.b1f")]
    class Outgoing_Payments : SystemFormBase
    {
        public Outgoing_Payments()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.BtnAdd = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.txtCC = ((SAPbouiCOM.EditText)(this.GetItem("5").Specific));
            this.txtDN = ((SAPbouiCOM.EditText)(this.GetItem("3").Specific));
            this.txtDUE = ((SAPbouiCOM.EditText)(this.GetItem("12").Specific));
            this.txtRemarks = ((SAPbouiCOM.EditText)(this.GetItem("26").Specific));
            this.BtnAdd.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.BtnAdd_ClickBefore);
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button BtnAdd;
        private SAPbouiCOM.EditText txtCC;
        private SAPbouiCOM.EditText txtDN;
        private SAPbouiCOM.EditText txtDUE;
        private SAPbouiCOM.EditText txtRemarks;

        private void OnCustomInitialize()
        {

        }

        private BankDetail GetSenderDetails(string bankCode)
        {
            BankDetail sender = null;
            string query = $@"SELECT T1.""AcctName"", T1.""Account"",  T0.""BankName"", T1.""UsrNumber1"", T1.""Branch"" FROM ODSC T0  INNER JOIN DSC1 T1 ON T0.""BankCode"" = T1.""BankCode"" WHERE T0.""BankCode"" ='{bankCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                sender = new BankDetail()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
            return sender;
        }

        private BankDetail GetReceiverDetails(string cardCode)
        {
            BankDetail receiver = null;
            string query = $@"SELECT T1.""AcctName"", T1.""Account"", T2.""BankName"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" INNER JOIN ODSC T2 ON T0.""BankCode"" = T2.""BankCode"" INNER JOIN DSC1 T3 ON T0.""BankCode"" = T3.""BankCode"" WHERE T0.""CardCode"" = '{cardCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                receiver = new BankDetail()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
            return receiver;
        }

        private void BtnAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                {
                    CipsBatchResponseModel response = FundTransfer().GetAwaiter().GetResult();

                    var responseMsg = response.cipsBatchResponse.responseMessage;
                    if (responseMsg != "SUCCESS")
                    {
                        Program.SBO_Application.MessageBox($"Error in fund transfer due to {responseMsg}");
                        BubbleEvent = false;
                    }
                    else
                    {
                        txtRemarks.Value = JsonConvert.SerializeObject(response.cipsBatchResponse.batchId);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task<CipsBatchResponseModel> FundTransfer()
        {
            RealTimeTransaction transaction = new RealTimeTransaction();
            var bankCode = ConfigurationManager.AppSettings["BankCode"];
            var sender = GetSenderDetails(bankCode);

            var vendor = txtCC.Value;
            var receiver = GetReceiverDetails(vendor);

            var realTime = GetTransactionData(sender, receiver);
            var connectIpsService = new ConnectIpsService();
            CipsBatchResponseModel response = await connectIpsService.RealTimeFundTransferAsync(realTime);

            return response;
        }

        private RealTimeTransaction GetTransactionData(BankDetail sender, BankDetail receiver)
        {
            var batchId = $"HIM{txtDN.Value}";

            var amountString = txtDUE.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);

            var cipsBatchDetail = new CIpsBatchDetail()
            {
                batchId = batchId,
                batchAmount = amount.ToString(),
                debtorAgent = sender.BankCode,
                debtorBranch = sender.BranchCode,
                debtorName = sender.AccountName,
                debtorAccount = sender.AccountNo,
            };

            var InstructionId = $"HIM-{txtDN.Value}-1";
            var cipsTransactionDetailList = new CIpsTransactionDetail()
            {
                instructionId = InstructionId,
                endToEndId = "TPJNG-PMT-79/80-0002",
                amount = amount.ToString(),
                creditorAgent = receiver.BankCode,
                creditorBranch = receiver.BranchCode,
                creditorName = receiver.AccountName,
                creditorAccount = receiver.AccountNo,

            };

            var realTime = new RealTimeTransaction
            {
                cipsBatchDetail = cipsBatchDetail
            };
            realTime.cipsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }
    }
}
