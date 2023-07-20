using ConnectIPS.Integration.Helpers;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Configuration;

namespace ConnectIPS.Integration
{
    class Program
    {
        #region Variables

        public static SAPbobsCOM.Company oCompany;
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbouiCOM.Form oForm { get; set; }

        #endregion

        /// <summary_>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //Application.SBO_Application.StatusBar.SetSystemMessage("Connecting to the Add-on", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                Application oApp = null;
                if (args.Length < 1)
                    oApp = new Application();
                else
                    oApp = new Application(args[0]);

                SBO_Application = SAPbouiCOM.Framework.Application.SBO_Application;
                oCompany = (SAPbobsCOM.Company)SBO_Application.Company.GetDICompany();
                Menu MyMenu = new Menu();
                Menu.addMenu();

                GlobalMethods_Variables.SetsCreateUDF(ConfigurationManager.AppSettings["UDF"].ToString());
                if (GlobalMethods_Variables.CreateUDF == "N")
                {
                    AddonInfoInfo.InstallUDOs();
                    //CreateFMS();
                }
                //AddonInfoInfo.GetCommonSettings();
                var applicationHandler = new ApplicationHandlers();
                var addonName = "ConnectIPS Integration";
                Application.SBO_Application.StatusBar.SetSystemMessage($"{addonName} Add-on installed successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static void CreateFMS()
        {
            int UserQueryCategoryID = GetUserQueryCategoryID("FMS", true);

            string PreFetchDocEQry = "SELECT \"U_ITN_Stages\" from OITT where \"Code\" =$[$6.0]";
            int PreFetchDocNQID = CreateUserQuery("Production Order Stages", PreFetchDocEQry, UserQueryCategoryID);
            AssignQuery(PreFetchDocNQID, "65211", "txtStage", "Stage", false, "Brand");
        }

        public static int GetUserQueryCategoryID(string categoryName, bool autoCreateCategory = false)
        {
            // Create a new User Category if the categoryName specified does not exist
            // Returns the Internal Key ID of the Category
            int catid = 0;
            SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {
                oRecordSet.DoQuery("SELECT \"CategoryId\", \"CatName\" FROM \"OQCN\" WHERE \"CatName\" = '" + categoryName + "'");

                if (!oRecordSet.EoF)
                {
                    catid = Convert.ToInt32(oRecordSet.Fields.Item("CategoryId").Value);
                }
                else
                {
                    if (autoCreateCategory)
                    {
                        // Add a New Query Category Called Integratech to the [OQCN] Table
                        SAPbobsCOM.QueryCategories qc = (SAPbobsCOM.QueryCategories)Program.oCompany.GetBusinessObject(BoObjectTypes.oQueryCategories);
                        qc.Name = categoryName;
                        qc.Permissions = "YYYYYYYYYYYYYYY";
                        if (qc.Add() == 0)
                        {
                            return Convert.ToInt32(Program.oCompany.GetNewObjectKey());
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // GC Release
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet);
            }

            return catid;
        }

        public static int CreateUserQuery(string name, string sql, int queryCategoryId)
        {
            // Check if query name exists first
            int queryId = GetUserQueryID(name);

            // Create if query doesn't exist
            if (queryId == 0)
            {
                SAPbobsCOM.UserQueries userQuery = (SAPbobsCOM.UserQueries)Program.oCompany.GetBusinessObject(BoObjectTypes.oUserQueries);
                try
                {


                    // Create new Query
                    userQuery.QueryType = UserQueryTypeEnum.uqtWizard;
                    userQuery.QueryCategory = queryCategoryId;
                    userQuery.QueryDescription = name;
                    userQuery.Query = sql;

                    if (userQuery.Add() == 0)
                    {
                        queryId = GetUserQueryID(name);
                        //**** Not Exist Query
                        return queryId;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(userQuery);
                }
            }

            else
            {

            }
            return queryId;
        }

        public static int GetUserQueryID(string name)
        {
            // Creates a new User Query if the name specified does not exist
            // Returns the Internal Key ID of the Query

            int queryID = 0;

            #region Lookup Query Name and Get ID if exists

            Recordset oRecordset = (Recordset)Program.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                string sql = "SELECT \"IntrnalKey\" FROM \"OUQR\" WHERE \"QName\" = '" + name + "'";

                oRecordset.DoQuery(sql);
                if (!oRecordset.EoF)
                {
                    // Return query ID as it already exists
                    return Convert.ToInt32(oRecordset.Fields.Item("intrnalkey").Value);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordset);
            }

            #endregion

            // Garbage Collection
            GC.Collect();

            return queryID;
        }

        public static void AssignQuery(int queryId, string formId, string itemId, string column = "-1", bool AutoRefresh = false, string FieldId = "")
        {
            // This will assign a query to a form item & column 
            // This will reassign a query to an item that already has a query assigned
            // CSHS Table = User Defined Values Table

            SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            try
            {
                // Validate query exists
                oRecordSet.DoQuery("SELECT \"IntrnalKey\", \"QName\" FROM \"OUQR\" WHERE \"IntrnalKey\" = " + queryId);
                bool queryExists = false;
                if (!oRecordSet.EoF)
                {
                    queryExists = true;
                }
                else
                {

                }

                if (queryExists)
                {
                    // Values Example
                    //string form = "VoucherCodes.Form1";
                    //string item = "PMatrix";
                    //string column = "PName";  // Matrix Column Name 

                    // Get UDV For Form Item
                    oRecordSet = (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery("SELECT \"IndexID\" FROM \"CSHS\" WHERE \"FormID\" = '" + formId + "' AND \"ItemID\" = '" + itemId + "' AND \"ColID\" = '" + column + "'");
                    if (!oRecordSet.EoF)
                    {
                        #region Update Existing

                        SAPbobsCOM.FormattedSearches fms = (SAPbobsCOM.FormattedSearches)Program.oCompany.GetBusinessObject(BoObjectTypes.oFormattedSearches);
                        fms.GetByKey(Convert.ToInt32(oRecordSet.Fields.Item("IndexID").Value));
                        fms.Action = BoFormattedSearchActionEnum.bofsaQuery;
                        if (AutoRefresh)
                        {
                            fms.ForceRefresh = BoYesNoEnum.tYES;
                            fms.FieldID = FieldId;
                            fms.Refresh = BoYesNoEnum.tYES;
                        }
                        fms.FormID = formId;
                        fms.ItemID = itemId;
                        fms.ColumnID = column;
                        fms.QueryID = queryId;
                        fms.Update();
                        //  Program.ErrorHandler(fms.Update(), "updating a form items assigned user defined values(fms)"); 

                        #endregion
                    }
                    else
                    {
                        #region Add New

                        SAPbobsCOM.FormattedSearches fms = (SAPbobsCOM.FormattedSearches)Program.oCompany.GetBusinessObject(BoObjectTypes.oFormattedSearches);
                        fms.Action = BoFormattedSearchActionEnum.bofsaQuery;
                        if (AutoRefresh)
                        {
                            fms.ForceRefresh = BoYesNoEnum.tYES;
                            fms.FieldID = FieldId;
                            fms.Refresh = BoYesNoEnum.tYES;
                        }
                        fms.FormID = formId;
                        fms.ItemID = itemId;
                        fms.ColumnID = column;
                        fms.QueryID = queryId;
                        fms.Add();
                        //  Program.ErrorHandler(fms.Add(), "adding a user defined value to form item");

                        #endregion
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // GC Release
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet);
            }
        }
    }
}
