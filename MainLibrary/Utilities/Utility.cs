using MainLibrary.SAPB1;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


namespace MainLibrary.Utilities
{
    public static class Utility
    {
        #region Members
        private static FileInfo _fileInfo;
        private static FileStream _sw;
        private static string _fileName;
        #endregion

        #region Utilities Methods
        public static bool IsNumber(string key)
        {
            return Regex.IsMatch(key, @"^[0-9]*\.?[0-9]+$");
        }
        public static T Convert<T>(this string input)
        {
            var convert = TypeDescriptor.GetConverter(typeof(T));
            if (convert != null)
                return (T)convert.ConvertFromString(input);
            return default(T);
        }
        public static DateTime GetDateTime(string date, string dateFormat)
        {
            return DateTime.ParseExact(date, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static DateTime GetDateTime(SAPbouiCOM.EditText control, string dateFormat)
        {
            return DateTime.ParseExact(control.Value, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static Dictionary<string, double> GetDictionary(DataTable dt)
        {
            return dt.AsEnumerable()
              .ToDictionary<DataRow, string, double>(row => row.Field<string>(0),
                                                        row => double.Parse(row.Field<string>(1)));
        }
        public static DataTable ConvertRecordset(SAPbobsCOM.Recordset SAPRecordset)
        {
            int ColCount;
            DataRow NewRow;
            DataColumn NewCol;
            DataTable dtTable = new DataTable();
            DataTable functionReturnValue = default(DataTable);

            try
            {
                for (ColCount = 0; (ColCount <= (SAPRecordset.Fields.Count - 1)); ColCount++)
                {
                    NewCol = new DataColumn(SAPRecordset.Fields.Item(ColCount).Name);
                    dtTable.Columns.Add(NewCol);
                }

                do
                {
                    NewRow = dtTable.NewRow();

                    for (ColCount = 0; (ColCount <= (SAPRecordset.Fields.Count - 1)); ColCount++)
                        NewRow[SAPRecordset.Fields.Item(ColCount).Name] = SAPRecordset.Fields.Item(ColCount).Value;

                    dtTable.Rows.Add(NewRow);
                    SAPRecordset.MoveNext();
                }
                while (!SAPRecordset.EoF);

                return dtTable;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                //Log.LogException(LogLevel.Error, ex);
                return functionReturnValue;
            }
        }
        //public static void WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType)
        //{
        //    Microsoft.Office.Interop.Excel.Application excel;
        //    Microsoft.Office.Interop.Excel.Workbook excelworkBook;
        //    Microsoft.Office.Interop.Excel.Worksheet excelSheet;
        //    Microsoft.Office.Interop.Excel.Range excelCellrange;

        //    try
        //    {
        //        // Start Excel and get Application object.
        //        excel = new Microsoft.Office.Interop.Excel.Application();

        //        // for making Excel visible
        //        excel.Visible = false;
        //        excel.DisplayAlerts = false;

        //        // Creation a new Workbook
        //        excelworkBook = excel.Workbooks.Add(Type.Missing);

        //        // Workk sheet
        //        excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
        //        excelSheet.Name = worksheetName;


        //        excelSheet.Cells[1, 1] = ReporType;
        //        excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

        //        // loop through each row and add values to our sheet
        //        int rowcount = 2;

        //        foreach (DataRow datarow in dataTable.Rows)
        //        {
        //            rowcount += 1;
        //            for (int i = 1; i <= dataTable.Columns.Count; i++)
        //            {
        //                // on the first iteration we add the column headers
        //                if (rowcount == 3)
        //                {
        //                    excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
        //                    excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

        //                }

        //                excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

        //                //for alternate rows
        //                if (rowcount > 3)
        //                {
        //                    if (i == dataTable.Columns.Count)
        //                    {
        //                        if (rowcount % 2 == 0)
        //                        {
        //                            excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
        //                            FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
        //                        }

        //                    }
        //                }

        //            }

        //        }

        //        // now we resize the columns
        //        excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
        //        excelCellrange.EntireColumn.AutoFit();
        //        Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
        //        border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        //        border.Weight = 2d;


        //        excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
        //        FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


        //        //now save the workbook and exit Excel


        //        excelworkBook.SaveAs(saveAsLocation); ;
        //        excelworkBook.Close();
        //        excel.Quit();
        //       // return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogErrors(ex.Message);
        //        //MessageBox.Show(ex.Message);
        //       // return false;
        //    }
        //    finally
        //    {
        //        excelSheet = null;
        //        excelCellrange = null;
        //        excelworkBook = null;
        //    }

        //}

        //public static void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        //{
        //    range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
        //    range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
        //    if (IsFontbool == true)
        //    {
        //        range.Font.Bold = IsFontbool;
        //    }
        //}

        public static string  Left(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
        public static string Right(string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }

        #endregion

        #region File Processing Methods
        public static void DeleteTextFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
        public static string GenerateTextFile(string fileName)
        {
            return GenerateTextFile(System.Configuration.ConfigurationManager.AppSettings["filePath"].ToString(), fileName);
        }
        public static string GenerateTextFile(string filePath, string fileName)
        {
            // Utilities.WriteToEventLog(string.Format("[DR] Generating txt file. FilePath: {0} FileName: {1}", filePath, fileName));
            try
            {
                if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
                {
                    filePath = System.Environment.CurrentDirectory;
                    //Utilities.WriteToEventLog(string.Format("[DR] Warning FilePath not found. Writting to: {0}", filePath));
                }

                StringBuilder s = new StringBuilder(filePath);
                s.AppendFormat("\\{0}.txt", fileName);
                if (!File.Exists(s.ToString()))
                    File.CreateText(s.ToString()).Dispose();

                return s.ToString();
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                //   Log.LogException(LogLevel.Error, ex);                
                return string.Empty;
            }

        }
        #endregion

        #region Exception Handling Methods
        public static string GetErrorMessage()
        {
            string ErrMsg = string.Empty;
            int errCode = int.MinValue;
            B1Helper.DiCompany.GetLastError(out errCode, out ErrMsg);
            return ErrMsg;
        }
        public static void LogErrors(string p)
        {
            Application.SBO_Application.StatusBar.SetSystemMessage(p, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
        }
        public static void LogException(string ex)
        {
            Application.SBO_Application.SetStatusBarMessage(ex, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            LogToFile(ex);
        }
        public static void LogException(Exception ex)
        {
            Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            LogToFile(ex.Message);
        }
        private static void LogToFile(string message)
        {
            DirectoryInfo dInfo;
            StringBuilder sb = new StringBuilder();
            var folderPath = System.Configuration.ConfigurationManager.AppSettings["LoggingPath"];
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            dInfo = new DirectoryInfo(folderPath);

            if (_fileName == null)
            {
                var filePath = System.IO.Path.Combine(dInfo.FullName, string.Concat(Guid.NewGuid(), ".txt"));
                _fileName = filePath;
                _fileInfo = new FileInfo(filePath);
                _sw = _fileInfo.Create();

                using (StreamWriter sw = new StreamWriter(_sw))
                {
                    try
                    {
                        sb.Append("Time");
                        sb.Append(' ', 50);
                        sb.AppendLine("Error");
                        sb.Append(DateTime.Now.ToLongTimeString());
                        sb.Append(' ', 50);
                        sb.Append(message, 0, message.Length);
                        sw.WriteLine(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        Utility.LogException(ex);
                        //  Log.LogException(LogLevel.Error, ex);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_fileName))
                {
                    sb.Append(DateTime.Now.ToLongTimeString());
                    sb.Append(' ', 50);
                    sb.Append(message, 0, message.Length);
                    sw.WriteLine(sb.ToString());
                }
            }
        }
        #endregion

        #region Excel Component 
    
        #endregion


        #region SYSTEM SWITCHER

        public static string GetStoredProcFormat(string procName, Dictionary<string, string> parameters)
        {
            var query = new StringBuilder();

            int i = 0;
            if (B1Helper.DiCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                query.Append(string.Format("CALL {0} ", procName));
                if (parameters.Count > 0)
                {
                    query.Append("(");
                    foreach (var kvp in parameters)
                    {
                        if (i == 0)
                            query.Append(string.Format("{0}", kvp.Value));
                        else
                            query.Append(string.Format(",{0}", kvp.Value));
                        i++;
                    }
                    query.Append(")");
                }
            }
            else
            {
                query.Append(string.Format("EXEC {0} ", procName));
                foreach (var kvp in parameters)
                {
                    if (i == 0)
                        query.Append(string.Format("{0} = {1}", kvp.Key, kvp.Value));
                    else
                        query.Append(string.Format(",{0} = {1}", kvp.Key, kvp.Value));

                    i++;
                }
            }
            return query.ToString();
        }

        #endregion
    }
}
