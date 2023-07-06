using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.SAPB1
{
    public class Constants
    {
        public const string Hash = "#";
        public const int CREATED = 1;
        public const int CANCELLED = 2;
        public const string CurrenyEGP = "EGP";
    }

    public static class AccountCodes
    {
        //EGP Account Codes
        public const string EGPARAcctCode = "1272222";
        public const string EGPUnERevAcctCode = "1272226";
        public const string EGPOthARAcctCode = "1111111";
        public const string EGPFIAcctCode = "1272222";
        public const string EGPAdvLeaseAcctCode = "2163111";

        //Non EGP Account Codes
        public const string NonEGPARAcctCode = "1272223";
        public const string NonEGPUnERevAcctCode = "1272227";
        public const string NonEGPOthARAcctCode = "1111112";
        public const string NonEGPFIAcctCode = "1272223";
        public const string NonEGPAdvLeaseAcctCode = "2163112";

        //Common Account Codes
        public const string LeasedPSActCode = "2151111";
        public const string ProvExpPLAcctCode = "5231111";
        public const string ProvExpBSAcctCode = "1273222";
        public const string AdmnFeeAcctCode = "4111111";
        public const string ArngFeeAcctCode = "4111112";
        public const string AdvLPAcctCode = "1272224";
        public const string LeaseReserveAcctCode = "2252222";
        public const string OthersIncAcctCode = "4111116";
        public const string LCClearingAcctCode = "1175112";

        public const string EGPRInvAcctCode = "1111115";
        public const string NonEGPRInvAcctCode = "1111116";
        public const string EGPOverDueAcctCode = "1111113";
        public const string NonEGPOverDueAcctCode = "1111114";

        public const string claimAcctCode = "2161114";
        public const string ClaimLCAcctCode = "1175112";

        //Revenue Recognition Account Codes
        public const string EASLeaseSettAcctCode = "1262222";
        public const string FinLeaseIntrstAcctCode = "4211111";
        public const string FinLeaseInsuAcctCode = "4211112";
        public const string OpeLeaseMiscAcctCode = "4211120";
        public const string OpeLeaseRegAcctCode = "4211121";
        public const string OpeLeaseRepcAcctCode = "4211122";
        public const string OpeLeaseRdAsAcctCode = "4211123";
        public const string OpeLeaseTiersAcctCode = "4211124";
        public const string OpeLeaseInsuAcctCode = "4211125";
        public const string OpeLeaseMainAcctCode = "4211126";

        public const string ConsordianSec4AcctCode = "2131112";
        public const string ConsordianSec5AcctCode = "2131113";
        public const string ConsordianSec6AcctCode = "2131114";

        //Testing Account Coode for Control Accounts
        public const string NonControlAccountCode = "1233222";
        public const string NonControlAccountCode1 = "1243230";
    }

    public static class BillingCycles
    {
        public const int Monthly = 1;
        public const int Quaterly = 3;
        public const int SemiAnnual = 6;
        public const int Annual = 12;

    }
}
