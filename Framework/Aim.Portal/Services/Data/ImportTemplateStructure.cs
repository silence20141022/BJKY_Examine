using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using NHibernate.Id;
using Aim.Data;
using Aim.Portal.Model;

namespace Aim.Portal.Data
{
    /// <summary>
    /// 导入命令码
    /// </summary>
    public enum ImportTemplateCommandCode
    {
        Begin,
        End,
        Other
    }

    /// <summary>
    /// 导入模版结构
    /// </summary>
    public class ImportTemplateStructure
    {
        #region 成员属性

        private string _templateFileTypeString = String.Empty;
        private TemplateFileType _templateFileType = TemplateFileType.Other;
        private ImportTemplateGroupList _groupList;

        /// <summary>
        /// 模版文件类型
        /// </summary>
        [JsonIgnore]
        public TemplateFileType TemplateFileType
        {
            get
            {
                return TemplateFileType;
            }

            set
            {
                _templateFileType = value;
                _templateFileTypeString = _templateFileType.ToString();
            }
        }

        /// <summary>
        /// 模版文件类型字符串
        /// </summary>
        public string TemplateFileTypeString
        {
            get
            {
                return _templateFileTypeString;
            }

            set
            {
                _templateFileTypeString = value;

                try
                {
                    _templateFileType = (TemplateFileType)Enum.Parse(typeof(TemplateFileType), _templateFileTypeString, true);
                }
                catch
                {
                    _templateFileType = TemplateFileType.Other;
                }
            }
        }

        /// <summary>
        /// 组列表
        /// </summary>
        public ImportTemplateGroupList GroupList
        {
            get
            {
                return _groupList;
            }
        }

        public ImportTemplateGroup DefaultGroup
        {
            get
            {
                return GroupList.DefaultGroup;
            }
        }

        #endregion

        #region 构造函数

        public ImportTemplateStructure()
        {
            TemplateFileType = TemplateFileType.Other;

            _groupList = new ImportTemplateGroupList();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        public string GetConfig()
        {
            string _config = JsonHelper.GetJsonString(this);

            return _config;
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 由配置文件获取模版结构
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static ImportTemplateStructure GetFromConfig(string config)
        {
            ImportTemplateStructure its = JsonHelper.GetObject<ImportTemplateStructure>(config);

            return its;
        }

        #endregion
    }

    public class ImportTemplateGroupList : List<ImportTemplateGroup>
    {
        #region 成员属性

        /// <summary>
        /// 默认组
        /// </summary>
        [JsonIgnore]
        public ImportTemplateGroup DefaultGroup
        {
            get
            {
                ImportTemplateGroup _defaultGroup = base.Find(tent => tent.Name == ImportTemplateGroup.DEFULAT_GROUP_NAME);

                if (_defaultGroup == null)
                {
                    _defaultGroup = new DefaultImportTemplateGroup();

                    base.Add(_defaultGroup);
                }

                return _defaultGroup;
            }
        }

        #endregion

        #region 构造函数

        public ImportTemplateGroupList()
        {
            Init();
        }

        private void Init()
        {
            base.Clear();
        }

        #endregion

        #region List成员

        /// <summary>
        /// 添加节点
        /// </summary>
        new public void Remove(ImportTemplateGroup group)
        {
            if (group.IsDefault)
            {
                throw new Exception("不可移除默认组");
            }
            else
            {
                base.Remove(group);
            }
        }

        /// <summary>
        /// 移除指定index
        /// </summary>
        /// <param name="index"></param>
        new public void RemoveAt(int index)
        {
            ImportTemplateGroup group = this[index];

            this.Remove(group);
        }

        /// <summary>
        /// 移除所有，并且重新初始化
        /// </summary>
        new public void RemoveAll(Predicate<ImportTemplateGroup> match)
        {
            List<ImportTemplateGroup> groups = base.FindAll(match);

            for (int i = 0; i < groups.Count; i++)
            {
                this.Remove(groups[i]);
            }
        }

        /// <summary>
        /// 移除指定范围内对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        new public void RemoveRange(int index, int count)
        {
            List<ImportTemplateGroup> groups = base.GetRange(index, count);

            for (int i = 0; i < groups.Count; i++)
            {
                this.Remove(groups[i]);
            }
        }

        /// <summary>
        /// 清空操作
        /// </summary>
        new public void Clear()
        {
            this.Init();
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="group"></param>
        new public void Add(ImportTemplateGroup group)
        {
            if (group.IsDefault)
            {
                throw new Exception("不可添加默认组");
            }
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groups"></param>
        new public void AddRange(IEnumerable<ImportTemplateGroup> groups)
        {
            foreach (ImportTemplateGroup tgroup in groups)
            {
                this.Add(tgroup);
            }
        }

        #endregion
    }

    /// <summary>
    /// 模版组
    /// </summary>
    public class ImportTemplateGroup
    {
        public const string DEFULAT_GROUP_NAME = "Default";

        #region 成员属性

        /// <summary>
        /// 是否默认组
        /// </summary>
        [JsonIgnore]
        public bool IsDefault
        {
            get
            {
                return this._name == DEFULAT_GROUP_NAME;
            }
        }

        protected string _name = String.Empty;

        /// <summary>
        /// 组名
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// 默认属性节点
        /// </summary>
        [JsonIgnore]
        public ImportTemplatePropertyNode PropertyNode
        {
            get
            {
                if (PropertyNodeList.Count > 0)
                {
                    return PropertyNodeList[0];
                }

                return null;
            }
        }

        private List<ImportTemplatePropertyNode> _propertyNodeList;

        /// <summary>
        /// 命令节点列表
        /// </summary>
        public IList<ImportTemplatePropertyNode> PropertyNodeList
        {
            get
            {
                if (_propertyNodeList == null)
                {
                    _propertyNodeList = new List<ImportTemplatePropertyNode>();
                }

                return _propertyNodeList;
            }
        }

        private List<ImportTemplateColumnNode> _columnNodeList;

        /// <summary>
        /// 列模版节点列表
        /// </summary>
        public IList<ImportTemplateColumnNode> ColumnNodeList
        {
            get
            {
                if (_columnNodeList == null)
                {
                    _columnNodeList = new List<ImportTemplateColumnNode>();
                }

                return _columnNodeList;
            }
        }

        private List<ImportTemplateCommandNode> _commandNodeList;

        /// <summary>
        /// 命令节点列表
        /// </summary>
        public IList<ImportTemplateCommandNode> CommandNodeList
        {
            get
            {
                if (_commandNodeList == null)
                {
                    _commandNodeList = new List<ImportTemplateCommandNode>();
                }

                return _commandNodeList;
            }
        }

        #endregion

        #region 构造函数

        public ImportTemplateGroup(string name)
        {
            _name = name;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取公用数据列节点列表
        /// </summary>
        /// <returns></returns>
        public IList<ImportTemplateColumnNode> GetCommonColumnNodeList()
        {
            IEnumerable<ImportTemplateColumnNode> colNodes = ColumnNodeList.Where(tent => tent.IsCommon);

            return colNodes.ToList();
        }

        /// <summary>
        /// 获取普通数据列节点列表
        /// </summary>
        /// <returns></returns>
        public IList<ImportTemplateColumnNode> GetOrdinaireColumnNodeList()
        {
            IEnumerable<ImportTemplateColumnNode> colNodes = ColumnNodeList.Where(tent => !tent.IsCommon);

            return colNodes.ToList();
        }

        /// <summary>
        /// 列节点字典
        /// </summary>
        public Dictionary<string, ImportTemplateColumnNode> GetColumnNodeDict()
        {
            Dictionary<string, ImportTemplateColumnNode> dict = new Dictionary<string, ImportTemplateColumnNode>();

            foreach (ImportTemplateColumnNode tnode in ColumnNodeList)
            {
                dict.Add(tnode.ColumnName, tnode);
            }

            return dict;
        }

        /// <summary>
        /// 获取DataTable架构
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableSchema()
        {
            DataTable tdt = new DataTable();

            foreach (ImportTemplateColumnNode tcnode in ColumnNodeList)
            {
                tdt.Columns.Add(tcnode.ColumnName);
            }

            return tdt;
        }

        /// <summary>
        /// 获取ID生成器
        /// </summary>
        /// <returns></returns>
        public IIdentifierGenerator GetIDGenerator()
        {
            return new AimIdentifierGenerator();
        }

        #endregion
    }

    /// <summary>
    /// 默认模版组
    /// </summary>
    public class DefaultImportTemplateGroup : ImportTemplateGroup
    {
        #region 构造函数

        public DefaultImportTemplateGroup()
            : base(ImportTemplateGroup.DEFULAT_GROUP_NAME)
        {
        }

        #endregion
    }

    /// <summary>
    /// 导入模版节点
    /// </summary>
    public class ImportTemplateNode : TemplateNode
    {
        #region 成员属性

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ImportTemplateNode()
        {
            Name = String.Empty;
        }

        #endregion
    }

    /// <summary>
    /// 导入模版数据节点
    /// </summary>
    public class ImportTemplateColumnNode : ImportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否公共列
        /// </summary>
        public bool IsCommon
        {
            get;
            set;
        }

        /*
        /// <summary>
        /// 是否检查合法性操作
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }

        /// <summary>
        /// 合法性检查操作
        /// </summary>
        public string CheckExpression
        {
            get;
            set;
        }
         * */

        /// <summary>
        /// 行值位置
        /// </summary>
        public int? ValueRowIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 列值位置
        /// </summary>
        public int? ValueColumnIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格默认值
        /// </summary>
        public object DefaultValue
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ImportTemplateColumnNode()
        {
            Name = String.Empty;
            IsCommon = false;
            // IsCheck = true;
        }

        #endregion
    }

    /// <summary>
    /// 导入模版命令节点
    /// </summary>
    public class ImportTemplateCommandNode : ImportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 命令编码
        /// </summary>
        public ImportTemplateCommandCode CommandCode
        {
            get;
            set;
        }

        /// <summary>
        /// 命令行位置
        /// </summary>
        public int RowIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 命令列位置
        /// </summary>
        public int ColumnIndex
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ImportTemplateCommandNode()
        {
            Name = String.Empty;
        }

        #endregion
    }

    /// <summary>
    /// 导入模版属性节点
    /// </summary>
    public class ImportTemplatePropertyNode : ImportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 导入目标表
        /// </summary>
        public string Target
        {
            get;
            set;
        }

        /// <summary>
        /// 是否检查合法性操作
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }

        /// <summary>
        /// 是否作事务处理
        /// </summary>
        public bool IsTransaction
        {
            get;
            set;
        }

        /// <summary>
        /// 一次导入操作大小(默认100)
        /// </summary>
        public int? BlockSize
        {
            get;
            set;
        }

        /// <summary>
        /// 标识生成器
        /// </summary>
        public string IDGenerator
        {
            get;
            set;
        }

        /// <summary>
        /// ID生成器
        /// </summary>
        [JsonIgnore]
        public IIdentifierGenerator IdentifierGenerator
        {
            get
            {
                if (String.IsNullOrEmpty(IDGenerator))
                {
                    return new Aim.Data.AimIdentifierGenerator();
                }

                // Castle.ActiveRecord.PrimaryKeyType.可以由PrimaryKeyType获取IDGenerator

                return null;
            }
        }

        #endregion

        #region 构造函数

        public ImportTemplatePropertyNode()
        {
            Name = String.Empty;
            IsCheck = false;
            IsTransaction = true;
            BlockSize = 100;
        }

        #endregion
    }
}
