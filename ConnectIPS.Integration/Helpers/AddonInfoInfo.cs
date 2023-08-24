using GlobalVariable;
using MainLibrary.SAPB1;
using MainLibrary.Utilities;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;

namespace ConnectIPS.Integration.Helpers
{
    public class AddonInfoInfo
    {
        #region Members
        public int Index { get; set; }
        public bool isHana { get; set; }
        private static int RetCode = 0;
        private static string ErrMsg = null;
        #endregion

        #region Constructor
        public AddonInfoInfo()
        {
        }
        #endregion

        #region UDODEAFAUTFORMSFORLC
        public static void CreateUDOForms()
        {
            try
            {
                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];

               
            }
            catch
            {
            }
        }

        #endregion

        #region AutoUDO
        public static void AutoUDO(string code, string parm)
        {
            try
            {
                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];
                B1Helper.AddTable("ITN_" + parm, parm, BoUTBTableType.bott_MasterData);
                Array.Resize(ref FindColumn, 1);
                Array.Resize(ref FormColumn, 2);

                FormColumn[0] = "Code";
                FormColumn[1] = "Name";
                FindColumn[0] = "DocEntry";
                B1Helper.CreateUdo("UDO" + code, parm, "ITN_" + parm, "M", "Y", FormColumn, null);
            }
            catch
            {
            }
        }

        #endregion

        #region AutoCreateUDF
        public static void AutoCreateUDF(string parm)
        {
            Application.SBO_Application.StatusBar.SetText("Database structure is modifying...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            try
            {
                MainLibrary.SAPB1.B1Helper.AddField(parm, parm, "OITM", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                //  MainLibrary.SAPB1.B1Helper.AddField(parm, parm, "OITM", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
            }
            catch
            {
            }
        }

        #endregion
        
        #region Methods
        public static bool InstallUDOs()
        {
            try
            {
                bool UDOAdded = true;

                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];
                string[,] srt = new string[,] { { "N", "No" }, { "Y", "Yes" } };

                B1Helper.AddField("CONNIPS", "NCHL-NPI (Bank Integration)", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("TRANTYPE", "Connect IPS Transaction Type", "OVPM", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CATEGORY", "Category Purpose", "OVPM", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("VALIDATED", "Verified Account", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("PAYSTATUS", "Connect IPS Payment Status", "OVPM", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CIBATCH", "Connect IPS Batch", "OVPM", BoFieldTypes.db_Alpha, 254, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                B1Helper.AddField("PAYSTATUS", "Connect IPS", "OINV", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("TRACEID", "Validation TraceId", "OINV", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ISQRPAY", "Is QR Payment?", "OINV", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("QRAMT", "QR Payment Amount", "OINV", BoFieldTypes.db_Float, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, false, "");


                //B1Helper.AddTable("NCHL", "Connect IPS Integration", BoUTBTableType.bott_MasterData);
                //B1Helper.AddField("INBKACCT", "Incoming Bank Account", "NCHL", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                //Array.Resize(ref FormColumn, 2);
                //FormColumn[0] = "Code";
                //FormColumn[1] = "U_INBKACCT";
                //CreateUDO("NCHL", "Incoming Bank Account", "NCHL", FormColumn, BoUDOObjType.boud_MasterData, "F");

                B1Helper.AddTable("OLPT", "Terms of Payment", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("PAYTERMS", "Payment Terms", "OLPT", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                Array.Resize(ref ChildTable, 0);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_PAYTERMS";
                CreateUDO("OLPT", "Terms Of Payment", "OLPT", FormColumn, BoUDOObjType.boud_MasterData, "F");


                return UDOAdded;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                //B1Helper.DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                return false;
            }
        }

        private static bool CreateUDO(string CodeID, string Name, string TableName, string[] FormColoums, SAPbobsCOM.BoUDOObjType ObjectType, string ManageSeries)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = default(SAPbobsCOM.UserObjectsMD);
            try
            {
                oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
                if (oUserObjectMD.GetByKey(CodeID) == true)
                {
                    return true;
                }
                oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO;

                oUserObjectMD.Code = CodeID;
                oUserObjectMD.Name = Name;
                oUserObjectMD.TableName = TableName;
                oUserObjectMD.ObjectType = ObjectType;

                oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.MenuCaption = Name;
                oUserObjectMD.FatherMenuID = 47616;
                oUserObjectMD.Position = 0;
                oUserObjectMD.MenuUID = CodeID;

                if (FormColoums != null)
                {
                    for (int i = 0; i <= FormColoums.Length - 1; i++)
                    {
                        if (FormColoums[i].Trim() != "U_RUNDB")
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = FormColoums[i];
                            oUserObjectMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tNO;
                            oUserObjectMD.FormColumns.Add();
                        }
                        else
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = FormColoums[i];
                            oUserObjectMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tYES;
                            oUserObjectMD.FormColumns.Add();
                        }
                    }
                }
                // check for errors in the process
                RetCode = oUserObjectMD.Add();

                if (RetCode != 0)
                {
                    if (RetCode != -1)
                    {
                        Program.oCompany.GetLastError(out RetCode, out ErrMsg);
                        Program.SBO_Application.StatusBar.SetText("Object Failed : " + ErrMsg + "", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
                else
                {
                    Program.SBO_Application.StatusBar.SetText("Object Registered : " + Name + "", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool GetCommonSettings()
        {
            string query = "SELECT T0.\"U_A_Email\", T0.\"U_S_Email\", T0.\"U_J_Email\" , \"U_ExcessDay\" , \"U_N_Email\" FROM OADM T0";
            SAPbobsCOM.Recordset rsQry = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rsQry.DoQuery(query);
            if (rsQry.RecordCount > 0)
            {
                Globals.SetsAEmail(rsQry.Fields.Item(0).Value.ToString());
                Globals.SetsSEmail(rsQry.Fields.Item(1).Value.ToString());
                Globals.SetsJournal(rsQry.Fields.Item(2).Value.ToString());
                Globals.SetsExcessDay(Convert.ToDouble(rsQry.Fields.Item(3).Value.ToString()));
                Globals.SetsNEmail(rsQry.Fields.Item(4).Value.ToString());
            }

            query = "SELECT T0.\"U_BillProcees\", T0.\"U_Account\" FROM \"@Z_SCGL\"  T0";
            rsQry = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rsQry.DoQuery(query);
            if (rsQry.RecordCount > 0)
            {
                while (rsQry.EoF == false)
                {
                    if (rsQry.Fields.Item(0).Value.ToString() == "A")
                    { Globals.SetsSAdvance(rsQry.Fields.Item(1).Value.ToString()); }
                    else if (rsQry.Fields.Item(0).Value.ToString() == "C") { Globals.SetsSCredit(rsQry.Fields.Item(1).Value.ToString()); }
                    rsQry.MoveNext();
                }
            }
            rsQry = null;
            return true;

        }

        public static void SetFormFilter()
        {
            try
            {
                //SAPbouiCOM.EventFilters objFilters = new SAPbouiCOM.EventFilters();
                //SAPbouiCOM.EventFilter objFilter;

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_CLOSE);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
                //objFilter.AddEx("frm_TransferItems");

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_KEY_DOWN);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_RIGHT_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK);
                //objFilter.AddEx("frm_TransferItems");

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_PICKER_CLICKED);
                //objFilter.AddEx("frm_TransferItems");


                //SetFilter(objFilters);
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                // Log.LogException(LogLevel.Error, ex);
            }
        }

        public static void RemoveMenu(string menuId)
        {
            Application.SBO_Application.Menus.RemoveEx(menuId);
        }

        public static string GetNextEntryIndex(string tableName)
        {
            try
            {
                var result = B1Helper.GetNextEntryIndex(tableName);
                if (result.Equals(string.Empty))
                    result = "0";
                else
                    if (result.Equals("0"))
                    {
                        result = "1";
                    }

                return result;
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                // Log.LogException(LogLevel.Error, ex);
                return null;
            }

        }

        protected static void SetFilter(SAPbouiCOM.EventFilters Filters)
        {
            Application.SBO_Application.SetFilter(Filters);
        }

        #endregion
    }
}

