// Business class SysModule generated from SysModule
// Creator: Ray
// Created Date: [2010-04-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SysModule")]
	public partial class SysModule : EntityBase<SysModule> , INotifyPropertyChanged 	
	{

		#region Property_Names

        public static string Prop_ModuleID = "ModuleID";
        public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_Type = "Type";
		public static string Prop_ApplicationID = "ApplicationID";
        public static string Prop_ParentID = "ParentID";
		public static string Prop_Path = "Path";
		public static string Prop_PathLevel = "PathLevel";
		public static string Prop_Url = "Url";
		public static string Prop_Icon = "Icon";
		public static string Prop_Description = "Description";
		public static string Prop_Status = "Status";
		public static string Prop_IsSystem = "IsSystem";
        public static string Prop_SortIndex = "SortIndex";
        public static string Prop_IsEntityPage = "IsEntityPage";
        public static string Prop_IsQuickSearch = "IsQuickSearch";
        public static string Prop_IsQuickCreate = "IsQuickCreate";
        public static string Prop_IsRecyclable = "IsRecyclable";
        public static string Prop_EditPageUrl = "EditPageUrl";
        public static string Prop_AfterEditScript = "AfterEditScript";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreateDate = "CreateDate";

		#endregion

		#region Private_Variables

        private string _moduleid;
        private string _code;
		private string _name;
		private int? _type;
		private string _applicationID;
		private string _parentID;
		private string _path;
		private int? _pathLevel;
		private string _url;
		private string _icon;
		private string _description;
		private int? _status;
        private int? _isSystem;
        private int? _sortIndex;
        private bool? _isEntityPage;
        private bool? _isQuickSearch;
        private bool? _isQuickCreate;
        private bool? _isRecyclable;
        private string _editPageUrl;
        private string _afterEditScript;
		private DateTime? _lastModifiedDate;
		private DateTime? _createDate;

		#endregion

		#region Constructors

		public SysModule()
		{
		}

		public SysModule(
            string p_moduleid,
            string p_code,
			string p_name,
			int? p_type,
			string p_applicationID,
			string p_parentID,
			string p_path,
			int? p_pathLevel,
			string p_url,
			string p_icon,
			string p_description,
			int? p_status,
			int? p_isSystem,
            int? p_sortIndex,
            bool? p_isEntityPage,
            bool? p_isQuickSearch,
            bool? p_isQuickCreate,
            bool? p_isRecyclable,
            string p_editPageUrl,
            string p_afterEditScript,
			DateTime? p_lastModifiedDate,
			DateTime? p_createDate)
		{
			_moduleid = p_moduleid;
			_name = p_name;
            _code = p_code;
			_type = p_type;
			_applicationID = p_applicationID;
			_parentID = p_parentID;
			_path = p_path;
			_pathLevel = p_pathLevel;
			_url = p_url;
			_icon = p_icon;
			_description = p_description;
			_status = p_status;
			_isSystem = p_isSystem;
            _sortIndex = p_sortIndex;
            _isEntityPage = p_isEntityPage;
            _isQuickSearch = p_isQuickSearch;
            _isQuickCreate = p_isQuickCreate;
            _isRecyclable = p_isRecyclable;
            _editPageUrl = p_editPageUrl;
            _afterEditScript = p_afterEditScript;
			_lastModifiedDate = p_lastModifiedDate;
			_createDate = p_createDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("ModuleID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ModuleID
		{
			get { return _moduleid; }
        }

        [Property("Code", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Code
        {
            get { return _code; }
            set
            {
                if ((_code == null) || (value == null) || (!value.Equals(_code)))
                {
                    _code = value;
                    NotifyPropertyChanged(SysModule.Prop_Code);
                }
            }
        }

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
					_name = value;
					NotifyPropertyChanged(SysModule.Prop_Name);
				}
			}
		}

		[Property("Type", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? Type
		{
			get { return _type; }
			set
			{
				if (value != _type)
				{
					_type = value;
					NotifyPropertyChanged(SysModule.Prop_Type);
				}
			}
		}

		[Property("ApplicationID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ApplicationID
		{
			get { return _applicationID; }
			set
			{
				if ((_applicationID == null) || (value == null) || (!value.Equals(_applicationID)))
				{
					_applicationID = value;
					NotifyPropertyChanged(SysModule.Prop_ApplicationID);
				}
			}
        }

        [Property("ParentID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
        public string ParentID
        {
            get { return _parentID; }
            set
            {
                if ((_parentID == null) || (value == null) || (!value.Equals(_parentID)))
                {
                    _parentID = value;
                    NotifyPropertyChanged(SysModule.Prop_ParentID);
                }
            }
        }

		[Property("Path", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string Path
		{
			get { return _path; }
			set
			{
				if ((_path == null) || (value == null) || (!value.Equals(_path)))
				{
					_path = value;
					NotifyPropertyChanged(SysModule.Prop_Path);
				}
			}
		}

		[Property("PathLevel", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? PathLevel
		{
			get { return _pathLevel; }
			set
			{
				if (value != _pathLevel)
				{
					_pathLevel = value;
					NotifyPropertyChanged(SysModule.Prop_PathLevel);
				}
			}
		}

		[Property("Url", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Url
		{
			get { return _url; }
			set
			{
				if ((_url == null) || (value == null) || (!value.Equals(_url)))
				{
					_url = value;
					NotifyPropertyChanged(SysModule.Prop_Url);
				}
			}
		}

		[Property("Icon", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Icon
		{
			get { return _icon; }
			set
			{
				if ((_icon == null) || (value == null) || (!value.Equals(_icon)))
				{
					_icon = value;
					NotifyPropertyChanged(SysModule.Prop_Icon);
				}
			}
		}

		[Property("Description", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Description
		{
			get { return _description; }
			set
			{
				if ((_description == null) || (value == null) || (!value.Equals(_description)))
				{
					_description = value;
					NotifyPropertyChanged(SysModule.Prop_Description);
				}
			}
		}

		[Property("Status", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? Status
		{
			get { return _status; }
			set
			{
				if (value != _status)
				{
					_status = value;
					NotifyPropertyChanged(SysModule.Prop_Status);
				}
			}
		}

		[Property("IsSystem", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? IsSystem
		{
			get { return _isSystem; }
			set
			{
				if (value != _isSystem)
				{
					_isSystem = value;
					NotifyPropertyChanged(SysModule.Prop_IsSystem);
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
					NotifyPropertyChanged(SysModule.Prop_SortIndex);
				}
			}
        }

        [Property("IsEntityPage", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool? IsEntityPage
        {
            get { return _isEntityPage; }
            set
            {
                if (value != _isEntityPage)
                {
                    object oldValue = _isEntityPage;
                    _isEntityPage = value;
                    RaisePropertyChanged(SysModule.Prop_IsEntityPage, oldValue, value);
                }
            }
        }

        [Property("IsQuickSearch", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool? IsQuickSearch
        {
            get { return _isQuickSearch; }
            set
            {
                if (value != _isQuickSearch)
                {
                    object oldValue = _isQuickSearch;
                    _isQuickSearch = value;
                    RaisePropertyChanged(SysModule.Prop_IsQuickSearch, oldValue, value);
                }
            }
        }

        [Property("IsQuickCreate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool? IsQuickCreate
        {
            get { return _isQuickCreate; }
            set
            {
                if (value != _isQuickCreate)
                {
                    object oldValue = _isQuickCreate;
                    _isQuickCreate = value;
                    RaisePropertyChanged(SysModule.Prop_IsQuickCreate, oldValue, value);
                }
            }
        }

        [Property("IsRecyclable", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool? IsRecyclable
        {
            get { return _isRecyclable; }
            set
            {
                if (value != _isRecyclable)
                {
                    object oldValue = _isRecyclable;
                    _isRecyclable = value;
                    RaisePropertyChanged(SysModule.Prop_IsRecyclable, oldValue, value);
                }
            }
        }

        [Property("EditPageUrl", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string EditPageUrl
        {
            get { return _editPageUrl; }
            set
            {
                if ((_editPageUrl == null) || (value == null) || (!value.Equals(_editPageUrl)))
                {
                    object oldValue = _editPageUrl;
                    _editPageUrl = value;
                    RaisePropertyChanged(SysModule.Prop_EditPageUrl, oldValue, value);
                }
            }
        }

        [Property("AfterEditScript", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
        public string AfterEditScript
        {
            get { return _afterEditScript; }
            set
            {
                if ((_afterEditScript == null) || (value == null) || (!value.Equals(_afterEditScript)))
                {
                    object oldValue = _afterEditScript;
                    _afterEditScript = value;
                    RaisePropertyChanged(SysModule.Prop_AfterEditScript, oldValue, value);
                }
            }
        }

		[Property("LastModifiedDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? LastModifiedDate
		{
			get { return _lastModifiedDate; }
			set
			{
				if (value != _lastModifiedDate)
				{
					_lastModifiedDate = value;
					NotifyPropertyChanged(SysModule.Prop_LastModifiedDate);
				}
			}
		}

		[Property("CreateDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreateDate
		{
			get { return _createDate; }
			set
			{
				if (value != _createDate)
				{
					_createDate = value;
					NotifyPropertyChanged(SysModule.Prop_CreateDate);
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

	} // SysModule
}

