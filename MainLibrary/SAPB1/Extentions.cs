using MainLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainLibrary.SAPB1
{
    public static class Extentions
    {
        #region FORM METHODS
        public static void HideFields(this SAPbouiCOM.IForm frmControl, bool isVisible, params string[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                frmControl.Items.Item(ids[i]).Visible = isVisible;
            }
        }
        #endregion

        #region MATRIX METHODS
        public static void AddLine(this SAPbouiCOM.Matrix mtxControl)
        {
            try
            {
                SAPbouiCOM.EditText currentControl;
                if (mtxControl.RowCount <= 0)
                {
                    mtxControl.AddRow();
                    mtxControl.ClearRowData(1);
                }
                else
                {
                    mtxControl.AddRow(1, mtxControl.RowCount + 1);
                    mtxControl.ClearRowData(mtxControl.RowCount);
                }
                var hash = mtxControl.Columns.Item(0).Cells.Item(mtxControl.RowCount).Specific as SAPbouiCOM.EditText;
                hash.Value = "";
                currentControl = mtxControl.GetCellSpecific(1, mtxControl.RowCount) as SAPbouiCOM.EditText;
                //currentControl.Item.Click();
                //currentControl.Active = true;
                mtxControl.FlushToDataSource();
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                //Log.LogException(LogLevel.Error, ex);
            }
        }
        public static void SetLineId(this SAPbouiCOM.Matrix mtxControl)
        {
            try
            {
                for (int j = 1; j <= mtxControl.RowCount; j++)
                {
                    ((SAPbouiCOM.EditText)mtxControl.Columns.Item("#").Cells.Item(j).Specific).Value = j.ToString();
                }
            }
            catch
            {
               // ExceptionLog.SendErrorToText(ex, "ParameterMapping");
            }
        }
        public static void AddRowIndex(this SAPbouiCOM.Matrix mtxControl)
        {
            //var rec = B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            //rec.DoQuery("SELECT MAX([DocEntry]) AS DocEntry FROM [dbo].[@T_ITMTRNSFRSTUP]");
            //string lastIndex = string.Empty;
            //while (rec.EoF == false)
            //{
            //    lastIndex = rec.Fields.Item("DocEntry").Value.ToString();
            //    rec.MoveNext();
            //}

            var lastIndex = ((dynamic)mtxControl.Columns.Item("#").Cells.Item(mtxControl.RowCount).Specific).Value;
            if (lastIndex == string.Empty)
                lastIndex = "0";
            ((dynamic)mtxControl.Columns.Item("#").Cells.Item(mtxControl.RowCount).Specific).Value = int.Parse(lastIndex) + 1;
        }
        public static void GetValuedRows(this SAPbouiCOM.Matrix mtxControl)
        {
            for (int i = 1; i <= mtxControl.RowCount; i++)
            {
                int c = mtxControl.Columns.Count - 1;
                bool isEmpty = true;
                while (c >= 0)
                {
                    var cellValue = ((dynamic)mtxControl.Columns.Item(c).Cells.Item(i).Specific).Value;
                    if (cellValue != string.Empty && mtxControl.Columns.Item(c).UniqueID != Constants.Hash && cellValue != "0.0")
                    {
                        isEmpty = false;
                        break;
                    }
                    c--;
                }
                if (isEmpty)
                {
                    mtxControl.DeleteRow(i);
                }
            }
        }
        public static bool RemoveEmptyLines(this SAPbouiCOM.Matrix control)
        {
            bool BubbleEvent = true;
            int rowCount = control.RowCount;
            try
            {
                for (int i = rowCount; i > 0; i--)
                {
                    SAPbouiCOM.Cell cell = control.Columns.Item(1).Cells.Item(i);
                    SAPbouiCOM.EditText txtBox = cell.Specific as SAPbouiCOM.EditText;
                    if (!string.IsNullOrEmpty(txtBox.Value))
                    {
                        return BubbleEvent;
                    }
                    else
                    {
                        control.DeleteRow(i);
                        BubbleEvent = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
            }
            return BubbleEvent;
        }
        public static void TrySetValue(this SAPbouiCOM.EditText control, string value)
        {
            try
            {
                control.Value = value;
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
            }
        }
        public static int GetColumnIndex(this SAPbouiCOM.Matrix mtxControl, string columnName)
        {
            var index = 0;
            for (int i = 0; i < mtxControl.Columns.Count; i++)
            {
                if (mtxControl.Columns.Item(i).UniqueID == columnName)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public static object GetCellValue(this SAPbouiCOM.Matrix control, string column, object row)
        {

            if (control.Columns.Item(column).Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
            {
                return ((dynamic)control.Columns.Item(column).Cells.Item(row).Specific).Checked;
            }
            else
            {
                return ((dynamic)control.Columns.Item(column).Cells.Item(row).Specific).Value;
            }

        }
        public static void HideColumns(this SAPbouiCOM.Matrix control, bool isVisible, params string[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                control.Columns.Item(ids[i]).Visible = isVisible;
            }
        }
        public static bool IsMatrixCellsNotEmpty(this SAPbouiCOM.Matrix mtxControl, params string[] fields)
        {
            bool result = true;
            List<string> ValuesList = new List<string>();
            for (int i = 1; i <= mtxControl.RowCount; i++)
            {
                foreach (var v in fields)
                {
                    ValuesList.Add(mtxControl.GetCellValue(v, i).ToString());
                }
                var emptyValues = ValuesList.Where(x => x.Equals(string.Empty)).ToList();
                if (emptyValues.Count != 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public static void SetCellValue(this SAPbouiCOM.Matrix control, string column, object row, object newValue)
        {
            try
            {

                ((dynamic)control.Columns.Item(column).Cells.Item(row).Specific).Value = newValue;

            }
            catch (System.Exception ex)
            {
                //Utility.LogException(ex);
               // Log.LogException(LogLevel.Error, ex);
            }
        }
        public static void TrySetCellValue(this SAPbouiCOM.Matrix control, string column, object row, object newValue)
        {
            try
            {
                SetCellValue(control, column, row, newValue);
            }
            catch (System.Exception ex)
            {
                Utility.LogException(ex);
                //Log.LogException(LogLevel.Error, ex);
            }
        }
        #endregion

        #region DATATABLE METHODS
        public static string[,] ConvertTo2DArray(this SAPbouiCOM.DataTable control)
        {
            int tableRows = control.Rows.Count;
            int tableColumns = control.Columns.Count;
            string[,] temp = new string[tableRows + 1, tableColumns];

            for (int columnHeaderIndex = 0; columnHeaderIndex < tableColumns; columnHeaderIndex++)
            {
                temp[0, columnHeaderIndex] = control.Columns.Item(columnHeaderIndex).Name;
            }
            for (int rowIndex = 0; rowIndex < tableRows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < tableColumns; columnIndex++)
                {
                    temp[rowIndex + 1, columnIndex] = control.Columns.Item(columnIndex).Cells.Item(rowIndex).Value.ToString();
                }
            }
            return temp;
        }
        #endregion

        #region GRID METHODS
        public static void AddLine(this SAPbouiCOM.Grid grdControl)
        {
            grdControl.DataTable.Rows.Add(1);
            grdControl.Columns.Item(0).Click(grdControl.DataTable.Rows.Count - 1);
        }
        public static void SetGridHeaderIndex(this SAPbouiCOM.Grid control)
        {
            for (int i = 0; i < control.Rows.Count; i++)
            {
                control.RowHeaders.SetText(i, (i + 1).ToString());
            }
            control.Columns.Item("RowsHeader").TitleObject.Caption = "#";
        }
        public static int GetSelectedRowIndex(this SAPbouiCOM.Grid gvControl)
        {
            var selectedRows = gvControl.Rows.SelectedRows.Cast<dynamic>();
            int selectedRow = selectedRows.FirstOrDefault();
            return selectedRow;
        }
        public static void DeleteRow(this SAPbouiCOM.Grid grdControl, int rowNumber)
        {
            grdControl.DataTable.Rows.Remove(rowNumber);
        }
        #endregion

        #region CONTROLS METHODS
        public static void ReleaseObject(this object ob)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ob);
            ob = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public static string ToUserFieldFormat(this string field)
        {
            return string.Format("U_{0}", field);
        }
        public static void ActivateControl(this SAPbouiCOM.EditText control)
        {
            if (control != null)
                control.Active = true;
            control = null;
        }
        #endregion
    }
}
