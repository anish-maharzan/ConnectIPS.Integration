using MainLibrary.Utilities;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace MainLibrary.SAPB1
{
    public class B1Helper
    {
        #region Members
        private static bool returnValue;
        private static SAPbobsCOM.Company diCompany = null;
        #endregion

        #region Menu Methods
        public static void RemoveMenu(string MenuId)
        {
            try
            {
                Application.SBO_Application.Menus.RemoveEx(MenuId);
            }
            catch (Exception ex)
            {
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1Helper", "RemoveMenu", ex.ToString()));
            }
        }
        public static void AddMenu(string MenuId, string MenuValue)
        {
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;

            try
            {
                //  CREATE MENU POPUP MYUSERMENU01 AND ADD IT TO TOOLS MENU
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
                oCreationPackage.Image = string.Concat(System.Windows.Forms.Application.StartupPath, @"\Images\logo.bmp");
                oMenuItem = Application.SBO_Application.Menus.Item(MenuUid.DATA); // Data'

                if (!oMenuItem.SubMenus.Exists(MenuId))
                {
                    oMenus = oMenuItem.SubMenus;
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = MenuId;
                    oCreationPackage.String = MenuValue;
                    oCreationPackage.Enabled = true;
                    oCreationPackage.Position = 1;
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddLineMenu", ex.ToString()), SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
        public static void RemoveMenuItem(string FatherID, string UniqueID, String Name)
        {
            RemoveMenuItem(FatherID, UniqueID, Name, -1);
        }
        public static void RemoveMenuItem(string FatherID, string UniqueID, String Name, int Position)
        {
            try
            {
                // Get the menu collection of the newly added pop-up item
                var oMenuItem = Application.SBO_Application.Menus.Item(FatherID);
                var oMenus = oMenuItem.SubMenus;
                SAPbouiCOM.MenuCreationParams oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = UniqueID;
                oCreationPackage.String = Name;
                oCreationPackage.Position = Position;
                if (oMenus.Exists(UniqueID))
                    oMenus.RemoveEx(UniqueID);
                //oMenus.AddEx(oCreationPackage);
            }
            catch (Exception ex)
            {
                //  Menu already exists
                ////Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public static void addMenuItem(string FatherID, string UniqueID, String Name)
        {
            addMenuItem(FatherID, UniqueID, Name, -1);
        }
        public static void addSubMenu(string FatherID, string UniqueID, string Name, int position)
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = Application.SBO_Application.Menus.Item(FatherID); // moudles'

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = UniqueID;
            oCreationPackage.String = Name;
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = position;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the menu already exists this code will fail
                if (oMenus.Exists(UniqueID))
                    oMenus.RemoveEx(UniqueID);
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "addSubMenu", ex.ToString()), SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

        }
        public static void addMenuItem(string FatherID, string UniqueID, String Name, int Position)
        {
            try
            {
                // Get the menu collection of the newly added pop-up item
                var oMenuItem = Application.SBO_Application.Menus.Item(FatherID);
                var oMenus = oMenuItem.SubMenus;
                SAPbouiCOM.MenuCreationParams oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = UniqueID;
                oCreationPackage.String = Name;
                oCreationPackage.Position = Position;
                if (oMenus.Exists(UniqueID))
                    oMenus.RemoveEx(UniqueID);
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception ex)
            {
                //  Menu already exists
                ////Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
        public static void AddSubMenu(string FatherID, string UniqueID, string Name, int position, string image)
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = Application.SBO_Application.Menus.Item(FatherID); // moudles'

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = UniqueID;
            oCreationPackage.String = Name;
            //   if (image != null)
            //   oCreationPackage.Image = image;
            oCreationPackage.Image = string.Concat(System.Windows.Forms.Application.StartupPath, @"\Images\logo.bmp");
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = position;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the menu already exists this code will fail
                if (oMenus.Exists(UniqueID))
                    oMenus.RemoveEx(UniqueID);
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception ex)
            {
                ////Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddSubMenu", ex.ToString()), SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
        #endregion

        #region UDO METHODS

        public static GeneralData GetUdoObject(string udoName, string keyName, string keyValue)
        {
            SAPbobsCOM.GeneralService oClassSubjectsGeneralService;
            SAPbobsCOM.GeneralData oClassSubjectsHeaderGeneralData;
            SAPbobsCOM.GeneralDataParams oClassSubjectGeneralCollectionParams;
            try
            {
                DiCompany.StartTransaction();

                SAPbobsCOM.UserObjectsMD UserObjectMD = (SAPbobsCOM.UserObjectsMD)DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

                oClassSubjectsGeneralService = (SAPbobsCOM.GeneralService)DiCompany.GetCompanyService().GetGeneralService(udoName);
                oClassSubjectsHeaderGeneralData = (SAPbobsCOM.GeneralData)oClassSubjectsGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                oClassSubjectGeneralCollectionParams = (SAPbobsCOM.GeneralDataParams)oClassSubjectsGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                oClassSubjectGeneralCollectionParams.SetProperty(keyName, keyValue);

                oClassSubjectsHeaderGeneralData = oClassSubjectsGeneralService.GetByParams(oClassSubjectGeneralCollectionParams);

                if (DiCompany.InTransaction)
                {
                    DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
                return oClassSubjectsHeaderGeneralData;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                return null;
            }
        }
        public static bool CreateUdo(string udoName, string description, string tableName, string DocumentType, string defaultform = null, string[] FormColoums = null, params string[] childTables)
        {
            return CreateUdo(udoName, description, tableName, DocumentType, null, defaultform, FormColoums, childTables);
        }
        public static bool CreateUdo(string udoName, string description, string tableName, string DocumentType, List<string> findFields, string defaultform = null, string[] FormColoums = null, params string[] childTables)
        {
            return CreateUdo(udoName, description, tableName, DocumentType == "D" ? SAPbobsCOM.BoUDOObjType.boud_Document : SAPbobsCOM.BoUDOObjType.boud_MasterData, findFields, defaultform, FormColoums, childTables);
        }
        public static bool CreateUdo(string udoName, string description, string tableName, SAPbobsCOM.BoUDOObjType nObjectType, List<string> findFields, string defaultform = null, string[] FormColoums = null, params string[] childTables)
        {
            // this method is called 
            var oUserObjectMD = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD) as SAPbobsCOM.UserObjectsMD;
            try
            {
                if (!oUserObjectMD.GetByKey(udoName))
                {
                    oUserObjectMD.Code = udoName;
                    oUserObjectMD.Name = description;
                    oUserObjectMD.TableName = tableName;
                    oUserObjectMD.ObjectType = nObjectType;
                    oUserObjectMD.ExtensionName = string.Empty;
                    //    if (nObjectType != SAPbobsCOM.BoUDOObjType.boud_MasterData && defaultform != "Y")
                    //  {
                    oUserObjectMD.FindColumns.ColumnAlias = "DocEntry";
                    //}

                    oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tNO;
                    oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                    oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                    oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                    oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                    oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
                    oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO;
                    //  oUserObjectMD.LogTableName = string.Format("A{0}", tableName);



                    //this is passed default form as yes 
                    if (defaultform == "Y")
                    {
                        //will show you the code running.
                        oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                        oUserObjectMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                        oUserObjectMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tNO;
                        oUserObjectMD.MenuCaption = udoName;
                        oUserObjectMD.FatherMenuID = 43523;
                        oUserObjectMD.Position = 0;
                        oUserObjectMD.MenuUID = udoName;
                        if (FormColoums != null)
                        {
                            for (int i = 0; i <= FormColoums.Length - 1; i++)
                            {
                                if (FormColoums[i].Trim() == "DocEntry")
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
                    }

                    if (findFields != null)
                    {
                        for (int i = 0; i < findFields.Count; i++)
                        {
                            if (i + 1 != findFields.Count)
                                oUserObjectMD.FindColumns.Add();

                            oUserObjectMD.FindColumns.SetCurrentLine(i);
                            oUserObjectMD.FindColumns.ColumnAlias = findFields[i];
                        }
                    }

                    if (childTables != null)
                    {
                        for (int i = 0; i < childTables.Length; i++)
                        {
                            if (i + 1 != childTables.Length)
                                oUserObjectMD.ChildTables.Add();

                            oUserObjectMD.ChildTables.SetCurrentLine(i);
                            oUserObjectMD.ChildTables.TableName = childTables[i];
                        }
                    }
                    //if (DefaultValues != null)
                    //{
                    //    foreach (var item in DefaultValues)
                    //    {
                    //       oUserObjectMD. 

                    //    }
                    //}
                    if (oUserObjectMD.Add() != 0)
                    {
                        var err = Utility.GetErrorMessage();
                        returnValue = false;
                    }
                    else
                        returnValue = true;
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
            }
            finally
            {
                oUserObjectMD.ReleaseObject();
            }
            return returnValue;
        }
        #endregion

        #region Table Methods
        public static void DeleteTable(string tableName)
        {
            var oUserTablesMD = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables) as SAPbobsCOM.UserTablesMD;
            try
            {
                if (oUserTablesMD.GetByKey(tableName))
                {
                    if (oUserTablesMD.Remove() != 0)
                    {
                        var error = B1Helper.DiCompany.GetLastErrorDescription();
                    }
                }
            }
            catch (System.Exception ex)
            {
                //Utility.LogException(ex);
            }
            finally
            {
                oUserTablesMD.ReleaseObject();
            }
        }
        public static void AddTable(string tableName, string description, SAPbobsCOM.BoUTBTableType tableType)
        {

            var oUserTablesMD = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables) as SAPbobsCOM.UserTablesMD;
            try
            {
                if (!oUserTablesMD.GetByKey(tableName))
                {
                    oUserTablesMD.TableName = tableName;
                    oUserTablesMD.TableDescription = description;
                    oUserTablesMD.TableType = tableType;
                    if (oUserTablesMD.Add() != 0)
                    {
                        var error = B1Helper.DiCompany.GetLastErrorDescription();
                        throw new Exception(string.Format("Table {0} Could Not Be added - {1}", tableName, error));
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddTable", ex.ToString()), SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
            finally
            {
                oUserTablesMD.ReleaseObject();
            }
        }
        #endregion

        #region Field Methods
        /// <summary>
        /// Check if the field is already created in a table
        /// </summary>
        /// <param name="fieldName">Field name to be checked</param>
        /// <param name="tableName">table to checked the values in</param>
        /// <returns>bool: return the value if teh field is created or not</returns>
        public static bool IsFieldExists(string fieldName, string tableName)
        {
            var recordsSet = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            try
            {
                StringBuilder query = new StringBuilder("SELECT COUNT(\"AliasID\") AS Count ");
                query.Append(string.Format("FROM \"CUFD\" ", B1Helper.DiCompany.CompanyDB));
                query.Append("WHERE \"AliasID\" ='{0}' AND \"TableID\" = '{1}'");

                recordsSet.DoQuery(string.Format(query.ToString(), fieldName, tableName));
                recordsSet.MoveFirst();
                if (Convert.ToInt32(recordsSet.Fields.Item("Count").Value) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                Application.SBO_Application.SetStatusBarMessage(ex.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, true);
                return true;
            }
            finally
            {
                recordsSet.ReleaseObject();
            }
        }
        /// <summary>
        /// A method for adding new field to B1 table
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="description">Field description</param>
        /// <param name="tableName">Table the field will be added to</param>
        /// <param name="fieldType">Field Type</param>
        /// <param name="size">Field size in the database</param>     
        /// <param name="mandatory">bool: if the value is mandatory to be filled</param>
        public static void AddField(string name, string description, string tableName, SAPbobsCOM.BoFieldTypes fieldType, SAPbobsCOM.BoYesNoEnum mandatory, bool addedToUDT)
        {
            AddField(name, description, tableName, fieldType, null, mandatory, 0, addedToUDT);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldType"></param>
        /// <param name="size"></param>
        /// <param name="mandatory"></param>
        /// <param name="addedToUDT"></param>
        public static void AddField(string name, string description, string tableName, SAPbobsCOM.BoFieldTypes fieldType, int size, SAPbobsCOM.BoYesNoEnum mandatory, bool addedToUDT)
        {
            AddField(name, description, tableName, fieldType, size, mandatory, 0, addedToUDT);
        }
        /// <summary>
        /// A method for adding new field to B1 table
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="description">Field description</param>
        /// <param name="tableName">Table the field will be added to</param>
        /// <param name="fieldType">Field Type</param>
        /// <param name="size">Field size in the database</param>     
        /// <param name="mandatory">bool: if the value is mandatory to be filled</param>
        /// <param name="subType">Sub field type</param>
        public static void AddField(string name, string description, string tableName, SAPbobsCOM.BoFieldTypes fieldType, SAPbobsCOM.BoYesNoEnum mandatory, SAPbobsCOM.BoFldSubTypes subType, bool addedToUDT)
        {
            AddField(name, description, tableName, fieldType, null, mandatory, subType, addedToUDT);
        }
        /// <summary>
        /// A method for adding new field to B1 table
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="description">Field description</param>
        /// <param name="tableName">Table the field will be added to</param>
        /// <param name="fieldType">Field Type</param>
        /// <param name="size">Field size in the database</param>       
        /// <param name="mandatory">bool: if the value is mandatory to be filled</param>
        /// <param name="subType"></param>
        /// <param name="addedToUDT">If this field will be added to system table or User defined table</param>
        public static void AddField(string name, string description, string tableName, SAPbobsCOM.BoFieldTypes fieldType, Nullable<int> size, SAPbobsCOM.BoYesNoEnum mandatory, SAPbobsCOM.BoFldSubTypes subType, bool addedToUDT)
        {
            AddField(name, description, tableName, fieldType, size, mandatory, subType, addedToUDT, null);
        }
        /// <summary>
        /// A method for adding new field to B1 table
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="description">Field description</param>
        /// <param name="tableName">Table the field will be added to</param>
        /// <param name="fieldType">Field Type</param>
        /// <param name="size">Field size in the database</param>
        /// <param name="subType"></param>
        /// <param name="mandatory"></param>
        /// <param name="addedToUDT">If this field will be added to system table or User defined table</param>
        /// <param name="valiedValue">The default selected value</param>
        /// <param name="validValues">Add the values seperated by comma "," for value and description ex:(Value,Description)</param>
        public static void AddField(string name, string description, string tableName, SAPbobsCOM.BoFieldTypes fieldType, Nullable<int> size, SAPbobsCOM.BoYesNoEnum mandatory, SAPbobsCOM.BoFldSubTypes subType, bool addedToUDT, string validValue, string[,] LOV = null, string DefV = "", string linktoEntities = "", string linktoTable = "", params string[] validValues)
        {

            var objUserFieldMD = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields) as SAPbobsCOM.UserFieldsMD;
            try
            {
                if (addedToUDT)
                    tableName = string.Format("@{0}", tableName);

                if (!IsFieldExists(name, tableName))
                {
                    objUserFieldMD.TableName = tableName;
                    objUserFieldMD.Name = name;
                    objUserFieldMD.Description = description;
                    objUserFieldMD.Type = fieldType;
                    objUserFieldMD.Mandatory = mandatory;

                    if (size == null || size <= 0)
                        size = 50;

                    if (fieldType != SAPbobsCOM.BoFieldTypes.db_Numeric)
                        objUserFieldMD.Size = (int)size;
                    else
                        objUserFieldMD.EditSize = (int)size;

                    if (fieldType == BoFieldTypes.db_Float && subType == BoFldSubTypes.st_None)
                        objUserFieldMD.SubType = BoFldSubTypes.st_Quantity;
                    else
                        objUserFieldMD.SubType = subType;

                    if (!string.IsNullOrEmpty(validValue))
                        objUserFieldMD.DefaultValue = validValue;

                    if (validValues != null)
                    {
                        foreach (string s in validValues)
                        {
                            var valuesAttributes = s.Split(',');

                            if (valuesAttributes.Length == 2)
                                objUserFieldMD.ValidValues.Description = valuesAttributes[1];

                            objUserFieldMD.ValidValues.Value = valuesAttributes[0];
                            objUserFieldMD.ValidValues.Add();
                        }
                    }

                    if (linktoEntities != "")
                    {
                        if (linktoEntities == "1")
                            objUserFieldMD.LinkedSystemObject = UDFLinkedSystemObjectTypesEnum.ulChartOfAccounts;
                        //else if (linktoEntities == "247")
                        //    objUserFieldMD.LinkedSystemObject = 247;

                        //objUserFieldMD.LinkedTable = linktoEntities;

                    }

                    if (linktoTable != "")
                    {
                        objUserFieldMD.LinkedTable = linktoTable;
                    }

                    if (!string.IsNullOrEmpty(DefV))
                    {
                        objUserFieldMD.DefaultValue = DefV;
                    }

                    if (LOV == null)
                    {
                    }
                    else
                    {
                        for (int k = 0; k <= LOV.Length / 2 - 1; k++)
                        {
                            objUserFieldMD.ValidValues.Value = LOV[k, 0];
                            objUserFieldMD.ValidValues.Description = LOV[k, 1];
                            objUserFieldMD.ValidValues.Add();
                        }
                    }
                    if (objUserFieldMD.Add() != 0)
                    {
                        var error = Utility.GetErrorMessage();
                        Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}  {3}   {4}", "B1FormBase", "AddField", error, name, tableName), SAPbouiCOM.BoMessageTime.bmt_Short, true);
                        Utility.LogErrors(error);
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText("UDF Generated " + name, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddField", ex.ToString()), SAPbouiCOM.BoMessageTime.bmt_Short, true);
                //Utility.LogException(ex);
            }
            finally
            {
                objUserFieldMD.ReleaseObject();
            }
        }
        #endregion

        #region Company Connection
        public static SAPbobsCOM.Company DiCompany
        {
            get
            {
                if (diCompany == null)
                {
                    // //Utility.LogException("Attempting to the function");
                    if (Application.SBO_Application != null)
                        // //Utility.LogException("DI Property initializes");
                        diCompany = Application.SBO_Application.Company.GetDICompany() as SAPbobsCOM.Company;
                    //  //Utility.LogException("Successfully connected");
                    return diCompany;
                }
                else
                    return diCompany;
            }
        }
        public static bool IsHana
        {
            get
            {
                if (diCompany != null)
                {
                    if (Application.SBO_Application != null)
                    {
                        if (diCompany.DbServerType == BoDataServerTypes.dst_HANADB)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    diCompany = Application.SBO_Application.Company.GetDICompany() as SAPbobsCOM.Company;
                    if (diCompany.DbServerType == BoDataServerTypes.dst_HANADB)
                    {
                        return true;
                    }
                    return false;
                }

            }
        }
        public static SAPbobsCOM.Company ConnectCompany()
        {
            try
            {
                SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
                Trace.WriteLine(string.Format("[DR] Connecting to baseCompany starts..."));
                var appSetting = ConfigurationManager.AppSettings;

                oCompany.Server = appSetting[ConfigKeys.ServerName];
                oCompany.LicenseServer = appSetting[ConfigKeys.LicenseServer];
                string DbServerType = appSetting[ConfigKeys.DbServerType];
                SAPbobsCOM.BoDataServerTypes dbServerType;
                switch (DbServerType)
                {
                    case "2005": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005; break;
                    case "2008": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008; break;
                    case "2012": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012; break;
                    case "2014": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014; break;
                    default: dbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB; break;

                }
                oCompany.DbServerType = dbServerType;
                oCompany.CompanyDB = appSetting[ConfigKeys.CompanyDB];
                oCompany.UserName = appSetting[ConfigKeys.B1UserName];
                oCompany.Password = appSetting[ConfigKeys.B1Password];
                oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ExportImportMode;

                int checkConnected = oCompany.Connect();
                if (checkConnected != 0)
                {
                    string message = string.Format("Error: Could not connect to Company {0}", oCompany.CompanyDB);
                    Trace.WriteLine(message);
                    Utility.LogErrors(message);
                    // Log.LogError(LogLevel.Error, message);
                    return null;
                }
                else
                    return oCompany;
            }
            catch (System.Exception ex)
            {
                Utility.LogErrors(string.Concat("Error occured at ConnectCompany: ", ex.Message));
                //Log.LogError(LogLevel.Error, string.Concat("Error occured at ConnectCompany: ", ex.Message));
                return null;
            }
        }
        #endregion

        #region System Methods
        public static int GetNextCodeId(string tableName)
        {
            var codeIntValue = 0;
            var recordSet = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            string query = string.Empty;
            if (IsHana)
            {
                query = String.Format("SELECT IFNULL( max(CAST(\"Code\" as INTEGER )), 0) Code from \"{0}\"", tableName);
            }
            else
            {
                query = String.Format("SELECT ISNULL( max(CAST(Code as int )), 0) Code from [{0}]", tableName);
            }

            recordSet.DoQuery(query);
            //recordSet.DoQuery(String.Format("SELECT ISNULL( max(CAST(Code as int )), 0) Code from [{0}]", tableName));
            recordSet.MoveFirst();
            while (recordSet.EoF == false)
            {
                codeIntValue = Convert.ToInt32(recordSet.Fields.Item("Code").Value);
                codeIntValue += 1;
                recordSet.MoveNext();
            }
            return codeIntValue;
        }

        public static int GetNextDocNum(string tableName)
        {
            var codeIntValue = 0;
            var recordSet = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            string query = string.Empty;
            if (IsHana)
            {
                query = String.Format("SELECT IFNULL( max(CAST(\"DocNum\" as INTEGER )), 0) Code from \"{0}\"", tableName);
            }
            else
            {
                query = String.Format("SELECT ISNULL( max(CAST(DocNum as int )), 0) Code from [{0}]", tableName);
            }

            recordSet.DoQuery(query);
            //recordSet.DoQuery(String.Format("SELECT ISNULL( max(CAST(Code as int )), 0) Code from [{0}]", tableName));
            recordSet.MoveFirst();
            while (recordSet.EoF == false)
            {
                codeIntValue = Convert.ToInt32(recordSet.Fields.Item("Code").Value);
                codeIntValue += 1;
                recordSet.MoveNext();
            }
            return codeIntValue;
        }
        public static string[,] DoQuery(string query)
        {
            string[,] result;
            SAPbobsCOM.Recordset oRec = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            try
            {
                oRec.DoQuery(query);
                int row = oRec.RecordCount;
                int col = oRec.Fields.Count;
                result = new string[row + 1, col];
                for (int headerIndex = 0; headerIndex < col; headerIndex++)
                {
                    result[0, headerIndex] = oRec.Fields.Item(headerIndex).Name;
                }
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        result[r + 1, c] = oRec.Fields.Item(c).Value.ToString();
                    }
                    oRec.MoveNext();
                }
                return result;

            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return null;
            }
            finally
            {
                oRec.ReleaseObject();
            }
        }
        public static decimal GetItemAvgCost(string itemCode)
        {
            decimal avgPrice = 0;
            StringBuilder query = new StringBuilder("SELECT  ISNULL(AvgPrice, 0) AS AvgPrice ");
            query.Append("FROM OITM ");
            query.Append("WHERE ItemCode ='{0}'");

            var recordsSet = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            try
            {
                recordsSet.DoQuery(string.Format(query.ToString(), itemCode));
                recordsSet.MoveFirst();
                if (!recordsSet.EoF)
                {
                    avgPrice = Convert.ToDecimal(recordsSet.Fields.Item("AvgPrice").Value);
                    recordsSet.MoveNext();
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
            }
            finally
            {
                recordsSet.ReleaseObject();
            }
            return avgPrice;
        }
        public static SAPbobsCOM.Items GetItemByKey(string key)
        {
            var itm = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems) as SAPbobsCOM.Items;
            try
            {
                if (itm.GetByKey(key) == false)
                {
                    B1Helper.DiCompany.GetLastErrorDescription();
                    return null;
                }
                else
                    return itm;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return null;
            }
            finally
            {
                itm.ReleaseObject();
            }
        }
        public static string GetNextEntryIndex(string tableName)
        {
            var record = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            string query = "";
            try
            {
                if (IsHana)
                {
                    query = string.Format("SELECT MAX(\"DocEntry\") AS DocEntry FROM \"{0}\"", tableName);
                }
                else
                {
                    query = string.Format("SELECT MAX([DocEntry]) AS DocEntry FROM [dbo].[{0}]", tableName);
                }
                record.DoQuery(query);
                string lastIndex = string.Empty;
                while (record.EoF == false)
                {
                    lastIndex = record.Fields.Item("DocEntry").Value.ToString();
                    record.MoveNext();
                }
                return lastIndex;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return "0";
            }
            finally
            {
                record.ReleaseObject();
            }
        }
        public static int CreateTransferGoodsIssue(SAPbouiCOM.Application oApplication, string reference, string itemCode, double itemQuantity, string whsCode)
        {
            return CreateTransferGoodsIssue(oApplication, reference, itemCode, itemQuantity, whsCode, null);
        }
        public static int CreateTransferGoodsReceipt(SAPbouiCOM.Application oApplication, string reference, string itemCode, double itemQuantity, string whsCode, double totalCost)
        {
            return CreateTransferGoodsReceipt(oApplication, reference, itemCode, itemQuantity, whsCode, totalCost, null);
        }
        public static int CreateTransferGoodsIssue(SAPbouiCOM.Application oApplication, string reference, string itemCode, double itemQuantity, string whsCode, System.Data.DataTable batchesTable)
        {
            var goodsIssue = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit) as SAPbobsCOM.Documents;
            try
            {
                goodsIssue.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                goodsIssue.Lines.ItemCode = itemCode;
                goodsIssue.Lines.Quantity = itemQuantity;
                goodsIssue.Lines.WarehouseCode = whsCode;
                goodsIssue.Reference2 = reference;
                if (batchesTable != null)
                {
                    for (int i = 0; i < batchesTable.Rows.Count; i++)
                    {
                        goodsIssue.Lines.BatchNumbers.Quantity = Convert.ToDouble(batchesTable.Rows[i]["Selected Qty"]);
                        goodsIssue.Lines.BatchNumbers.BatchNumber = batchesTable.Rows[i]["Batch"].ToString();
                        goodsIssue.Lines.BatchNumbers.SetCurrentLine(i);
                        goodsIssue.Lines.BatchNumbers.Add();
                    }
                }
                if (goodsIssue.Add() != 0)
                {
                    Utility.GetErrorMessage();
                    return 0;
                }
                else
                    return goodsIssue.DocEntry;
            }
            catch
            {
                return 0;
            }
            finally
            {
                goodsIssue.ReleaseObject();
            }
        }
        public static int CreateTransferGoodsReceipt(SAPbouiCOM.Application oApplication, string reference, string itemCode, double itemQuantity, string whsCode, double totalCost, System.Data.DataTable batchesTable)
        {
            var goodsReceipt = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry) as SAPbobsCOM.Documents;
            try
            {
                goodsReceipt.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                goodsReceipt.Lines.ItemCode = itemCode;
                goodsReceipt.Lines.Quantity = itemQuantity;
                goodsReceipt.Lines.WarehouseCode = whsCode;
                goodsReceipt.Lines.UnitPrice = totalCost;
                goodsReceipt.Reference2 = reference;
                if (batchesTable != null)
                {
                    for (int i = 0; i < batchesTable.Rows.Count; i++)
                    {
                        goodsReceipt.Lines.BatchNumbers.Quantity = Convert.ToDouble(batchesTable.Rows[i]["Selected Qty"]);
                        goodsReceipt.Lines.BatchNumbers.BatchNumber = batchesTable.Rows[i]["Batch"].ToString();
                        goodsReceipt.Lines.BatchNumbers.SetCurrentLine(i);
                        goodsReceipt.Lines.BatchNumbers.Add();
                    }
                }
                if (goodsReceipt.Add() != 0)
                {
                    Utility.GetErrorMessage();
                    return 0;
                }
                else
                    return goodsReceipt.DocEntry;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return 0;
            }
            finally
            {
                goodsReceipt.ReleaseObject();
            }
        }

        public static void AddMatrixNewLine()
        {
            try
            {
                var form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm("60150", Application.SBO_Application.Forms.ActiveForm.TypeCount);
                var oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Meter").Specific;
                oMatrix.AddRow();
                oMatrix.SetCellValue("V_LineId", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
            }
            catch (Exception ex)
            {
                //Utility.LogException("Error at B1Helper.AddMatrixNewLine Method: " + ex.Message);
            }
        }

        public static void DeleteMatrixLine(string sFormuid)
        {
            try
            {
                //Service Contract
                string sLine = string.Empty;
                string sCode = string.Empty;
                string squery = string.Empty;
                SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.Form form = null;

                switch (sFormuid)
                {
                    case "60126":
                        form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(60126, Application.SBO_Application.Forms.ActiveForm.TypeCount);
                        //Service Call
                        //var formSC = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm("60110", Application.SBO_Application.Forms.ActiveForm.TypeCount);

                        var oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Meter").Specific;
                        var oMatrixPrice = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Price").Specific;

                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        sLine = oMatrix.GetCellValue("V_Line", i).ToString();
                                        sCode = oMatrix.GetCellValue("V_Code", i).ToString();
                                        squery = "delete from \"@Z_SRM1\"  WHERE \"Code\" = '" + sCode + "' and  \"LineId\"  = '" + sLine + "'";
                                        oRS.DoQuery(squery);
                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                        }

                        if (oMatrixPrice.RowCount > 0)
                        {
                            for (int i = oMatrixPrice.RowCount; i >= 1; i--)
                            {
                                if (oMatrixPrice.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        sLine = oMatrix.GetCellValue("V_Line", i).ToString();
                                        sCode = oMatrix.GetCellValue("V_Code", i).ToString();
                                        squery = "delete from \"@Z_SRP1\"  WHERE \"Code\" = '" + sCode + "' and  \"LineId\"  = '" + sLine + "'";
                                        oRS.DoQuery(squery);

                                        oMatrixPrice.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                        }


                        break;
                    case "60110":
                        form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(60110, Application.SBO_Application.Forms.ActiveForm.TypeCount);
                        //Service Call
                        //var formSC = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm("60110", Application.SBO_Application.Forms.ActiveForm.TypeCount);

                        oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Meter").Specific;

                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        sLine = oMatrix.GetCellValue("V_Line", i).ToString();
                                        sCode = oMatrix.GetCellValue("V_Code", i).ToString();
                                        squery = "delete from \"@Z_SCM1\"  WHERE \"Code\" = '" + sCode + "' and  \"LineId\"  = '" + sLine + "'";
                                        oRS.DoQuery(squery);

                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                        }

                        break;

                    case "60150":
                        form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(60150, Application.SBO_Application.Forms.ActiveForm.TypeCount);
                        //Service Call
                        //var formSC = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm("60110", Application.SBO_Application.Forms.ActiveForm.TypeCount);

                        oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("mtx_Meter").Specific;

                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        sLine = oMatrix.GetCellValue("V_Line", i).ToString();
                                        sCode = oMatrix.GetCellValue("V_Code", i).ToString();
                                        squery = "delete from \"@Z_ECM1\"  WHERE \"Code\" = '" + sCode + "' and  \"LineId\"  = '" + sLine + "'";
                                        oRS.DoQuery(squery);
                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                        }

                        break;

                    case "model":
                        form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.ActiveForm;
                        //Service Call
                        //var formSC = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm("60110", Application.SBO_Application.Forms.ActiveForm.TypeCount);

                        oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("Item_42").Specific;

                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                            for (int iRow = 1; iRow <= oMatrix.RowCount; iRow++)
                            {
                                if (!string.IsNullOrEmpty(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(iRow).Specific).Value))
                                {
                                    oMatrix.CommonSetting.SetCellEditable(iRow, 1, false);
                                    oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                }
                                else { oMatrix.CommonSetting.SetCellEditable(iRow, 1, true); }
                            }
                            if (oMatrix.RowCount == 0)
                            {
                                oMatrix.AddRow();
                                oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                oMatrix.CommonSetting.SetCellEditable(1, 1, true);
                            }
                        }

                        oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("Item_43").Specific;
                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                            for (int iRow = 1; iRow <= oMatrix.RowCount; iRow++)
                            {
                                if (!string.IsNullOrEmpty(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(iRow).Specific).Value))
                                {
                                    oMatrix.CommonSetting.SetCellEditable(iRow, 1, false);
                                    oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                }
                                else { oMatrix.CommonSetting.SetCellEditable(iRow, 1, true); }
                            }
                            if (oMatrix.RowCount == 0)
                            {
                                oMatrix.AddRow();
                                oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                oMatrix.CommonSetting.SetCellEditable(1, 1, true);
                            }
                        }

                        oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("Item_44").Specific;
                        if (oMatrix.RowCount > 0)
                        {
                            for (int i = oMatrix.RowCount; i >= 1; i--)
                            {

                                if (oMatrix.IsRowSelected(i) == true)
                                {
                                    int result = (int)Application.SBO_Application.MessageBox("Do You Want to Delete This Row", 1, "OK", "Cancel");
                                    if (result == 1)
                                    {
                                        oMatrix.DeleteRow(i);
                                        if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                            form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    }
                                }
                            }
                            for (int iRow = 1; iRow <= oMatrix.RowCount; iRow++)
                            {
                                if (!string.IsNullOrEmpty(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(iRow).Specific).Value))
                                {
                                    oMatrix.CommonSetting.SetCellEditable(iRow, 1, false);
                                    oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                }
                                else { oMatrix.CommonSetting.SetCellEditable(iRow, 1, true); }
                            }
                            if (oMatrix.RowCount == 0)
                            {
                                oMatrix.AddRow();
                                oMatrix.SetCellValue("#", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                                oMatrix.CommonSetting.SetCellEditable(1, 1, true);
                            }
                        }

                        break;
                }


                //  form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                //oMatrix.SetCellValue("V_LineId", oMatrix.VisualRowCount, oMatrix.VisualRowCount.ToString());
                //oMatrix.DeleteRow();
            }
            catch (Exception ex)
            {
                //Utility.LogException("Error at B1Helper.DeleteMatrixLine Method: " + ex.Message);
            }
        }
        public static int UpdateContractPortifolio(string contractGUID, string portifoliName)
        {
            //Get Contract No
            int contractID = 0;
            string cquery = "Select Number From [OOAT] where U_ELSContractGUID = '" + contractGUID + "'";
            SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rs.DoQuery(cquery);
            if (rs.RecordCount > 0)
            {
                contractID = int.Parse(rs.Fields.Item(0).Value.ToString());
                string query = "Update OOAT set U_Portfolio='" + portifoliName + "' From OOAT where U_ELSContractGUID = '" + contractGUID + "'";
                rs.DoQuery(query);
            }
            return contractID;
        }

        // private static void 

        //public static bool UpdateServiceCallNoBasedonDocType(string ServiceCallNo, int DocNum, string DocType, string ContractID)
        //{
        //    SAPbobsCOM.Documents objDoc = null;
        //    switch (DocType)
        //    {

        //        case "SQ":
        //            //Sale Quotation
        //            objDoc = (SAPbobsCOM.Documents)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.oQuotations);
        //            if (objDoc.GetByKey(DocNum))
        //            {
        //                objDoc.
        //               //Check the Items - itemGroup in the Contract
        //            }
        //            break;
        //        case "SO":
        //            //Sale Order
        //            break;
        //        case "AI":
        //            //A/R Invoice
        //            break;
        //        case "DO":
        //            //Delivery
        //            break;
        //        case "CR":
        //            //Credit Memo
        //            break;
        //        case "RE":
        //            //Returns
        //            break;
        //        default:
        //            break;
        //    }
        //    //Sales Quotation

        //    //Sales Order
        //    return false;
        //}


        public static bool CheckItemGroupofItemUnderCoverage(string ContractID, string itemCode)
        {
            try
            {
                SAPbobsCOM.Items oItem = (SAPbobsCOM.Items)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.oItems);
                if (oItem.GetByKey(itemCode))
                {
                    int itemGrouCode = oItem.ItemsGroupCode;
                    List<int> lstItemsgrps = GetAllCovergeItemGroup(ContractID);
                    if (lstItemsgrps.Contains(itemGrouCode))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
            }
            return false;
        }

        public static List<int> GetAllCovergeItemGroup(string ContractID)
        {
            List<int> lstItemGroupCodes = new List<int>(); ;
            try
            {
                string query = "SELECT T2.\"U_ItemGroupCode\" FROM OCTR T0 , \"@Z_OCCV\"  T1, \"@Z_CCV1\"  T2 WHERE " +
                                "T0.\"ContractID\" = T1.\"U_ContractNo\" and  T2.\"U_Active\" ='Y' and  T1.\"Code\" = T2.\"Code\" " +
                                " and T0.\"ContractID\"='" + ContractID + "' GROUP BY T2.\"U_ItemGroupCode\"";
                SAPbobsCOM.Recordset rsObj = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                rsObj.DoQuery(query);
                if (rsObj.RecordCount > 0)
                {
                    while (rsObj.EoF == false)
                    {
                        lstItemGroupCodes.Add(int.Parse(rsObj.Fields.Item(0).Value.ToString()));
                        rsObj.MoveNext();
                    }
                    return lstItemGroupCodes;
                }
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
            }
            return lstItemGroupCodes;
        }
        #endregion

        #region UI Methods
        public static void ChangeToAddMode(SAPbouiCOM.IForm form)
        {
            if (form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
            {
                form.Select();
                Application.SBO_Application.ActivateMenuItem("1282");
            }
        }
        public static string IsFormTypeCustom(SAPbouiCOM.IForm control)
        {
            var UDFForm = Application.SBO_Application.Forms.Item(control.UDFFormUID);
            var isCustomForm = UDFForm.Items.Item("U_IsCustomForm").Specific as SAPbouiCOM.ComboBox;
            return isCustomForm.Selected.Value;
        }
        public static void ChangeTypeToCustom(SAPbouiCOM.IForm control)
        {
            control.Title = "Oil " + control.Title;
            var UDFForm = Application.SBO_Application.Forms.Item(control.UDFFormUID);
            var isCustomForm = UDFForm.Items.Item("U_IsCustomForm").Specific as SAPbouiCOM.ComboBox;
            isCustomForm.Select("Y");
        }

        public static void FillComboValues(SAPbouiCOM.ComboBox control, string query)
        {
            var dr = ExecuteQuery(query, false, null);
            DataTable dtValues = new DataTable();
            dtValues.Load(dr);
            var drows = dtValues.AsEnumerable().ToList();

            foreach (DataRow drow in drows)
            {
                control.ValidValues.Add(drow.Field<int>(0).ToString(), drow.Field<string>(1).ToString());
            }

        }

        public static void ClearCombo(SAPbouiCOM.ComboBox cmbCombo)
        {
            try
            {
                if (cmbCombo != null)
                {
                    int CmbC = cmbCombo.ValidValues.Count;
                    for (int i = 1; i <= CmbC; i++)
                    {
                        cmbCombo.ValidValues.Remove(CmbC - i - 0, SAPbouiCOM.BoSearchKey.psk_Index);
                    }
                }
                cmbCombo.Select(0, SAPbouiCOM.BoSearchKey.psk_ByDescription);
            }
            catch
            {

            }
        }

        public static void SAPFillComboValues(SAPbouiCOM.ComboBox control, string query)
        {

            try
            {
                SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                rs.DoQuery(query);
                if (rs.RecordCount > 0)
                {
                    rs.MoveFirst();

                    while (rs.EoF == false)
                    {
                        control.ValidValues.Add(rs.Fields.Item(0).Value.ToString(), rs.Fields.Item(1).Value.ToString());

                        rs.MoveNext();
                    }

                }

            }
            catch
            {

            }


        }

        public static void SAPFillComboSelect(SAPbouiCOM.ComboBox control, string query)
        {

            try
            {
                SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                rs.DoQuery(query);
                if (rs.RecordCount > 0)
                {
                    control.Select(rs.Fields.Item(0).Value.ToString());


                }
            }
            catch
            {

            }
        }


        public static void SAPFillMatrixComboValues(SAPbouiCOM.Column control, string query)
        {
            SAPbobsCOM.Recordset rs = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rs.DoQuery(query);
            if (rs.RecordCount > 0)
            {
                rs.MoveFirst();

                while (rs.EoF == false)
                {
                    control.ValidValues.Add(rs.Fields.Item(0).Value.ToString(), rs.Fields.Item(1).Value.ToString());

                    rs.MoveNext();
                }

            }


        }

        public static SqlDataReader ExecuteQuery(string query, bool isSP, params IDataParameter[] sqlParams)
        {
            SqlConnection con = B1Helper.ConnectToSQL();
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = isSP ? CommandType.StoredProcedure : CommandType.Text;
            if (isSP)
            {
                if (sqlParams != null)
                {
                    foreach (IDataParameter para in sqlParams)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
            }
            var dr = cmd.ExecuteReader();

            return dr;
        }
        #endregion

        #region Generic Methods
        public static bool AddBusinessObject<T>(T businessObject)
        {
            if (((dynamic)businessObject).Add() != 0)
            {
                var err = DiCompany.GetLastErrorDescription();
                return false;
            }
            else
                return true;
        }
        public static T CreateBusinessObjectInstance<T>(SAPbobsCOM.BoObjectTypes objectType)
        {
            T objectInstance = (T)DiCompany.GetBusinessObject(objectType);
            return objectInstance;
        }
        public static T GetBusinessObject<T>(SAPbobsCOM.BoObjectTypes objectType, object key)
        {
            dynamic objectInstance = (T)DiCompany.GetBusinessObject(objectType);
            try
            {
                if (key.GetType() == typeof(string))
                    objectInstance.GetByKey(key.ToString());
                else
                    objectInstance.GetByKey(Convert.ToInt32(key));
                return (T)objectInstance;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return default(T);
            }
        }
        public static T GetBusinessObject<T>(SAPbobsCOM.BoObjectTypes objectType, string query)
        {
            dynamic objectInstance = (T)DiCompany.GetBusinessObject(objectType);
            try
            {
                if (SAPbobsCOM.BoObjectTypes.BoRecordset == objectType)
                {
                    objectInstance.DoQuery(query);
                }
                else
                {
                    SAPbobsCOM.Recordset records = DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset) as Recordset;
                    records.DoQuery(query);
                    objectInstance.Browser.Recordset = records;
                }

                return (T)objectInstance;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                return default(T);
            }
        }

        public static string UTform(string n) // function to load Udt forms
        {
            SAPbouiCOM.MenuItem oMenu;
            int i;
            oMenu = Application.SBO_Application.Menus.Item("51200");
            for (i = 0; i <= oMenu.SubMenus.Count - 1; i++)
            {
                if (oMenu.SubMenus.Item(i).String == n)
                {
                    return oMenu.SubMenus.Item(i).UID;
                    break;
                }
            }
            return "";
        }



        #endregion

        #region SQLConnection
        public static SqlConnection ConnectToSQL()
        {
            string connString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            return con;
        }
        #endregion

    }
}
