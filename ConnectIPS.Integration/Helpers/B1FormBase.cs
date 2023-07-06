using MainLibrary.SAPB1;
using MainLibrary.Utilities;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Helpers
{
    public abstract class B1FormBase : UserFormBase
    {
        #region Members
        private bool _IsMatrix;
        private string _MatrixID;
        private int _RowSelected;
        protected int RowToDeleteIndex;
        private SAPbouiCOM.Matrix oMatrix;
        private Dictionary<Tuple<string, string>, string> ColIdNCFLColumn = new Dictionary<Tuple<string, string>, string>();
        private Dictionary<Tuple<string, string>, List<SAPColumn>> AutoFillMatrixColumns = new Dictionary<Tuple<string, string>, List<SAPColumn>>();
        List<SAPbouiCOM.BoFormMode> ModeToAddMatrixLine = new List<SAPbouiCOM.BoFormMode>();
        #endregion

        #region Methods
        protected void AddLineMenu()
        {
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;

            try
            {
                //  CREATE MENU POPUP MYUSERMENU01 AND ADD IT TO TOOLS MENU
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

                oMenuItem = Application.SBO_Application.Menus.Item(MenuUid.DATA); // Data'

                if (!oMenuItem.SubMenus.Exists("AddLine"))
                {
                    oMenus = oMenuItem.SubMenus;
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "AddLine";
                    oCreationPackage.String = "Add Line";
                    oCreationPackage.Enabled = true;
                    oCreationPackage.Position = 1;
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch (Exception ex)
            {
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddLineMenu", ex.ToString()));
            }
        }
        protected void AddDeleteMenu()
        {
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;

            try
            {
                //  CREATE MENU POPUP MYUSERMENU01 AND ADD IT TO TOOLS MENU
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

                oMenuItem = Application.SBO_Application.Menus.Item("1280"); // Data'

                if (!oMenuItem.SubMenus.Exists("DeleteLine"))
                {
                    oMenus = oMenuItem.SubMenus;
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "DeleteLine";
                    oCreationPackage.String = "Delete Line";
                    oCreationPackage.Enabled = true;
                    oCreationPackage.Position = 0;
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch (Exception ex)
            {
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "AddDeleteMenu", ex.ToString()));
            }
        }
        protected void RemoveLineMenu()
        {
            try
            {
                Application.SBO_Application.Menus.RemoveEx("AddLine");
            }
            catch (Exception ex)
            {
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "RemoveLineMenu", ex.ToString()));
            }
        }
        protected void RemoveDeleteMenu()
        {
            try
            {
                Application.SBO_Application.Menus.RemoveEx("DeleteLine");
            }
            catch (Exception ex)
            {
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "B1FormBase", "RemoveDeleteMenu", ex.ToString()));
            }
        }
        protected void AddMatrixHandlers()
        {
            AddMatrixHandlers(true, SAPbouiCOM.BoFormMode.fm_ADD_MODE, SAPbouiCOM.BoFormMode.fm_UPDATE_MODE);
        }
        protected static bool IsCellValueZero(string cellValue)
        {
            double n;
            bool isCellNumeric = double.TryParse(cellValue, out n);
            if (isCellNumeric && Double.Parse(cellValue) == 0)
                return true;
            else
                return false;
        }
        protected void AddCFLAutoFill(SAPbouiCOM.Matrix control)
        {
            AddCFLAutoFill(control, null);
        }
        protected void AddNewMatrixLine(SAPbouiCOM.Matrix control)
        {
            control.ValidateAfter += control_ValidateAfter;
        }
        private static bool LastRowEmpty(SAPbouiCOM.Matrix oMatrix)
        {
            int emptyColumns = 0;
            for (int i = 1; i < oMatrix.Columns.Count; i++)
            {
                string cellValue = oMatrix.GetCellValue(oMatrix.Columns.Item(i).UniqueID.ToString(), oMatrix.RowCount).ToString();
                if (String.IsNullOrEmpty(cellValue) || IsCellValueZero(cellValue))
                    emptyColumns++;
            }
            if ((oMatrix.Columns.Count - 1) == emptyColumns)
                return true;
            else
                return false;
        }
        private bool IsCFLColumnAutoManaged(string matrixID, string colID)
        {
            Tuple<string, string> id = new Tuple<string, string>(matrixID, colID);
            return ColIdNCFLColumn == null || ColIdNCFLColumn.ContainsKey(id);
        }
        private int getMatrixEditableColumnsCount(SAPbouiCOM.Matrix omatrix)
        {
            int sum = 0;
            for (int i = 1; i < omatrix.Columns.Count; i++)
            {
                if (omatrix.Columns.Item(i).Editable == true)
                    sum++;
            }
            return sum;
        }
        private void AddNRemoveLine<T>(T control, SAPbouiCOM.MenuEvent ppVal)
        {
            switch (ppVal.MenuUID)
            {
                case "AddLine":
                    control.GetType().InvokeMember("AddLine", System.Reflection.BindingFlags.GetProperty, null, null, null);
                    break;
                case "DeleteLine":
                    control.GetType().InvokeMember("DeleteLine", System.Reflection.BindingFlags.GetProperty, null, null, null);
                    break;
            }
        }
        private bool IsCFLColumnMultiAutoManaged(string matrixID, string colID)
        {
            Tuple<string, string> id = new Tuple<string, string>(matrixID, colID);
            return AutoFillMatrixColumns.Count > 0 || AutoFillMatrixColumns.ContainsKey(id);
        }
        public bool AreAllColumnsFilled(SAPbouiCOM.Matrix oMatrix, int selectedRow)
        {
            int EnabledColumnCount = getMatrixEditableColumnsCount(oMatrix);
            for (int i = 1; i <= EnabledColumnCount; i++)
            {
                if (oMatrix.Columns.Item(i).Editable == true)
                {
                    string cellValue = oMatrix.GetCellValue(oMatrix.Columns.Item(i).UniqueID.ToString(), selectedRow).ToString();

                    if (String.IsNullOrEmpty(cellValue))
                        return false;
                }
            }
            return true;
        }
        private string GetSelectedValue(object colID, SAPbouiCOM.DataTable selected)
        {
            string selectedValue = selected.Columns.Item(colID).Cells.Item(0).Value.ToString();
            return selectedValue;
        }
        protected void Matrix_RightClickAfter(ref SAPbouiCOM.ContextMenuInfo eventInfo)
        {
            try
            {

                if (!string.IsNullOrEmpty(eventInfo.ColUID))
                {
                    RemoveDeleteMenu();
                    RemoveLineMenu();
                }
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "TransferItems", "TransferItems_RightClickAfter", ex.ToString()));

            }
        }
        private List<SAPColumn> getColumnsMultiAutoManaged(string matrixID, string colID)
        {
            Tuple<string, string> id = new Tuple<string, string>(matrixID, colID);
            return AutoFillMatrixColumns[id];
        }
        protected SAPbouiCOM.DataTable GetCFLSelectedItem(SAPbouiCOM.SBOItemEventArg pVal)
        {
            return (pVal as SAPbouiCOM.ISBOChooseFromListEventArg).SelectedObjects;
        }
        private void SetMatrixValue(string selectedValue, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var controlMtx = this.UIAPIRawForm.Items.Item(pVal.ItemUID).Specific as SAPbouiCOM.Matrix;
            controlMtx.SetCellWithoutValidation(pVal.Row, pVal.ColUID, selectedValue);
        }
        protected void AddMatrixHandlers(bool isMatrix, params SAPbouiCOM.BoFormMode[] Modes)
        {
            _IsMatrix = isMatrix;
            foreach (SAPbouiCOM.BoFormMode mode in Modes)
            {
                ModeToAddMatrixLine.Add(mode);
            }
            this.RightClickBefore += Matrix_RightClickBefore;
            this.RightClickAfter += Matrix_RightClickAfter;
            Application.SBO_Application.MenuEvent += AddRemoveMtrxLines;
        }
        private void control_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var controlMtx = this.UIAPIRawForm.Items.Item(pVal.ItemUID).Specific as SAPbouiCOM.Matrix;
            if (pVal.Row == controlMtx.RowCount)
                if (AreAllColumnsFilled(controlMtx, pVal.Row))
                    controlMtx.AddLine();
        }
        protected void AddRemoveMtrxLines(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (Application.SBO_Application.Forms.ActiveForm.UniqueID == this.UIAPIRawForm.UniqueID)
            {
                try
                {
                    if (!pVal.BeforeAction && !string.IsNullOrEmpty(_MatrixID) && (ModeToAddMatrixLine.Contains(UIAPIRawForm.Mode)))
                    {
                        SAPbouiCOM.Matrix control = this.GetItem(_MatrixID).Specific as SAPbouiCOM.Matrix;

                        if (this.UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                            this.UIAPIRawForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;

                        if (control == null)
                        {
                            var controlGrid = this.GetItem(_MatrixID).Specific as SAPbouiCOM.Grid;
                            AddNRemoveLineGrid(controlGrid, pVal);
                        }
                        else
                        {
                            AddNRemoveLineMatrix(control, pVal);
                        }
                        _MatrixID = null;
                    }
                }
                catch (Exception ex)
                {
                    Utility.LogException(ex);
                    //Log.LogException(LogLevel.Error, ex);
                }
            }
        }
        private void AddNRemoveLineGrid(SAPbouiCOM.Grid controlGrid, SAPbouiCOM.MenuEvent pVal)
        {
            switch (pVal.MenuUID)
            {
                case "AddLine":
                    controlGrid.AddLine();
                    break;
                case "DeleteLine":
                    controlGrid.DeleteRow(_RowSelected - 1);
                    break;
            }
        }
        private void AddNRemoveLineMatrix(SAPbouiCOM.Matrix control, SAPbouiCOM.MenuEvent pVal)
        {
            switch (pVal.MenuUID)
            {
                case "AddLine":
                    control.AddLine();
                    //.AddLine();
                    break;
                case "DeleteLine":
                    control.DeleteRow(_RowSelected);
                    break;
            }
        }
        protected void AddCFLAutoFill(SAPbouiCOM.Matrix control, params string[] colIDandCFLAlias)
        {
            if (colIDandCFLAlias != null)
            {
                foreach (string s in colIDandCFLAlias)
                {
                    var values = s.Split(',');
                    if (values.Length == 2)
                    {
                        ColIdNCFLColumn.Add(new Tuple<string, string>(control.Item.UniqueID, values[0]), values[1]);
                    }
                }
            }
            control.ChooseFromListAfter += control_ChooseFromListAfter;
        }
        protected void AutoManageMatrixLines(SAPbouiCOM.Matrix control, SAPbouiCOM.Button addButton)
        {
            AddNewMatrixLine(control);
            DeleteLastRowInMatrixIfEmpty(addButton, control);
        }
        protected void control_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var ppVal = pVal as SAPbouiCOM.SBOChooseFromListEventArg;
            var selected = ppVal.SelectedObjects;

            if (selected != null)
            {
                if (IsCFLColumnAutoManaged(pVal.ItemUID, pVal.ColUID))
                {
                    object columnId = ColIdNCFLColumn == null ? (object)0 : (object)ColIdNCFLColumn[new Tuple<string, string>(pVal.ItemUID, pVal.ColUID)];
                    string SelectedValue = GetSelectedValue(columnId, selected);
                    SetMatrixValue(SelectedValue, pVal);
                }
            }
        }
        protected void DeleteLastRowInMatrixIfEmpty(SAPbouiCOM.Button addButton, SAPbouiCOM.Matrix omatrix)
        {
            oMatrix = omatrix;
            addButton.PressedBefore += addButton_PressedBefore;
        }
        protected void controlMultiFill_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var ppVal = pVal as SAPbouiCOM.SBOChooseFromListEventArg;
            var selected = ppVal.SelectedObjects;

            if (selected != null)
            {
                if (IsCFLColumnMultiAutoManaged(pVal.ItemUID, pVal.ColUID))
                {
                    List<SAPColumn> fillColumns = getColumnsMultiAutoManaged(pVal.ItemUID, pVal.ColUID);
                    foreach (SAPColumn column in fillColumns)
                    {
                        string SelectedValue = GetSelectedValue(column.CFLAlias, selected);

                        var controlMtx = this.UIAPIRawForm.Items.Item(pVal.ItemUID).Specific as SAPbouiCOM.Matrix;
                        controlMtx.SetCellWithoutValidation(pVal.Row, column.colID, SelectedValue);
                    }
                }
            }
        }
        protected void Matrix_RightClickBefore(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (!string.IsNullOrEmpty(eventInfo.ColUID))
                {
                    AddDeleteMenu();
                    AddLineMenu();
                    _MatrixID = eventInfo.ItemUID;
                    _RowSelected = eventInfo.Row.Equals(0) ? 1 : eventInfo.Row;
                }
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                Utility.LogErrors(string.Format("Error Occured At Class {0}, Method {1}: {2}", "PriceStructure", "Matrix_RightClickBefore", ex.ToString()));

            }
        }
        private void addButton_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (this.UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                if (oMatrix != null && oMatrix.RowCount > 0)
                    if (LastRowEmpty(oMatrix))
                        oMatrix.DeleteRow(oMatrix.RowCount);

            }
        }
        protected void AddCFLAutoMultiFill(SAPbouiCOM.Matrix control, SAPColumn cflColumn, List<SAPColumn> extracolumnstoFill)
        {
            Tuple<string, string> MatrixNColumnID = new Tuple<string, string>(control.Item.UniqueID, cflColumn.colID);
            List<SAPColumn> columnsToFill = new List<SAPColumn>() { cflColumn };
            foreach (SAPColumn column in extracolumnstoFill)
            {
                columnsToFill.Add(column);
            }

            AutoFillMatrixColumns.Add(MatrixNColumnID, columnsToFill);

            control.ChooseFromListAfter += controlMultiFill_ChooseFromListAfter;
        }


        #endregion

        public B1FormBase()
        {
            // TODO: Complete member initialization
        }
    }
}
