using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.SAPB1
{
    public static class FieldNames
    {

        //Business Partner Fields
        public const string BP_Location = "Location";

        //Approval Stage

        public const string A_Owner = "owner";
        public const string A_Auth = "Auth";
        public const string A_Code = "Code";
      
        //OUSER
        public const string O_Recall = "Recall";
        public const string O_DebitAccount = "DebitAccount";
        public const string O_CreditAccount = "CreditAccount";
        public const string O_Normalrate = "Normalrate";
        public const string O_Overtimerate = "Overtimerate";
        public const string O_Travelrate = "Travelrate";
        public const string O_TDebitAccount = "TDebitAccount";
        public const string O_TCreditAccount = "TCreditAccount";

        //OADM
        public const string OADM_ServiceEmail = "S_Email";
        public const string OADM_ActivityEmail = "A_Email";
        public const string OADM_JournalEmail = "J_Email";
        public const string OADM_CompanyLogo1 = "Logo1";
        public const string OADM_CompanyLogo2 = "Logo2";
        public const string OADM_CompanyLogo3 = "Logo3";
        public const string OADM_ExcessDay = "ExcessDay";
        public const string OADM_NotificationEmail = "N_Email";

        //Invoice Fields
        public const string I_ContractID = "ContractID";
        public const string I_InvoiceType = "InvoiceType";

        public const string I_BillingType = "BillingType";
        public const string I_FromDate = "FromDate";
        public const string I_ToDate = "ToDate";
        public const string I_Lastbilledmeter = "Lastbilledmeter";
        public const string I_Currentmeter = "Currentmeter";

        public const string I_PoolCode = "PoolCode";
        public const string I_UsedMeter = "UsedMeter";

        //Documents Fields
        public const string D_IsCovered = "IsCovered";
        public const string D_ServiceCallFlag = "ServiceFlag";
        public const string D_ServiceCallNo = "ServiceCallNo";

        //Service Call Fields
        public const string SC_PoolID = "PoolID";
        public const string SC_RealTimeTimer = "RealTimeTimer";
        public const string SC_CustomerWorkingHrs = "CustomerWorkingHrs";
        public const string SC_Recall = "Recallflag";
        public const string SC_JEN = "JENormal";
        public const string SC_JET = "JETravel";
        public const string SC_RONDate = "RONDate";
        public const string SC_RONTime = "RONTime";
        public const string SC_ResTime = "ResTime";

        //Activity
        public const string A_respByDate = "respByDate";
        public const string A_respByTime = "respByTime";

        ///Service Call Meter Fields
        public const string SC_SerCallDocNum = "SerCallDocNum";
        public const string SC_MeterCode = "MeterCode";
        public const string SC_Description = "Description";
        public const string SC_LastReading = "LastReading";
        public const string SC_LastReadingDate = "LastReadingDate";
        public const string SC_CurrentReading = "CurrentReading";
        public const string SC_ItemCode = "ItemCode";
        public const string SC_ItemName = "ItemName";
        public const string SC_ManuFSN = "ManuFSN";
        public const string SC_InternalSN = "IntenalSN";
        public const string SC_MeterReset = "MeterReset";
        public const string SC_Billed = "Billed";
        public const string SC_A3MeterReading = "A3MeterReading";
        public const string SC_DocEntry = "DocEntry";
        public const string SC_DocType = "DocType";
        public const string SC_PoolCode = "PoolCode";
        public const string SC_Attachment = "Attachment";
        public const string SC_Line = "Line";

        //public const string SC_Attachment = "Attachment";
        //public const string SC_Attachment = "Attachment";



        //Equipment Card Fields
        public const string EC_RecallDays = "RecallDays";
        public const string EC_Longitude = "Longitude";
        public const string EC_Latitude = "Latitude";
        public const string EC_InstallationDate = "InstallationDate";
        public const string EC_PDIDate = "PDIDate";
        public const string EC_ResponseTime = "ResponseTime";
        public const string EC_MonWDay = "Monday";
        public const string EC_TueWDay = "Tuesday";
        public const string EC_WedWDay = "Wednesday";
        public const string EC_ThurWDay = "Thursday";
        public const string EC_FriWDay = "Friday";
        public const string EC_SatWDay = "Saturday";
        public const string EC_SunWDay = "Sunday";
        public const string EC_MonStartTime = "MondayStartTime";
        public const string EC_MonEndTime = "MondayEndTime";
        public const string EC_TueStartTime = "TuesdayStartTime";
        public const string EC_TueEndTime = "TuesdayEndTime";
        public const string EC_WedStartTime = "WednesdayStartTime";
        public const string EC_WedEndTime = "WednesdayEndTime";
        public const string EC_ThuStartTime = "ThursdayStartTime";
        public const string EC_ThuEndTime = "ThursdayEndTime";
        public const string EC_FriStartTime = "FridayStartTime";
        public const string EC_FriEndTime = "FridayEndTime";
        public const string EC_SatStartTime = "SaturdayStartTime";
        public const string EC_SatEndTime = "SaturdayEndTime";
        public const string EC_SunStartTime = "SundayStartTime";
        public const string EC_SunEndTime = "SundayEndTime";

        public const string EC_MonStartTimeOS = "MondayStartTimeOS";
        public const string EC_MonEndTimeOS = "MondayEndTimeOS";
        public const string EC_TueStartTimeOS = "TuesdayStartTimeOS";
        public const string EC_TueEndTimeOS = "TuesdayEndTimeOS";
        public const string EC_WedStartTimeOS = "WednesdayStartTimeOS";
        public const string EC_WedEndTimeOS = "WednesdayEndTimeOS";
        public const string EC_ThuStartTimeOS = "ThursdayStartTimeOS";
        public const string EC_ThuEndTimeOS = "ThursdayEndTimeOS";
        public const string EC_FriStartTimeOS = "FridayStartTimeOS";
        public const string EC_FriEndTimeOS = "FridayEndTimeOS";
        public const string EC_SatStartTimeOS = "SaturdayStartTimeOS";
        public const string EC_SatEndTimeOS = "SaturdayEndTimeOS";
        public const string EC_SunStartTimeOS = "SundayStartTimeOS";
        public const string EC_SunEndTimeOS = "SundayEndTimeOS";


        public const string EC_Endoflife = "EndofLife";
        public const string EC_GatePassRequired = "GatePassRequired";
        public const string EC_PACINumber = "PACINo";
        public const string EC_CustomerWrkHrs = "CustomerWorkingHours";
        public const string EC_MeterReadHistory = "MeterReadingHistory";

        //Equipment - Meter Details
        public const string EM_InsNo = "InsNo";
        public const string EM_MeterCode = "MeterCode";
        public const string EM_MeterName = "MeterName";
        public const string EM_ItemMeterCode = "ItemMeterCode";
        public const string EM_PaperType = "PaperType";
        public const string EM_PrintType = "PrintType";
        public const string EM_BasePrice = "BasePrice";
        public const string EM_ExcessPrice = "ExcessPrice";
        public const string EM_CountedA3inA4 = "CountedA3inA4";
        public const string EM_LastReading = "LastReading";
        public const string EM_LastReadingDate = "LastReadingDate";
        public const string EM_SerialNo = "SerialNo";
        public const string EM_Line = "Line";
        public const string EM_CodeT = "CodeT";
        public const string EM_Model = "Model";

        //Equipment - Component Details
        public const string EC_ProductCode = "ProductCode";
        public const string EC_ProductName = "ProductName";
        public const string EC_Active = "Active";

        //Equipment Card - Machine details (UDT)
        public const string MD_BillDate = "BillDate";
        public const string MD_DocNum = "DocNum";
        public const string MD_MeterCode = "MeterCode";
        public const string MD_MeterCde = "MeterCde";
        public const string MD_MeterName = "MeterName";
        public const string MD_ItemCode = "ItemCode";
        public const string MD_SerialNum = "SerialNum";
        public const string MD_PoolCode = "PoolCode";
        public const string MD_StartMeterReading = "StartMeterReading";
        public const string MD_LastMeterReading = "LastMeterReading";
        public const string MD_CurntMeterReading = "CurrentMeterReading";
        public const string MD_RLastMeterReading = "RLastMeterReading";
        public const string MD_RCurntMeterReading = "RCurrentMeterReading";
        public const string MD_Billed = "Billed";
        public const string MD_Reset = "Reset";
        public const string MD_EligQuantity = "EligQuantity";
        public const string MD_Price = "Price";
        public const string MD_ExcessPrice = "ExcessPrice";
        public const string MD_DocType = "DocType";
        public const string MD_LastReading = "LastReading";


        //Equipment - Consumption History Details
        public const string EH_ProductCode = "ProductCode";
        public const string EH_ProductName = "ProductName";
        public const string EH_Yield = "LifeofProduct";
        public const string EH_Type = "Type";
        public const string EH_Active = "Active";
        public const string EH_YieldN = "Yield";

        //Service Contract Fields
        // public const string SH_Status = "Status";
        //  public const string SH_Type = "Type";
        public const string SH_IsCoverage = "IsCoverage";
        public const string SH_IsPoolContract = "IsPoolContract";
        public const string SH_ContractType = "ContractType";
        public const string SH_PoolSize = "PoolSize";
        public const string SH_PriceType = "PriceType";
        public const string SH_ContractCoverage = "ContractCoverage";

        //Pricing Tab Fields
        public const string SP_PoolCode = "PoolCode";
        public const string SP_EligQuantity = "EligibleQuantity";
        public const string SP_Price = "Price";
        public const string SP_ExcessPrice = "ExcessPrice";
        public const string SP_ItemCode = "ItemCode";
        public const string SP_FPrice = "FixedPrice";
        public const string SP_TPrice = "TotalPrice";

        //Meter Tab Fields

        public const string SH_PoolCode = "PoolCode";
        public const string SH_MeterCode = "MeterCode";
        public const string SH_MeterName = "MeterName";
        public const string SH_ItemCode = "ItemCode";
        public const string SH_Serial = "Serial";
        public const string SH_MItemCode = "MeterItemCode";
        public const string SH_Flag = "Flag";
        public const string SH_FixedPrice = "FixedPrice";
        public const string SH_Interval = "Interval";
        public const string SH_SKIP = "Skip";

        //Billing Tab Fields
        public const string SB_ContractNo = "ContractNo";
        public const string SB_IsFixedPrice = "IsFixedPrice";
        public const string SB_BillingType = "BillingType";
        public const string SB_BillingProcessType = "BillingProcessType";
        public const string SB_BillingCycle = "BillingCycle";
        public const string SB_BillStartDate = "BillStartDate";
        public const string SB_LastBilledDate = "LastBilledDate";
        public const string SB_NextBilledDate = "NextBilledDate";
        public const string SB_FixedPrice = "FixedPrice";
        public const string SB_FixedItem = "FixedItem";

        //Service Contract Row Fields
        //Item Matrix
        public const string SI_AMCV = "AMCV";
        public const string SI_RMCV = "RMCV";
        public const string SI_StartMtrReading = "StartMeterReading";
        public const string SI_PoolCode = "PoolCode";
        public const string SI_ApplyCoverage = "ApplyCoverage";
        public const string SI_ServiceAmount = "ServiceAmount";
        public const string SI_Source = "Source";
        public const string SI_Termination = "Termination";

        //Machine Pricing Master Fields
        public const string MP_ModelNumber = "ModelNo";
        public const string MP_ModelName = "ModelName";

        //Machine Pricing Master Row Fields
        public const string MP_ProductCategory = "ProductCategory";
        public const string MP_MeterCode = "MeterCode";
        public const string MP_PaperSize = "PaperSize";
        public const string MP_Color = "Color";
        public const string MP_StartRange = "StartRange";
        public const string MP_ToRange = "ToRange";
        public const string MP_BasePrice = "BasePrice";
        public const string MP_ExcessPrice = "ExcessPrice";

        //Technical Skill Master Fields
        public const string TS_ModelNumber = "ModelNo";
        public const string TS_ModelName = "ModelName";

        //Technical Skill Master Row Fields
        public const string TS_TechnicianCode = "TechCode";
        public const string TS_TechnicianName = "TechName";


        //Meter Setup Fields
        public const string ItemCode = "ItemCode";
        public const string ItemName = "ItemName";
        public const string Status = "Status";


        //Employee Master Fields
        public const string EP_WebUserName = "UserName";
        public const string EP_WebPassword = "Password";
        public const string EP_Confirmed = "Confirmed";
        public const string EP_UserType = "UserType";
        public const string EP_Location = "Location";
        public const string EP_LoclastUpdateTime = "LocLstUpdtTime";

        //Inventory Transfer
        public const string IT_Notification = "Notification";
        public const string IT_Email = "Email";

        //Coverages Master Fields 
        public const string CV_CoverageDate = "CoverageDate";
        public const string CV_Remarks = "Remarks";
        public const string CV_Active = "Active";

        //Covergae Master Row Fields
        public const string CV_ItemGrpCode = "ItemGrpCode";

        //Contract Coverage Header Fields
        public const string CC_ContractNo = "ContractNo";
        public const string CC_ItemCode = "ItemCode";
        public const string CC_CoverageCode = "CoverageCode";
        public const string CC_CoverageName = "CoverageName";
        public const string CC_Serial = "Serial";

        //Contract Coverag Row Fields
        public const string CC_ItemGroupCode = "ItemGroupCode";
        public const string CC_Active = "Active";

        //Service Contract Schdule Header and Row Fields
        public const string SC_ContractNo = "ContractNo";

        public const string SC_ScheduleType = "ScheduleType";
        public const string SC_Frequency = "Frequency";
        public const string SC_Day = "ScheduleDay";
        public const string SC_Posted = "Posted";
        public const string SC_Interval = "Interval";

        //Lease Contract Header Fields
        public const string LC_CustomerCode = "CustomerCode";
        public const string LC_CustomerName = "CustomerName";
        public const string LC_ContctCode = "ContctCode";
        public const string LC_StartDate = "StartDate";
        public const string LC_EndDate = "EndDate";
        public const string LC_Status = "Status";

        //General Tab
        public const string LC_ContractType = "ContractType";
        public const string LC_BillingCycle = "BillingCycle";
        public const string LC_SODocNo = "SODocNo";
        public const string LC_ContractValue = "ContractValue";
        public const string LC_ContractNo = "ContractNo";
        public const string LC_BillingDay = "BillingDay";
        public const string LC_BillStartDate = "BillStartDate";
        public const string LC_BillInterval = "BillInterval";

        public const string LC_InterestRate = "InterestRate";
        public const string LC_RevRecogAcct = "RevRecogAcct";
        public const string LC_RevenueAcct = "RevenueAcct";
        public const string LC_InterestAcct = "InterestAcct";
        public const string LC_DiffIntAcct = "DiffInterestAcct";

        public const string LC_MonthlyValue = "MonthlyValue";
        public const string LC_PaymentTerms = "PaymentTerms";
        public const string LC_ServiceTotal = "ServiceTotal";
        public const string LC_ItemCode = "ItemCode";
        public const string LC_ItemDesc = "ItemDesc";
        public const string LC_ItemCodeLR = "ItemCodeLR";
        public const string LC_ItemDescLR = "ItemDescLR";

        public const string LC_InterestAmt = "InterestAmt";
        public const string LC_LoanAmt = "LoanAmt";
        public const string LC_Termdate = "Termdate";
        public const string LC_DocEntry = "DocEntry";
        public const string LC_JENumber = "JENumber";
        public const string LC_JEDate = "JEDate";
        public const string LC_Remarks = "Remarks";

        //Lease Contract Row - Lease Billing Fields
        public const string LB_Interval = "Interval";
        public const string LB_LBDate = "LBDate";
        public const string LB_LeaseBilling = "LeaseBilling";
        public const string LB_Status = "Status";
        public const string LB_InvoiceNo = "InvoiceNo";
        public const string LB_Posted = "Posted";

        public const string LB_Balance = "Balance";
        public const string LB_Principal = "Principal";
        public const string LB_Interest = "Interest";
        public const string LB_IStatus = "IStatus";

        //Lease Contract Row - Item Fields
        public const string LI_ItemCode = "ItemCode";
        public const string LI_ItemDesc = "ItemDescp";
        public const string LI_Quantity = "Quantity";
        public const string LI_SerialNo = "SerialNo";

        //Lease Contract Row - Document Fields
        public const string LD_DocumentTyp = "DocType";
        public const string LD_DocNo = "DocNo";
        public const string LD_DocDate = "DocDate";
        public const string LD_DocStatus = "DocStatus";

        //Pool Details Table
        public const string PL_DocumentTyp = "DocType";
        public const string PL_DocNo = "DocNo";
        public const string PL_BillDate = "BillDate";
        public const string PL_DocEntry = "DocEntry";
        public const string PL_PoolCode = "PoolCode";
        public const string PL_StartMeter = "StartMeter";
        public const string PL_EndMeter = "EndMeter";
        public const string PL_Used = "Used";
        public const string PL_FreeCopied = "FreeCopied";
        public const string PL_Excess = "Excess";
        public const string PL_BillingPrice = "BillingPrice";
        public const string PL_ExcessPrice = "ExcessPrice";
        public const string PL_FixedPrice = "FixedPrice";
        public const string PL_Total = "Total";
        public const string PL_IsBilled = "IsBilled";
        public const string PL_MeterConsider = "MeterConsider";

        //Scheduler Table
        public const string SC_CallSource = "CallSource";
        public const string SC_ProbSubType = "ProbSubType";
        public const string SC_ActivityChannel = "ActivityChannel";
        public const string SC_CallType = "CallType";

        //Service GL
        public const string SC_BillProcees = "BillProcees";
        public const string SC_Account = "Account";

        //Recurring Schedule
        //Z_ORSS
        public const string RS_StartDate = "RStartDate";
        public const string RS_EndDate = "REndDate";
        public const string RS_ContractNo = "RContractNo";
        public const string RS_Customer = "RCustomer";
        public const string RS_BillProcess = "RBillProcess";
        public const string RS_BillValue = "RBillValue";
        public const string RS_BillTerms = "RBillTerms";
        public const string RS_DayValue = "RDayValue";
        public const string RS_MonthValue = "RMonthValue";
        public const string RS_DebitAcc = "RDebitAcc";
        public const string RS_CreditAcc = "RCreditAcc";

        //Z_RSS1
        public const string RS_Month = "RMonth";
        public const string RS_RAmount = "RAmount";
        public const string RS_RPosted = "RPosted";
        public const string RS_RStatus = "RStatus";
        public const string RS_RDate = "RDate";
        public const string RS_RYear = "RYear";

        //Z_InstChecklist
        public const string PDI_CheckListItem = "PDICLItem";
        public const string PDI_Active = "PDIActive";
       

        //Z_PDIInspection

        public const string PDI_Model = "PDIModel";
        public const string PDI_ModelN = "PDIModelN";
        public const string PDI_SerialNumber = "PDISNumber";
        public const string PDI_Status = "Status";
        public const string PDI_date = "PDIdate";
        public const string PDI_Starttime = "PDIStarttime";
        public const string PDI_Endtime = "PDIEndtime";
        public const string PDI_Totalduration = "PDITduration";
        public const string PDI_Remarks = "PDIRemarks";
        public const string PDI_PendingRemarks = "PDIPenRemarks";
        public const string PDI_TotalMeterReading = "PDITMReading";
        public const string PDI_Technician = "PDITechnician";
        public const string PDI_TechnicianC = "PDITechnicianC";
        public const string PDI_PDISign = "PDISign";
       

        //Z_InstMeterlist
        public const string PDI_Meter = "PDIMeter";
        public const string PDI_MeterReading = "PDIMReading";
       

        //MODEL
        public const string MODEL_ReCallCopy = "ReCallCopy";
        public const string MODEL_Recall = "Recall";
        public const string MODEL_MExAMC = "MExAMC";
        public const string MODEL_MxExpAmc = "MxExpAmc";
        public const string MODEL_SOT = "SOT";
        public const string MODEL_EOS = "EOS";
        public const string MODEL_EOL = "EOL";
        public const string MODEL_PrdSubCat = "PrdSubCat";
        public const string MODEL_BWSpeed = "BWSpeed";
        public const string MODEL_ClrSpeed = "ClrSpeed";
        public const string MODEL_ResTime = "ResTime";
        public const string MODEL_Ctgry = "Ctgry";
        public const string MODEL_PlnDays = "PlnDays";
        public const string MODEL_PlnCpy = "PlnCpy";
        public const string MODEL_Jbwgt = "Jbwgt";
        public const string MODEL_JbTime = "JbTime";

        public const string MODEL_Type = "Type";
        public const string MODEL_QuoteLimit = "QuoteLimit";
        public const string MODEL_DflEmail = "DflEmail";
       

        //RELATED ITEM TAB
        public const string RITM_ItemCode = "ItemCode";
        public const string RITM_PartN = "PartN";
        public const string RITM_Descp = "Descp";
        public const string RITM_Status = "Status";

        //METER SETUP TAB
        public const string METER_MeterCode = "Code";
        public const string METER_MeterName = "Name";
        public const string METER_BillingCode = "ItemCode";
        public const string METER_BillingName = "ItemName";

        //PRICE SETUP TAB
        public const string PRICE_MeterCode = "Code";
        public const string PRICE_SMAPrice = "SMAPrice";
        public const string PRICE_SRange = "SRange";
        public const string PRICE_TRange = "TRange";


        // Notification
        public const string NOT_Type = "Type";
        public const string NOT_Code = "Code";
        public const string NOT_Title = "Title";
        public const string NOT_Details = "Details";
        public const string NOT_PageUrl = "PageUrl";
        public const string NOT_Sender = "Sender";
        public const string NOT_SentTo = "SentTo";
        public const string NOT_IsRead = "IsRead";
        public const string NOT_IsDeleted = "IsDeleted";
        public const string NOT_IsReminder = "IsReminder";
        public const string NOT_CreateDate = "CreateDate";
        public const string NOT_UpdateDate = "UpdateDate";





    }
}
