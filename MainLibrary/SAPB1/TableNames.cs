using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.SAPB1
{
    public static class TableNames
    {
        // System Tables
        public const string T_OCRD = "OCRD";
        public const string T_RDR1 = "RDR1";
        public const string T_OINV = "OINV";
        public const string T_INV1 = "INV1";
        public const string T_OSCL = "OSCL";
        public const string T_OINS = "OINS";
        public const string T_OCTR = "OCTR";
        public const string T_CTR1 = "CTR1";
        public const string T_OHEM = "OHEM";
        public const string T_OCGL = "OCLG";
        public const string T_OUSR = "OUSR";
        public const string T_OADM = "OADM";
        public const string T_OWTR = "OWTR";
        public const string T_OCLG = "OCLG";
       
        // Approval Stages
        public const string Z_OAPS = "OAPS";
        public const string Z_APS1 = "APS1";

        //Metter Setups Table
        public const string Z_OMTR = "Z_OMTR";

        //Service Call Meter Table
        public const string Z_OSCM = "Z_OSCM";
        public const string Z_SCM1 = "Z_SCM1";

        //Service Call Installations
        public const string Z_OINSS = "Z_OINSS";
        public const string Z_INSS1 = "Z_INSS1";


        //Equipment Card Table
        public const string Z_OECC = "Z_OECC";
        public const string Z_ECM1 = "Z_ECM1";
        public const string Z_ECD2 = "Z_ECD2";
        public const string Z_ECH3 = "Z_ECH3";

        //Equipment Card - Machine Details
        public const string Z_ECMD = "Z_ECMD";

        //Service Contract Meter Tables
        public const string Z_OSRM = "Z_OSRM";
        public const string Z_SRM1 = "Z_SRM1";

        // Service Contract Price Tables
        public const string Z_OSRP = "Z_OSRP";
        public const string Z_SRP1 = "Z_SRP1";

        // Service Billing Table
        public const string Z_OSRB = "Z_OSRB";
        public const string Z_SRB1 = "Z_SRB1";

        // Machine Pricing Master Tables
        public const string Z_MPRM = "Z_MPRM";
        public const string Z_MPM1 = "Z_MPM1";

        //Technical Skill Master Tables
        public const string Z_OTSM = "Z_OTSM";
        public const string Z_TSM1 = "Z_TSM1";

        // Coverages  Master Table
        public const string Z_OCVM = "Z_OCVM";
        public const string Z_CVM1 = "Z_CVM1";

        // Contract Coverages Table
        public const string Z_OCCV = "Z_OCCV";
        public const string Z_CCV1 = "Z_CCV1";

        // Contract Schduler TabLe
        public const string Z_OCSC = "Z_OCSC";
        public const string Z_CSC1 = "Z_CSC1";

        // Lease Contract Form
        public const string Z_OLCM = "Z_OLCM";
        public const string Z_LCIT = "Z_LCIT";
        public const string Z_LCDD = "Z_LCDD";
        public const string Z_LCLB = "Z_LCLB";

        //Pool Details Table
        public const string Z_PLDT = "Z_PLDT";

        //Schedule
        public const string Z_SCHEDULER = "Z_SCHEDULER";

        //Service GL Details
        public const string Z_SCGL = "Z_SCGL";

        //*************Recurring Schedule
        public const string Z_ORSS = "Z_ORSS";
        public const string Z_RSS1 = "Z_RSS1";

        public const string Z_Pool = "Z_POOL";

        // Installation Checklist - Setup
        public const string Z_InstChecklist = "Z_INSTCHECKLIST";

        //PDI Checklist - Setup
        public const string Z_PDIChecklist = "Z_PDICHECKLT";

        //PDI Inspection
        public const string Z_PDIInspection = "Z_PDIINSPECTION";
        public const string Z_PDIInsChecklist = "Z_PDIINSCHECKLIST";
        public const string Z_PDIInsMeterReading = "Z_PDIINSMETER";
        
        //MODEL

        public const string Z_Model = "MODEL";
        public const string Z_RelatedItem = "Z_RITEM";
        public const string Z_MeterSetup = "Z_METERSETUP";
        public const string Z_Price = "Z_PRICESETUP";


        // Notification
        public const string Z_NOTIF = "Z_NOTIF";


    }
}
