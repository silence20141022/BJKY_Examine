using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NHibernate.Id;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.FileSystem;
using Aim.Component.ThirdpartySupport.MsOffice;

namespace Aim.Portal.Data
{
    /// <summary>
    /// 数据导入服务
    /// </summary>
    public class DataImportService
    {
        /// <summary>
        /// 当模版文件变化时操作
        /// </summary>
        /// <param name="ent"></param>
        public static void DoImportTemplateFileChanged(SysDataImportTemplate ent)
        {
            // 当模版文件有变化是，同时更新下载文件与配置模版
            string filePath = FileService.GetFilePathByFullID(ent.TemplateFileID.TrimEnd(','));

            ImportTemplateParser itp = new ImportTemplateParser(filePath);
            ImportTemplateStructure its = itp.GetStructure();
            ent.Config = its.GetConfig();
            ent.DownloadFileID = ent.TemplateFileID;    // 这里下载文件与模版文件为同一文件
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tmplCode">模板编码</param>
        /// <param name="fileFullName"></param>
        public static void ImportDataByTemplateCode(string tmplCode, string fileFullName)
        {
            SysDataImportTemplate tmplEnt = SysDataImportTemplate.FindFirstByProperties("Code", tmplCode);

            ImportTemplateStructure its = ImportTemplateStructure.GetFromConfig(tmplEnt.Config);
            string filePath = FileService.GetFilePathByFullID(fileFullName);

            ImportData(its, filePath);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="template"></param>
        /// <param name="fileFullName"></param>
        public static void ImportData(string config, string fileFullName)
        {
            ImportTemplateStructure its = ImportTemplateStructure.GetFromConfig(config);
            string filePath = FileService.GetFilePathByFullID(fileFullName);

            ImportData(its, filePath);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="filePath"></param>
        /// <param name="blockSize"></param>
        public static void ImportData(ImportTemplateStructure struc, string filePath)
        {
            SqlConnection sqlConnection = DataHelper.GetCurrentDbConnection() as SqlConnection;

            ImportData(struc, struc.DefaultGroup.PropertyNode.Target, sqlConnection, filePath, 0);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="targetTable"></param>
        /// <param name="filePath"></param>
        public static void ImportData(ImportTemplateStructure struc, string targetTable, string filePath)
        {
            SqlConnection sqlConnection = DataHelper.GetCurrentDbConnection() as SqlConnection;

            ImportData(struc, targetTable, sqlConnection, filePath, 0);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="targetTable"></param>
        /// <param name="primaryKey"></param>
        /// <param name="filePath"></param>
        /// <param name="blockSize"></param>
        public static void ImportData(ImportTemplateStructure struc, string targetTable, SqlConnection sqlConnection, string filePath)
        {
            int blockSize = 0;
            if (struc.DefaultGroup.PropertyNode.BlockSize.HasValue)
            {
                blockSize = struc.DefaultGroup.PropertyNode.BlockSize.Value;
            }

            ImportData(struc, targetTable, sqlConnection, filePath, blockSize);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="struc">模版结构</param>
        /// <param name="targetTable">目标表</param>
        /// <param name="primaryKey">表主键</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="blockSize">数据块大小</param>
        public static void ImportData(ImportTemplateStructure struc, string targetTable, SqlConnection sqlConnection, string filePath, int blockSize)
        {
            string primaryKey = String.Empty;

            // 获取主键
            string[] primaryKeys = SysTableStructureRule.GetTablePrimaryKey(targetTable);

            if (primaryKeys.Length == 1)
            {
                primaryKey = primaryKeys[0];
            }

            IIdentifierGenerator idGenerator = struc.DefaultGroup.GetIDGenerator();

            IList<DataTable> dts = DataImportService.GetDataTableList(struc, filePath, blockSize);

            int successAmount = 0;
            foreach (DataTable tdt in dts)
            {
                try
                {
                    if (!String.IsNullOrEmpty(primaryKey))
                    {
                        if (!tdt.Columns.Contains(primaryKey))
                        {
                            tdt.Columns.Add(primaryKey);
                        }

                        foreach (DataRow tdrow in tdt.Rows)
                        {
                            tdrow[primaryKey] = idGenerator.Generate(null, null);
                        }
                    }

                    DataHelper.CopyDataToDatabase(tdt, sqlConnection, targetTable);

                    successAmount += tdt.Rows.Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("导入数据失败，已完成导入“" + successAmount + "”条。" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取EasyDictionary数据
        /// </summary>
        /// <param name="tmplCode">模版编码</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable GetDataTableByTemplateCode(string tmplCode, string fileFullName)
        {
            SysDataImportTemplate tmplEnt = SysDataImportTemplate.FindFirstByProperties("Code", tmplCode);

            ImportTemplateStructure struc = ImportTemplateStructure.GetFromConfig(tmplEnt.Config);
            string filePath = FileService.GetFilePathByFullID(fileFullName);

            DataTable dt = GetDataTable(struc, filePath);

            return dt;
        }

        /// <summary>
        /// 获取EasyDictionary数据
        /// </summary>
        /// <param name="struc">模版结构</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string config, string fileFullName)
        {
            ImportTemplateStructure struc = ImportTemplateStructure.GetFromConfig(config);
            string filePath = FileService.GetFilePathByFullID(fileFullName);

            DataTable dt = GetDataTable(struc, filePath);

            return dt;
        }

        /// <summary>
        /// 获取DataTable数据
        /// </summary>
        /// <param name="struc">模版结构</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable GetDataTable(ImportTemplateStructure struc, string filePath)
        {
            // 获取最大Table
            IList<DataTable> dts = GetDataTableList(struc, filePath, 0);

            if (dts.Count > 0)
            {
                return dts[0];
            }

            return null;
        }

        /// <summary>
        /// 获取根据blockSize获取DataTable列表
        /// </summary>
        /// <param name="struc">模版结构</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="blockSize">每个DataTable最大大小</param>
        /// <returns></returns>
        public static IList<DataTable> GetDataTableList(ImportTemplateStructure struc, string filePath, int blockSize)
        {
            if (blockSize <= 0)
            {
                blockSize = 1000;   // 默认一次性处理1000行
            }

            ImportTemplateCommandNode itcnBegin = struc.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ImportTemplateCommandCode.Begin);
            ImportTemplateCommandNode itcnEnd = struc.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ImportTemplateCommandCode.End);

            IList<DataTable> dtList = new List<DataTable>();

            DataSet ds = new DataSet();

            using (ExcelProcessor processor = ExcelService.GetProcessor(filePath))
            {
                ds = processor.GetDataSet();
            }

            DataTable dt = ds.Tables[0];

            IList<ImportTemplateColumnNode> ccnodes = struc.DefaultGroup.GetCommonColumnNodeList();

            // 设置公用数据列默认值
            foreach (ImportTemplateColumnNode tnode in ccnodes)
            {
                // 行第一个“-1”DataSet从第二行开始,行第二个“-1”，列第一个“-1”数组从0开始
                tnode.DefaultValue = dt.Rows[tnode.ValueRowIndex.Value - 2][tnode.ValueColumnIndex.Value - 1];
            }

            IList<ImportTemplateColumnNode> ocnodes = struc.DefaultGroup.GetOrdinaireColumnNodeList();

            int startRowIndex = itcnBegin.RowIndex;
            int endRowIndex = itcnEnd.RowIndex;

            int startColumnIndex = itcnBegin.ColumnIndex;
            int endColumnIndex = itcnEnd.ColumnIndex;

            if (itcnBegin.RowIndex == itcnEnd.RowIndex)
            {
                // DataSet从第二行开始这里需要+1
                endRowIndex = dt.Rows.Count + 1;
            }

            DataTable tdt = null;

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                if ((i - startRowIndex) % blockSize == 0)
                {
                    tdt = struc.DefaultGroup.GetDataTableSchema();
                    dtList.Add(tdt);
                }

                DataRow drow = tdt.NewRow();

                bool emptyflag = true;

                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    ImportTemplateColumnNode tnode = ocnodes.First(tent => (tent.ValueColumnIndex) == j);

                    if (tnode != null)
                    {
                        object tval = dt.Rows[(i - 2)][(j - 1)];

                        if (tval != null && tval.ToString().Trim() != String.Empty)
                        {
                            emptyflag = false;
                        }

                        tval = (tval != null ? tval : tnode.DefaultValue);

                        drow[tnode.ColumnName] = tval;
                    }
                }

                // 当前行为空行，这跳过执行
                if (emptyflag == true)
                {
                    continue;
                }

                // 设置公共列值
                foreach (ImportTemplateColumnNode tnode in ccnodes)
                {
                    drow[tnode.ColumnName] = tnode.DefaultValue;
                }

                tdt.Rows.Add(drow);
            }

            return dtList;
        }
    }
}
