// Business class DynamicAuth generated from DynamicAuth
// Creator: Ray
// Created Date: [2000-05-22]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("DynamicAuth")]
	public partial class DynamicAuth : EditSensitiveEntityBase<DynamicAuth> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DynamicAuthID = "DynamicAuthID";
		public static string Prop_Name = "Name";
		public static string Prop_CatalogCode = "CatalogCode";
		public static string Prop_ParentID = "ParentID";
		public static string Prop_Path = "Path";
		public static string Prop_PathLevel = "PathLevel";
		public static string Prop_Data = "Data";
		public static string Prop_EditStatus = "EditStatus";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_Tag = "Tag";
		public static string Prop_Description = "Description";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _dynamicauthid;
		private string _name;
		private string _catalogCode;
		private string _parentID;
		private string _path;
		private int? _pathLevel;
		private string _data;
		private string _editStatus;
		private int? _sortIndex;
		private string _tag;
		private string _description;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;

		#endregion

		#region Constructors

		public DynamicAuth()
		{
		}

		public DynamicAuth(
			string p_dynamicauthid,
			string p_name,
			string p_catalogCode,
			string p_parentID,
			string p_path,
			int? p_pathLevel,
			string p_data,
			string p_editStatus,
			int? p_sortIndex,
			string p_tag,
			string p_description,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_dynamicauthid = p_dynamicauthid;
			_name = p_name;
			_catalogCode = p_catalogCode;
			_parentID = p_parentID;
			_path = p_path;
			_pathLevel = p_pathLevel;
			_data = p_data;
			_editStatus = p_editStatus;
			_sortIndex = p_sortIndex;
			_tag = p_tag;
			_description = p_description;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("DynamicAuthID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string DynamicAuthID
		{
			get { return _dynamicauthid; }
		}

        [Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 150)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
					_name = value;
					NotifyPropertyChanged(DynamicAuth.Prop_Name);
				}
			}
		}

        [Property("CatalogCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 150)]
		public string CatalogCode
		{
			get { return _catalogCode; }
			set
			{
				if ((_catalogCode == null) || (value == null) || (!value.Equals(_catalogCode)))
				{
					_catalogCode = value;
					NotifyPropertyChanged(DynamicAuth.Prop_CatalogCode);
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
					NotifyPropertyChanged(DynamicAuth.Prop_ParentID);
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
					NotifyPropertyChanged(DynamicAuth.Prop_Path);
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
					NotifyPropertyChanged(DynamicAuth.Prop_PathLevel);
				}
			}
		}

        [Property("Data", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 150)]
		public string Data
		{
			get { return _data; }
			set
			{
				if ((_data == null) || (value == null) || (!value.Equals(_data)))
				{
					_data = value;
					NotifyPropertyChanged(DynamicAuth.Prop_Data);
				}
			}
		}

		[Property("EditStatus", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public override string EditStatus
		{
			get { return _editStatus; }
			set
			{
				if ((_editStatus == null) || (value == null) || (!value.Equals(_editStatus)))
				{
					_editStatus = value;
					NotifyPropertyChanged(DynamicAuth.Prop_EditStatus);
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
					NotifyPropertyChanged(DynamicAuth.Prop_SortIndex);
				}
			}
		}

		[Property("Tag", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Tag
		{
			get { return _tag; }
			set
			{
				if ((_tag == null) || (value == null) || (!value.Equals(_tag)))
				{
					_tag = value;
					NotifyPropertyChanged(DynamicAuth.Prop_Tag);
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
					NotifyPropertyChanged(DynamicAuth.Prop_Description);
				}
			}
		}

		[Property("CreaterID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CreaterID
		{
			get { return _createrID; }
			set
			{
				if ((_createrID == null) || (value == null) || (!value.Equals(_createrID)))
				{
					_createrID = value;
					NotifyPropertyChanged(DynamicAuth.Prop_CreaterID);
				}
			}
		}

		[Property("CreaterName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CreaterName
		{
			get { return _createrName; }
			set
			{
				if ((_createrName == null) || (value == null) || (!value.Equals(_createrName)))
				{
					_createrName = value;
					NotifyPropertyChanged(DynamicAuth.Prop_CreaterName);
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
					NotifyPropertyChanged(DynamicAuth.Prop_LastModifiedDate);
				}
			}
		}

		[Property("CreatedDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreatedDate
		{
			get { return _createdDate; }
			set
			{
				if (value != _createdDate)
				{
					_createdDate = value;
					NotifyPropertyChanged(DynamicAuth.Prop_CreatedDate);
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

	} // DynamicAuth
}

