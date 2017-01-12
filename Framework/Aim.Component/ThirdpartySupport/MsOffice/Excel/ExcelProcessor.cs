using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace Aim.Component.ThirdpartySupport.MsOffice
{
    /// <summary>
    /// Excel处理器
    /// </summary>
    public class ExcelProcessor : IDisposable
    {
        #region 成员属性

        private string _filePath;
        private string _extProp = "Excel 8.0";

        private bool _isNewApp = false; // 自己创建Application
        private DateTime? _appCreatedTimeBegin;
        private DateTime? _appCreatedTimeEnd;

        Excel.Application _app;
        Excel.Workbook _wb;

        /// <summary>
        /// Excel应用
        /// </summary>
        public Excel.Application Application
        {
            get{
                if (_app == null)
                {
                    _appCreatedTimeBegin = DateTime.Now;    // 应用创建时间开始
                    _app = new Excel.Application();
                    _appCreatedTimeEnd = DateTime.Now;    // 应用创建时间结束

                    _isNewApp = true;
                }

                if (!String.IsNullOrEmpty(_filePath))
                {
                    _wb = _app.Workbooks.Add(_filePath);
                }

                return _app;
            }
        }

        /// <summary>
        /// Excel工作册
        /// </summary>
        public Excel.Workbook Workbook
        {
            get
            {
                if (_wb == null)
                {
                    if (!String.IsNullOrEmpty(_filePath))
                    {
                        _wb = Application.Workbooks.Add(_filePath);
                    }
                }

                return _wb;
            }
        }

        /// <summary>
        /// Excel文件路径
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string ExtProp
        {
            get { return _extProp; }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string t_connstr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties={1}", _filePath, _extProp);

                return t_connstr;
            }
        }

        #endregion

        #region 构造析构函数

        internal ExcelProcessor(string filePath)
        {
            this._filePath = filePath;
        }

        internal ExcelProcessor(string filePath, string extProp)
            : this(filePath)
        {
            this._extProp = extProp;
        }

        internal ExcelProcessor(Excel.Application app, string filePath)
        {
            this._app = app;
            this._filePath = filePath;
        }

        internal ExcelProcessor(Excel.Application app, string filePath, string extProp)
            : this(app, filePath)
        {
            this._extProp = extProp;
        }

        ~ExcelProcessor()
        {
            this.Close();
        }

        #endregion

        #region 公共函数

        #region 数据操作

        /// <summary>
        /// 获取工作表名列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            List<string> sns = new List<string>();

            OleDbConnection conn = this.GetOleDbConnection();

            conn.Open();
            DataTable sntable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]{null, null, null, "TABLE"});
            conn.Close();

            foreach (DataRow dr in sntable.Rows)
            {
                sns.Add(dr[2].ToString());
            }

            return sns;
        }

        /// <summary>
        /// 获取第一个工作表DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            string sn = GetFirstSheetName();
            DataSet ds = GetDataSet(sn);

            return ds;
        }

        /// <summary>
        /// 根据工作表名获取DataSet
        /// </summary>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sheetname)
        {
            DataSet ds = new DataSet();

            OleDbConnection conn = this.GetOleDbConnection();

            OleDbDataAdapter oada = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", sheetname), conn);
            oada.Fill(ds);

            return ds;
        }

        /// <summary>
        /// 获取OleDb连接
        /// </summary>
        /// <returns></returns>
        public OleDbConnection GetOleDbConnection()
        {
            OleDbConnection conn = new OleDbConnection(this.ConnectionString);

            return conn;
        }

        #endregion

        #region 表操作

        /// <summary>
        /// 获取第一个工作表名
        /// </summary>
        /// <returns></returns>
        public string GetFirstSheetName()
        {
            IList<string> sns = GetSheetNames();

            if (sns.Count > 0)
            {
                return sns[0].TrimEnd('$');
            }

            return null;
        }

        /// <summary>
        /// 获取第一个工作表
        /// </summary>
        /// <returns></returns>
        public Excel.Worksheet GetFirstSheet()
        {
            string sname = GetFirstSheetName();

            if (!String.IsNullOrEmpty(sname))
            {
                return GetSheet(sname);
            }

            return null;
        }

        /// <summary>
        /// 获取一个工作表
        /// </summary>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public Excel.Worksheet GetSheet(string sheetname)
        {
            Excel.Worksheet s = Workbook.Worksheets[sheetname] as Excel.Worksheet;

            return s;
        }

        /// <summary>
        /// 获取所有工作表
        /// </summary>
        /// <returns></returns>
        public IList<Excel.Worksheet> GetAllSheets()
        {
            IList<Excel.Worksheet> wss = new List<Excel.Worksheet>();

            System.Collections.IEnumerator wsenum = Workbook.Worksheets.GetEnumerator();

            while (wsenum.MoveNext())
            {
                Excel.Worksheet tws = wsenum.Current as Excel.Worksheet;
                if (tws != null)
                {
                    wss.Add(tws);
                }
            }

            return wss;
        }

        /// <summary>
        /// 添加一个工作表
        /// </summary>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public Excel.Worksheet AddSheet(string sheetname)
        {
            Excel.Worksheet s = Workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing) as Excel.Worksheet;
            s.Name = sheetname;

            return s;
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        public void DelSheet(string sheetname)
        {
            Excel.Worksheet s = GetSheet(sheetname);

            s.Delete();
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
        public Excel.Worksheet RenameSheet(string oldname, string newname)
        {
            Excel.Worksheet s = GetSheet(oldname);

            return RenameSheet(s, newname);
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
        public Excel.Worksheet RenameSheet(Excel.Worksheet sheet, string newname)
        {
            sheet.Name = newname;

            return sheet;
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        public void SetCellValue(string sheetname, int rowIndex, int columnIndex, object value)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            SetCellValue(ws, rowIndex, columnIndex, value);
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        public void SetCellValue(Excel.Worksheet ws, int rowIndex, int columnIndex, object value)
        {
            ws.Cells[rowIndex, columnIndex] = value;
        }

        /// <summary>
        /// 获取Excel范围
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="endColumnIndex"></param>
        /// <returns></returns>
        public Excel.Range GetRange(string sheetname, int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            return GetRange(ws, startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
        }

        /// <summary>
        /// 获取Excel范围
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="endColumnIndex"></param>
        /// <returns></returns>
        public Excel.Range GetRange(Excel.Worksheet ws, int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            Excel.Range range = ws.get_Range(ws.Cells[startRowIndex, startColumnIndex], ws.Cells[endRowIndex, endColumnIndex]);

            return range;
        }

        /// <summary>
        /// 设置单元格属性
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="endColumnIndex"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="horizontalAlignment"></param>
        public void SetCellProperty(string sheetname, int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex, int size, string name, Excel.Constants color, Excel.Constants horizontalAlignment)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            SetCellProperty(ws, startRowIndex, startColumnIndex, endRowIndex, endColumnIndex, size, name, color, horizontalAlignment);
        }

        /// <summary>
        /// 设置单元格属性
        /// </summary>
        public void SetCellProperty(Excel.Worksheet ws, int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex, int size, string name, Excel.Constants color, Excel.Constants horizontalAlignment)
        {
            Excel.Range range = GetRange(ws, startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
            range.Font.Name = name;
            range.Font.Size = size;
            range.Font.Color = color;
            range.HorizontalAlignment = horizontalAlignment;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <param name="columnIndex2"></param>
        public void UniteCells(Excel.Worksheet ws, int rowIndex1, int columnIndex1, int rowIndex2, int columnIndex2)
        {
            Excel.Range range = GetRange(ws, rowIndex1, columnIndex1, rowIndex2, columnIndex2);

            range.Merge(Type.Missing);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <param name="columnIndex2"></param>
        public void UniteCells(string sheetname, int rowIndex1, int columnIndex1, int rowIndex2, int columnIndex2)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            UniteCells(ws, rowIndex1, columnIndex1, rowIndex2, columnIndex2);
        }

        /// <summary>
        /// 将内存中数据表格插入到Excel指定工作表的指定位置 为在使用模板时控制格式时使用
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        public void InsertTable(DataTable dt, string sheetname, int startRowIndex, int startColumnIndex)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            InsertTable(dt, ws, startRowIndex, startColumnIndex);
        }

        /// <summary>
        /// 将内存中数据表格插入到Excel指定工作表的指定位置 为在使用模板时控制格式时使用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        public void InsertTable(DataTable dt, Excel.Worksheet ws, int startRowIndex, int startColumnIndex)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; i++)
                {
                    ws.Cells[startRowIndex + i, startColumnIndex + j] = dt.Rows[i][j];
                }
            }
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertPicture(string picFileName, string sheetname, float left, float top, float height, float width)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            ws.Shapes.AddPicture(picFileName, MsoTriState.msoFalse, MsoTriState.msoTrue, left, top, height, width);
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertPicture(string picFileName, Excel.Worksheet ws, float left, float top, float height, float width)
        {
            ws.Shapes.AddPicture(picFileName, MsoTriState.msoFalse, MsoTriState.msoTrue, left, top, height, width);
        }

        /// <summary>
        /// 插入图表
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="sheetname"></param>
        /// <param name="dsRowIndex1"></param>
        /// <param name="dsColumnIndex1"></param>
        /// <param name="dsRowIndex2"></param>
        /// <param name="dsColumnIndex2"></param>
        /// <param name="chartDataType"></param>
        public void InsertActiveChart(Excel.XlChartType chartType, string sheetname, int dsRowIndex1, int dsColumnIndex1, int dsRowIndex2, int dsColumnIndex2, Excel.XlRowCol chartDataType)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            InsertActiveChart(chartType, ws, dsRowIndex1, dsColumnIndex1, dsRowIndex2, dsColumnIndex2, chartDataType);
        }

        /// <summary>
        /// 插入图表
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="ws"></param>
        /// <param name="dsRowIndex1"></param>
        /// <param name="dsColumnIndex1"></param>
        /// <param name="dsRowIndex2"></param>
        /// <param name="dsColumnIndex2"></param>
        /// <param name="chartDataType"></param>
        public void InsertActiveChart(Excel.XlChartType chartType, Excel.Worksheet ws, int dsRowIndex1, int dsColumnIndex1, int dsRowIndex2, int dsColumnIndex2, Excel.XlRowCol chartDataType){
            Workbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            Excel.Range range = GetRange(ws, dsRowIndex1, dsColumnIndex1, dsRowIndex2, dsColumnIndex2);

            Workbook.ActiveChart.ChartType = chartType;
            Workbook.ActiveChart.SetSourceData(range, chartDataType);
            Workbook.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, ws.Name);
        }

        /// <summary>
        /// 获取拥有批注的Cell
        /// </summary>
        /// <returns></returns>
        public IList<ExcelCell> GetCellsWithComment()
        {
            string sheetname = GetFirstSheetName();

            return GetCellsWithComment(sheetname);
        }
        
        /// <summary>
        /// 获取拥有批注的Cell
        /// </summary>
        /// <returns></returns>
        public IList<ExcelCell> GetCellsWithComment(string sheetname)
        {
            IList<ExcelCell> cells = new List<ExcelCell>();

            Excel.Worksheet ws = GetSheet(sheetname);
            IList<Excel.Comment> cms = GetAllComments(ws);

            foreach (Excel.Comment tcm in cms)
            {
                Excel.Range trang = tcm.Parent as Excel.Range;
                if (trang != null)
                {
                    cells.Add(new ExcelCell(trang));
                }
            }

            return cells;
        }

        /// <summary>
        /// 获取Excel单元格
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <returns></returns>
        public IList<ExcelCell> GetCells(int rowIndex1, int columnIndex1, int rowIndex2, int columnIndex2)
        {
            string sname = this.GetFirstSheetName();

            return GetCells(sname, rowIndex1, columnIndex1, rowIndex2, columnIndex2);
        }

        /// <summary>
        /// 获取Excel单元格
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <returns></returns>
        public IList<ExcelCell> GetCells(string sheetname, int rowIndex1, int columnIndex1, int rowIndex2, int columnIndex2)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            IList<ExcelCell> cellList = new List<ExcelCell>();

            for (int i = rowIndex1; i < rowIndex2; i++)
            {
                for (int j = columnIndex1; j < columnIndex2; j++)
                {
                    cellList.Add(new ExcelCell(ws, i, j));
                }
            }

            return cellList;
        }

        /// <summary>
        /// 获取指定工作表所有批注
        /// </summary>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public IList<Excel.Comment> GetAllComments(string sheetname)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            return GetAllComments(ws);
        }

        /// <summary>
        /// 获取指定工作表所有批注
        /// </summary>
        /// <param name="ws"></param>
        /// <returns></returns>
        public IList<Excel.Comment> GetAllComments(Excel.Worksheet ws)
        {
            IList<Excel.Comment> cms = new List<Excel.Comment>();

            System.Collections.IEnumerator cmenum = ws.Comments.GetEnumerator();

            while (cmenum.MoveNext())
            {
                Excel.Comment tcm = cmenum.Current as Excel.Comment;
                if (tcm != null)
                {
                    cms.Add(tcm);
                }
            }

            return cms;
        }

        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <param name="columnIndex2"></param>
        public Excel.Comment GetComment(string sheetname, int rowIndex1, int columnIndex1)
        {
            Excel.Worksheet ws = GetSheet(sheetname);

            return GetComment(ws, rowIndex1, columnIndex1);
        }

        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="columnIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <param name="columnIndex2"></param>
        public Excel.Comment GetComment(Excel.Worksheet ws, int rowIndex, int columnIndex)
        {
            Excel.Range range = GetRange(ws, rowIndex, columnIndex, rowIndex, columnIndex);

            return range.Comment;
        }

        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        public string GetCommentText(int rowIndex, int columnIndex)
        {
            Excel.Worksheet ws = GetFirstSheet();

            return GetCommentText(ws, rowIndex, columnIndex);
        }

        /// <summary>
        /// 获取批注文本
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="rowIndex1"></param>
        /// <param name="rowIndex2"></param>
        /// <returns></returns>
        public string GetCommentText(string sheetname, int rowIndex, int columnIndex)
        {
            Excel.Comment comment = GetComment(sheetname, rowIndex, columnIndex);

            return GetCommentText(comment);
        }

        /// <summary>
        /// 获取批注文本
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string GetCommentText(Excel.Worksheet ws, int rowIndex, int columnIndex)
        {
            Excel.Comment comment = GetComment(ws, rowIndex, columnIndex);

            return GetCommentText(comment);
        }

        /// <summary>
        /// 获取批注文本
        /// </summary>
        /// <returns></returns>
        public string GetCommentText(Excel.Comment comment)
        {
            return comment.Text(Type.Missing, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool SaveFile()
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                try
                {
                    Workbook.Save();
                    return true;
                }
#if debug
                catch (Exception ex)
#else
                catch
#endif
                {
                }
            }

            return false;
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <returns></returns>
        public bool SaveFileAs(object filename)
        {
            try
            {
                Workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                return true;
            }
#if debug
                catch (Exception ex)
#else
            catch
#endif
            { }

            return false;
        }

        #endregion

        #endregion

        #region 私有函数

        /// <summary>
        /// 关闭应用
        /// </summary>
        private void Close()
        {
            if (_wb != null)
            {
                COMHelper.ReleaseObject(_wb);
            }

            if (_isNewApp && _appCreatedTimeBegin != null && _appCreatedTimeEnd != null)
            {
                COMHelper.KillProcessByNameAndStartTime(Aim.Component.ThirdpartySupport.Application.MS_OFFICE_EXCEL_PROCESS_NAME, 
                    _appCreatedTimeBegin.Value, _appCreatedTimeEnd.Value);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }

    /// <summary>
    /// Excel单元格属性
    /// </summary>
    public class ExcelCell
    {
        #region 成员函数

        private int _rowIndex = 0;

        /// <summary>
        /// RowIndex位置
        /// </summary>
        public int RowIndex
        {
            get { return _rowIndex; }
        }


        private int _columnIndex = 0;

        /// <summary>
        /// ColumnIndex位置
        /// </summary>
        public int ColumnIndex
        {
            get { return _columnIndex; }
        }

        private string _sheetName = String.Empty;

        /// <summary>
        /// 工作表名
        /// </summary>
        public string SheetName
        {
            get { return _sheetName; }
        }

        private string _common = String.Empty;

        /// <summary>
        /// 获取批注字符
        /// </summary>
        public string Comment
        {
            get
            {
                return _common;
            }
        }

        private string _text = String.Empty;

        /// <summary>
        /// 获取内容
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
        }

        #endregion

        #region 构造函数d

        public ExcelCell(Excel.Worksheet ws, int rowIndex, int columnIndex)
        {
            Excel.Range range = ws.get_Range(ws.Cells[rowIndex, columnIndex], ws.Cells[rowIndex, columnIndex]);

            Init(range);
        }

        public ExcelCell(Excel.Range range)
        {
            Init(range);
        }

        private void Init(Excel.Range range)
        {
            _columnIndex = range.Column;
            _rowIndex = range.Row;
            _common = range.Comment.Text(Type.Missing, Type.Missing, Type.Missing);
            _text = range.Text.ToString();
            _sheetName = range.Worksheet.Name;
        }

        #endregion
    }
}
