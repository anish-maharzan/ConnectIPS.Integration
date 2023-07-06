using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using MainLibrary.SAPB1;


namespace GlobalVariable
{
    public static class Globals
    {

        public static void setgroupcode(string customerCode, SAPbouiCOM.ComboBox control)
        {
            try
            {

                if (!string.IsNullOrEmpty(customerCode))
                {
                    string query = "select OCQG.\"GroupName\" , OCQG.\"GroupName\"" +
                                    "from OCRD CROSS JOIN OCQG where OCRD.\"CardCode\" = '" + customerCode + "' and \"CardType\" = 'C' and";

                    for (int i = 1; i < 65; i++)
                    {
                        if (i == 1)
                        {
                            query += "((OCRD.\"QryGroup" + i + "\" = 'Y' and OCQG.\"GroupCode\" = " + i + " )";
                        }
                        else
                        {
                            query += "or (OCRD.\"QryGroup" + i + "\" = 'Y' and OCQG.\"GroupCode\" = " + i + ")";
                        }
                    }
                    query += ")";

                    //string Query = "SELECT T0.\"GroupCode\", T0.\"GroupName\" FROM OCQG T0 WHERE T0.\"GroupName\" NOT LIKE 'Business%' ";
                    B1Helper.ClearCombo(control);
                    B1Helper.SAPFillComboValues(control, query);

                }
            }
            catch
            {
            }
        }
        public static string SEmail { get; private set; }
        public static string AEmail { get; private set; }

        public static string NEmail { get; private set; }
        public static string Journal { get; private set; }

        public static double  ExcessDay { get; private set; }

        public static string Advance { get; private set; }

        public static string Credit { get; private set; }


        public static string DeleteForm { get; private set; }

        public static Int32 serviceformtype { get; private set; }

        public static string Fromwarehouse { get; private set; }

        public static string Towarehouse { get; private set; }
    
        // GlobalInt can be changed only via this method

        public static void SetsFromwarehouse(string newInt)
        {
            Fromwarehouse = newInt;

        }
        public static void SetsTowarehouse(string newInt)
        {
            Towarehouse = newInt;
        }
        public static void Setsserviceformtype(Int32 newInt)
        {
            serviceformtype = newInt;

        }

        public static void SetsExcessDay(double  newInt)
        {
            ExcessDay = newInt;
            
        }
        public static void SetsSEmail(string newInt)
        {
            SEmail = newInt;
        }

        public static void SetsAEmail(string newInt)
        {
            AEmail = newInt;
        }

        public static void SetsNEmail(string newInt)
        {
            NEmail = newInt;
        }

        public static void SetsJournal(string newInt)
        {
            Journal = newInt;
        }

        public static void SetsSAdvance(string newInt)
        {
            Advance  = newInt;
        }

        public static void SetsSCredit(string newInt)
        {
            Credit  = newInt;
        }

        public static void SetsDeleteForm(string newInt)
        {
            DeleteForm  = newInt;
        }
      
    }





}
