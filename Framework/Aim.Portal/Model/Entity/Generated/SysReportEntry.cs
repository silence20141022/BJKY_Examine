// Business class SysReportEntry generated from SysReportEntry
// Creator: Ray
// Created Date: [2007-05-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    [ActiveRecord("SysReportEntry")]
    public partial class SysReportEntry : EntityBase<SysReportEntry>, INotifyPropertyChanged
    {

        #region Property_Names

        public static string Prop_Id = "Id";
        public static string Prop_PrjId = "PrjId";
        public static string Prop_PrjCode = "PrjCode";
        public static string Prop_PrjName = "PrjName";
        public static string Prop_Module = "Module";
        public static string Prop_Type = "Type";
        public static string Prop_Name = "Name";
        public static string Prop_ReportKey = "ReportKey";
        public static string Prop_URL = "URL";
        public static string Prop_SortIndex = "SortIndex";
        public static string Prop_AllowUserIds = "AllowUserIds";
        public static string Prop_AllowUserNames = "AllowUserNames";
        public static string Prop_CreateId = "CreateId";
        public static string Prop_CreateName = "CreateName";
        public static string Prop_CreateTime = "CreateTime";
        public static string Prop_ModifyTime = "ModifyTime";

        #endregion

        #region Private_Variables

        private string _id;
        private string _prjId;
        private string _prjCode;
        private string _prjName;
        private string _module;
        private string _type;
        private string _name;
        private string _reportKey;
        private string _uRL;
        private int? _sortIndex;
        private string _allowUserIds;
        private string _allowUserNames;
        private string _createId;
        private string _createName;
        private DateTime? _createTime;
        private DateTime? _modifyTime;


        #endregion

        #region Constructors

        public SysReportEntry()
        {
        }

        public SysReportEntry(
            string p_id,
            string p_prjId,
            string p_prjCode,
            string p_prjName,
            string p_module,
            string p_type,
            string p_name,
            string p_reportKey,
            string p_uRL,
            int? p_sortIndex,
            string p_allowUserIds,
            string p_allowUserNames,
            string p_createId,
            string p_createName,
            DateTime? p_createTime,
            DateTime? p_modifyTime)
        {
            _id = p_id;
            _prjId = p_prjId;
            _prjCode = p_prjCode;
            _prjName = p_prjName;
            _module = p_module;
            _type = p_type;
            _name = p_name;
            _reportKey = p_reportKey;
            _uRL = p_uRL;
            _sortIndex = p_sortIndex;
            _allowUserIds = p_allowUserIds;
            _allowUserNames = p_allowUserNames;
            _createId = p_createId;
            _createName = p_createName;
            _createTime = p_createTime;
            _modifyTime = p_modifyTime;
        }

        #endregion

        #region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string Id
        {
            get { return _id; }
        }

        [Property("PrjId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
        public string PrjId
        {
            get { return _prjId; }
            set
            {
                if ((_prjId == null) || (value == null) || (!value.Equals(_prjId)))
                {
                    _prjId = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_PrjId);
                }
            }
        }

        [Property("PrjCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string PrjCode
        {
            get { return _prjCode; }
            set
            {
                if ((_prjCode == null) || (value == null) || (!value.Equals(_prjCode)))
                {
                    _prjCode = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_PrjCode);
                }
            }
        }

        [Property("PrjName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string PrjName
        {
            get { return _prjName; }
            set
            {
                if ((_prjName == null) || (value == null) || (!value.Equals(_prjName)))
                {
                    _prjName = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_PrjName);
                }
            }
        }

        [Property("Module", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Module
        {
            get { return _module; }
            set
            {
                if ((_module == null) || (value == null) || (!value.Equals(_module)))
                {
                    _module = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_Module);
                }
            }
        }

        [Property("Type", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
        public string Type
        {
            get { return _type; }
            set
            {
                if ((_type == null) || (value == null) || (!value.Equals(_type)))
                {
                    _type = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_Type);
                }
            }
        }

        [Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if ((_name == null) || (value == null) || (!value.Equals(_name)))
                {
                    _name = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_Name);
                }
            }
        }

        [Property("ReportKey", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string ReportKey
        {
            get { return _reportKey; }
            set
            {
                if ((_reportKey == null) || (value == null) || (!value.Equals(_reportKey)))
                {
                    _reportKey = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_ReportKey);
                }
            }
        }

        [Property("URL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string URL
        {
            get { return _uRL; }
            set
            {
                if ((_uRL == null) || (value == null) || (!value.Equals(_uRL)))
                {
                    _uRL = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_URL);
                }
            }
        }

        [Property("SortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? SortIndex
        {
            get { return _sortIndex; }
            set
            {
                if (value != _sortIndex)
                {
                    _sortIndex = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_SortIndex);
                }
            }
        }

        [Property("AllowUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
        public string AllowUserIds
        {
            get { return _allowUserIds; }
            set
            {
                if ((_allowUserIds == null) || (value == null) || (!value.Equals(_allowUserIds)))
                {
                    _allowUserIds = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_AllowUserIds);
                }
            }
        }

        [Property("AllowUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
        public string AllowUserNames
        {
            get { return _allowUserNames; }
            set
            {
                if ((_allowUserNames == null) || (value == null) || (!value.Equals(_allowUserNames)))
                {
                    _allowUserNames = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_AllowUserNames);
                }
            }
        }

        [Property("CreateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
        public string CreateId
        {
            get { return _createId; }
            set
            {
                if ((_createId == null) || (value == null) || (!value.Equals(_createId)))
                {
                    _createId = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_CreateId);
                }
            }
        }

        [Property("CreateName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string CreateName
        {
            get { return _createName; }
            set
            {
                if ((_createName == null) || (value == null) || (!value.Equals(_createName)))
                {
                    _createName = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_CreateName);
                }
            }
        }

        [Property("CreateTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? CreateTime
        {
            get { return _createTime; }
            set
            {
                if (value != _createTime)
                {
                    _createTime = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_CreateTime);
                }
            }
        }

        [Property("ModifyTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public DateTime? ModifyTime
        {
            get { return _modifyTime; }
            set
            {
                if (value != _modifyTime)
                {
                    _modifyTime = value;
                    NotifyPropertyChanged(SysReportEntry.Prop_ModifyTime);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler localPropertyChanged = PropertyChanged;
            if (localPropertyChanged != null)
            {
                localPropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

    } // SysReportEntry
}

