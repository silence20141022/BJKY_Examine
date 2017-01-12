using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace Aim.Component.ThirdpartySupport.MsOffice
{
    // Ms Office Excel数据处理
    public class ExcelService : IDisposable
    {
        #region 私有成员

        private static ExcelService service;

        private DateTime? _appCreatedTimeBegin;
        private DateTime? _appCreatedTimeEnd;

        private Excel.Application app;

        #endregion

        #region 构造析构函数

        /// <summary>
        /// 单体模式
        /// </summary>
        private ExcelService()
        {
        }

        ~ExcelService()
        {
            this.Close();
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 服务实例
        /// </summary>
        internal static ExcelService Instance
        {
            get
            {
                if (service == null)
                {
                    service = new ExcelService();
                }

                return service;
            }
        }

        /// <summary>
        /// Excel应用
        /// </summary>
        internal Excel.Application Application
        {
            get
            {
                if (app == null)
                {
                    _appCreatedTimeBegin = DateTime.Now;    // 应用创建时间开始
                    app = new Excel.Application();
                    _appCreatedTimeEnd = DateTime.Now;  // 应用创建时间结束
                }

                return app;
            }
        }

        /// <summary>
        /// 获取所有的Workbook
        /// </summary>
        /// <returns></returns>
        internal IList<Excel.Workbook> GetAllWorkbooks()
        {
            IList<Excel.Workbook> wbs = new List<Excel.Workbook>();

            System.Collections.IEnumerator wbenum = Application.Workbooks.GetEnumerator();

            while (wbenum.MoveNext())
            {
                Excel.Workbook twb = wbenum.Current as Excel.Workbook;
                if (wbs != null)
                {
                    wbs.Add(twb);
                }
            }

            return wbs;

        }

        /// <summary>
        /// 获取Excel处理器
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static ExcelProcessor GetProcessor(string filepath)
        {
            return new ExcelProcessor(Instance.Application, filepath);
            //return new ExcelProcessor(filepath);
        }

        /// <summary>
        /// 获取Excel处理器
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static ExcelProcessor GetProcessor(string filepath, string extProp)
        {
            return new ExcelProcessor(Instance.Application, filepath, extProp);
        }

        /// <summary>
        /// 从DataTable读取数据到Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sw"></param>
        public static void WriteExcel(DataTable dt, StreamWriter w)
        {
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    w.Write(dt.Columns[i]);
                    w.Write(' ');
                }

                w.Write(" ");

                object[] values = new object[dt.Columns.Count];
                foreach (DataRow dr in dt.Rows)
                {
                    values = dr.ItemArray;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        w.Write(values[i]);
                        w.Write(' ');
                    }
                    w.Write(" ");
                }
                w.Flush();
                w.Close();
            }
            finally
            {
                w.Close();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 关闭应用
        /// </summary>
        private void Close()
        {
            if (app != null)
            {
                app.Workbooks.Close();
                app.Quit();
                app = null;

                COMHelper.ReleaseObject(app);
            }

            if (_appCreatedTimeBegin != null && _appCreatedTimeEnd != null)
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
}
