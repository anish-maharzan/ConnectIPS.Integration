using MainLibrary.SAPB1;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Response.Report;
using NepalPay.Library.Services.Implementation;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            this.Option2 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Option2").Specific));
            this.Option2.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.Option2_PressedAfter);
            this.Option2.ClickAfter += new SAPbouiCOM._IOptionBtnEvents_ClickAfterEventHandler(this.Option2_ClickAfter);
            this.Option1 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Option1").Specific));
            this.Option1.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.Option1_PressedAfter);
            this.Option1.ClickAfter += new SAPbouiCOM._IOptionBtnEvents_ClickAfterEventHandler(this.Option1_ClickAfter);
            this.TxBatch = ((SAPbouiCOM.EditText)(this.GetItem("TxBatch").Specific));
            this.TxFDate = ((SAPbouiCOM.EditText)(this.GetItem("TxFDate").Specific));
            this.TxTDate = ((SAPbouiCOM.EditText)(this.GetItem("TxToDate").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_2").Specific));
            this.BtnLoad = ((SAPbouiCOM.Button)(this.GetItem("BtnLoad").Specific));
            this.BtnLoad.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.BtnLoad_ClickAfter);
            this.BtnLoad.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.BtnLoad_ClickBefore);
            this.CmbType = ((SAPbouiCOM.ComboBox)(this.GetItem("CmbType").Specific));
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
            Option1.GroupWith("Option2");
            Option1.Item.Click();
            TxBatch.Item.Enabled = false;
            UIAPIRawForm.State = SAPbouiCOM.BoFormStateEnum.fs_Maximized;
        }

        private SAPbouiCOM.OptionBtn Option2;
        private SAPbouiCOM.OptionBtn Option1;
        private SAPbouiCOM.EditText TxBatch;
        private SAPbouiCOM.EditText TxFDate;
        private SAPbouiCOM.EditText TxTDate;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.Button BtnLoad;
        private SAPbouiCOM.ComboBox CmbType;

        private void BtnLoad_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!BtnLoadValidation())
            {
                BubbleEvent = false;
                return;
            }
        }

        private bool BtnLoadValidation()
        {
            try
            {
                if (string.IsNullOrEmpty(CmbType.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the Transaction Type", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (Option1.Selected && string.IsNullOrEmpty(TxFDate.Value) && string.IsNullOrEmpty(TxFDate.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the From Data and To Date", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (Option2.Selected && string.IsNullOrEmpty(TxBatch.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Batch", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void BtnLoad_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                Program.SBO_Application.StatusBar.SetText("Loading.....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                Matrix0.Clear();
                var type = CmbType.Value;

                List<ReportDetail> rows = new List<ReportDetail>();
                List<ReportDetail> lines = new List<ReportDetail>();
                if (type == "RT")
                {
                    if (Option1.Selected)
                    {
                        DateTime fromDate = DateTime.ParseExact(TxFDate.Value, "yyyyMMdd", null);
                        DateTime toDate = DateTime.ParseExact(TxTDate.Value, "yyyyMMdd", null);
                        List<CIpsReportByDateResponse> response = CipsReportByDate(fromDate, toDate).GetAwaiter().GetResult();
                        lines = GetReportDetails(response);
                        SetDocEntry(lines);
                        LoadMatrix(lines);

                    }
                    else if (Option2.Selected)
                    {
                        var batchId = TxBatch.Value;
                        CIpsReportByBatchResponse response = CipsReportByBatch(batchId).GetAwaiter().GetResult();
                        lines = GetReportDetails(response);
                        SetDocEntry(lines);
                        LoadMatrix(lines);
                    }
                }
                else
                {
                    if (Option1.Selected)
                    {
                        DateTime fromDate = DateTime.ParseExact(TxFDate.Value, "yyyyMMdd", null);
                        DateTime toDate = DateTime.ParseExact(TxTDate.Value, "yyyyMMdd", null);
                        List<NchlIpsReportByDateResponse> response = NchlIpsReportByDate(fromDate, toDate).GetAwaiter().GetResult();
                        lines = GetReportDetails(response);
                        SetDocEntry(lines);
                        LoadMatrix(lines);

                    }
                    else if (Option2.Selected)
                    {
                        var batchId = TxBatch.Value;
                        NchlIpsReportByBatchResponse response = NchlIpsReportByBatch(batchId).GetAwaiter().GetResult();
                        lines = GetReportDetails(response);
                        SetDocEntry(lines);
                        LoadMatrix(lines);
                    }
                }

                Program.SBO_Application.StatusBar.SetText($"Successfully loaded", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void SetDocEntry(List<ReportDetail> Lines)
        {
            string query = $@"SELECT T0.""U_NCHLBATCH"", T0.""DocEntry"" FROM OVPM T0 WHERE T0.""U_NCHLBATCH""  IN ({string.Join(",", Lines.Select(x => "'" + x.BatchId + "'"))})";
            Recordset Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Rec.DoQuery(query);
            if (Rec.RecordCount > 0)
            {
                var batchID = Rec.Fields.Item(0).Value.ToString();
                var docEntry = Convert.ToInt32(Rec.Fields.Item(1).Value.ToString());
                Lines.First(x => x.BatchId == batchID).DocEntry = docEntry;
            }
        }

        private void LoadMatrix(List<ReportDetail> records)
        {
            var reportDT = UIAPIRawForm.DataSources.DataTables.Item("Report");
            reportDT.Rows.Clear();
            int index = 0;
            foreach (var record in records)
            {
                reportDT.Rows.Add();
                if (record.DocEntry != 0)
                    reportDT.SetValue("DocEntry", index, record.DocEntry);
                reportDT.SetValue("LineId", index, record.LineId);
                reportDT.SetValue("Id", index, record.Id);
                reportDT.SetValue("Batch ID", index, record.BatchId);
                reportDT.SetValue("RecDate", index, record.RecDate);
                reportDT.SetValue("Batch Amount", index, record.BatchAmount);
                reportDT.SetValue("Batch Charge Amount", index, record.BatchChargeAmount);
                reportDT.SetValue("Debtor Agent", index, record.DebtorAgent);
                reportDT.SetValue("Debtor Branch", index, record.DebtorBranch);
                reportDT.SetValue("Debtor Name", index, record.DebtorName);
                reportDT.SetValue("Debtor Account", index, record.DebtorAccount);
                reportDT.SetValue("rcreTime", index, record.RcreTime);
                reportDT.SetValue("debitStatus", index, record.DebitStatus);
                reportDT.SetValue("Id1", index, record.Id1);
                reportDT.SetValue("RecDate1", index, record.RecDate1);
                reportDT.SetValue("InstructionID", index, record.InstructionID);
                reportDT.SetValue("endToEndId", index, record.EndToEndId);
                reportDT.SetValue("amount", index, record.Amount);
                reportDT.SetValue("chargeAmount", index, record.ChargeAmount);
                reportDT.SetValue("creditorAgent", index, record.CreditorAgent);
                reportDT.SetValue("creditorBranch", index, record.CreditorBranch);
                reportDT.SetValue("creditorName", index, record.CreditorName);
                reportDT.SetValue("creditorAccount", index, record.CreditorAccount);
                reportDT.SetValue("particulars", index, record.Particulars ?? "");
                reportDT.SetValue("reversalStatus", index, record.ReversalStatus ?? "");

                index++;
            }

            Matrix0.LoadFromDataSourceEx();
            Matrix0.AutoResizeColumns();
        }

        private async Task<List<CIpsReportByDateResponse>> CipsReportByDate(DateTime fromDate, DateTime toDate)
        {
            var reportService = new CIpsReportingService();
            var response = await reportService.GetTransactionReport(fromDate, toDate);
            return response;
        }

        private async Task<CIpsReportByBatchResponse> CipsReportByBatch(string batchId)
        {
            var reportService = new CIpsReportingService();
            var response = await reportService.GetTransactionReport(batchId);
            return response;
        }

        private async Task<List<NchlIpsReportByDateResponse>> NchlIpsReportByDate(DateTime fromDate, DateTime toDate)
        {
            var reportService = new NhclIpsReportingService();
            var response = await reportService.GetTransactionReport(fromDate, toDate);
            return response;
        }

        private async Task<NchlIpsReportByBatchResponse> NchlIpsReportByBatch(string batchId)
        {
            var reportService = new NhclIpsReportingService();
            var response = await reportService.GetTransactionReport(batchId);
            return response;
        }

        private void Option1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private void Option2_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
        }

        private void Option1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            TxTDate.Item.Enabled = Option1.Selected;
            TxFDate.Item.Enabled = Option1.Selected;
            TxFDate.Item.Click();
            TxBatch.Item.Enabled = false;
            TxBatch.Value = "";
        }

        private void Option2_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            TxBatch.Item.Enabled = Option2.Selected;
            TxBatch.Item.Click();
            TxTDate.Item.Enabled = false;
            TxFDate.Item.Enabled = false;
            TxFDate.Value = "";
            TxTDate.Value = "";
        }

        private List<ReportDetail> GetReportDetails(List<CIpsReportByDateResponse> response)
        {
            int index = 1;
            var result = response.Select(x => new ReportDetail()
            {
                LineId = index++,
                Id = x.cipsBatchDetail.id,
                BatchId = x.cipsBatchDetail.batchId,
                RecDate = x.cipsBatchDetail.recDate.Replace("-", ""),
                BatchAmount = x.cipsBatchDetail.batchAmount,
                BatchChargeAmount = x.cipsBatchDetail.batchChargeAmount,
                DebtorAgent = x.cipsBatchDetail.debtorAgent,
                DebtorBranch = x.cipsBatchDetail.debtorBranch,
                DebtorName = x.cipsBatchDetail.debtorName,
                DebtorAccount = x.cipsBatchDetail.debtorAccount,
                RcreTime = x.cipsBatchDetail.rcreTime.ToString(),
                DebitStatus = x.cipsBatchDetail.debitStatus,
                Id1 = x.cipsTransactionDetailList.First().id,
                RecDate1 = x.cipsTransactionDetailList.First().recDate.Replace("-", ""),
                InstructionID = x.cipsTransactionDetailList.First().instructionId,
                EndToEndId = x.cipsTransactionDetailList.First().endToEndId,
                Amount = x.cipsTransactionDetailList.First().amount,
                ChargeAmount = x.cipsTransactionDetailList.First().chargeAmount,
                CreditorAgent = x.cipsTransactionDetailList.First().creditorAgent,
                CreditorBranch = x.cipsTransactionDetailList.First().creditorBranch,
                CreditorName = x.cipsTransactionDetailList.First().creditorName,
                CreditorAccount = x.cipsTransactionDetailList.First().creditorAccount,
                Particulars = x.cipsTransactionDetailList.First().particulars,
                ReversalStatus = x.cipsTransactionDetailList.First().reversalStatus?.ToString(),
            }).ToList();
            return result;
        }

        private List<ReportDetail> GetReportDetails(CIpsReportByBatchResponse report)
        {
            int index = 1;
            List<ReportDetail> result = new List<ReportDetail>()
            {
                new ReportDetail()
                {
                LineId = index++,
                    Id = report.id,
                    BatchId = report.batchId,
                    RecDate = report.recDate.Replace("-", ""),
                    BatchAmount = report.batchAmount,
                    BatchChargeAmount = report.batchChargeAmount,
                    DebtorAgent = report.debtorAgent,
                    DebtorBranch = report.debtorBranch,
                    DebtorName = report.debtorName,
                    DebtorAccount = report.debtorAccount,
                    RcreTime = report.rcreTime,
                    DebitStatus =report.debitStatus,
                    Id1 = report.cipsTransactionDetailList.First().id,
                    RecDate1 = report.cipsTransactionDetailList.First().recDate.Replace("-", ""),
                    InstructionID = report.cipsTransactionDetailList.First().instructionId,
                    EndToEndId = report.cipsTransactionDetailList.First().endToEndId,
                    Amount = report.cipsTransactionDetailList.First().amount,
                    ChargeAmount = report.cipsTransactionDetailList.First().chargeAmount,
                    CreditorAgent = report.cipsTransactionDetailList.First().creditorAgent,
                    CreditorBranch = report.cipsTransactionDetailList.First().creditorBranch,
                    CreditorName = report.cipsTransactionDetailList.First().creditorName,
                    CreditorAccount = report.cipsTransactionDetailList.First().creditorAccount,
                    Particulars =   report.cipsTransactionDetailList.First().particulars,
                    ReversalStatus = report.cipsTransactionDetailList.First().reversalStatus?.ToString(),
                }
            };
            return result;
        }

        private List<ReportDetail> GetReportDetails(List<NchlIpsReportByDateResponse> response)
        {
            int index = 1;
            var result = response.Select(x => new ReportDetail()
            {
                LineId = index++,
                Id = x.nchlIpsBatchDetail.id,
                BatchId = x.nchlIpsBatchDetail.batchId,
                RecDate = x.nchlIpsBatchDetail.recDate.Replace("-", ""),
                BatchAmount = x.nchlIpsBatchDetail.batchAmount,
                BatchChargeAmount = x.nchlIpsBatchDetail.batchChargeAmount,
                DebtorAgent = x.nchlIpsBatchDetail.debtorAgent,
                DebtorBranch = x.nchlIpsBatchDetail.debtorBranch,
                DebtorName = x.nchlIpsBatchDetail.debtorName,
                DebtorAccount = x.nchlIpsBatchDetail.debtorAccount,
                RcreTime = x.nchlIpsBatchDetail.rcreTime.ToString(),
                DebitStatus = x.nchlIpsBatchDetail.debitStatus,
                Id1 = x.nchlIpsTransactionDetailList.First().id,
                RecDate1 = x.nchlIpsTransactionDetailList.First().recDate.Replace("-", ""),
                InstructionID = x.nchlIpsTransactionDetailList.First().instructionId,
                EndToEndId = x.nchlIpsTransactionDetailList.First().endToEndId,
                Amount = x.nchlIpsTransactionDetailList.First().amount,
                ChargeAmount = x.nchlIpsTransactionDetailList.First().chargeAmount,
                CreditorAgent = x.nchlIpsTransactionDetailList.First().creditorAgent,
                CreditorBranch = x.nchlIpsTransactionDetailList.First().creditorBranch,
                CreditorName = x.nchlIpsTransactionDetailList.First().creditorName,
                CreditorAccount = x.nchlIpsTransactionDetailList.First().creditorAccount,
                Particulars = x.nchlIpsTransactionDetailList.First().particulars,
                ReversalStatus = x.nchlIpsTransactionDetailList.First().reversalStatus?.ToString(),
            }).ToList();
            return result;
        }

        private List<ReportDetail> GetReportDetails(NchlIpsReportByBatchResponse report)
        {
            int index = 1;
            List<ReportDetail> result = new List<ReportDetail>()
            {
                new ReportDetail()
                {
                LineId = index++,
                    Id = report.id,
                    BatchId = report.batchId,
                    RecDate = report.recDate.Replace("-", ""),
                    BatchAmount = report.batchAmount,
                    BatchChargeAmount = report.batchChargeAmount,
                    DebtorAgent = report.debtorAgent,
                    DebtorBranch = report.debtorBranch,
                    DebtorName = report.debtorName,
                    DebtorAccount = report.debtorAccount,
                    RcreTime = report.rcreTime,
                    DebitStatus =report.debitStatus,
                    Id1 = report.nchlIpsTransactionDetailList.First().id,
                    RecDate1 = report.nchlIpsTransactionDetailList.First().recDate.Replace("-", ""),
                    InstructionID = report.nchlIpsTransactionDetailList.First().instructionId,
                    EndToEndId = report.nchlIpsTransactionDetailList.First().endToEndId,
                    Amount = report.nchlIpsTransactionDetailList.First().amount,
                    ChargeAmount = report.nchlIpsTransactionDetailList.First().chargeAmount,
                    CreditorAgent = report.nchlIpsTransactionDetailList.First().creditorAgent,
                    CreditorBranch = report.nchlIpsTransactionDetailList.First().creditorBranch,
                    CreditorName = report.nchlIpsTransactionDetailList.First().creditorName,
                    CreditorAccount = report.nchlIpsTransactionDetailList.First().creditorAccount,
                    Particulars =   report.nchlIpsTransactionDetailList.First().particulars,
                    ReversalStatus = report.nchlIpsTransactionDetailList.First().reversalStatus?.ToString(),
                }
            };
            return result;
        }
    }

    public class ReportDetail
    {
        public int DocEntry { get; set; }
        public int LineId { get; set; }
        public int Id { get; set; }
        public string BatchId { get; set; }
        public string RecDate { get; set; }
        public double BatchAmount { get; set; }
        public double BatchChargeAmount { get; set; }
        public string DebtorAgent { get; set; }
        public string DebtorBranch { get; set; }
        public string DebtorName { get; set; }
        public string DebtorAccount { get; set; }
        public string RcreTime { get; set; }
        public string DebitStatus { get; set; }
        public int Id1 { get; set; }
        public string RecDate1 { get; set; }
        public string InstructionID { get; set; }
        public string EndToEndId { get; set; }
        public double Amount { get; set; }
        public double ChargeAmount { get; set; }
        public string CreditorAgent { get; set; }
        public string CreditorBranch { get; set; }
        public string CreditorName { get; set; }
        public string CreditorAccount { get; set; }
        public string Particulars { get; set; }
        public string ReversalStatus { get; set; }
        //public string OrignBranchId { get; set; }
    }
}
