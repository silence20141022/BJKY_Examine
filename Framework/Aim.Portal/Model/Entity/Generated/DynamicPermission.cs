// Business class DynamicPermission generated from DynamicPermission
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
	[ActiveRecord("DynamicPermission")]
    public partial class DynamicPermission : EditSensitiveEntityBase<DynamicPermission>, INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DynamicPermissionID = "DynamicPermissionID";
		public static string Prop_Name = "Name";
		public static string Prop_CatalogCode = "CatalogCode";
        public static string Prop_AuthID = "AuthID";
        public static string Prop_AuthCatalogCode = "AuthCatalogCode";
		public static string Prop_Operation = "Operation";
		public static string Prop_Data = "Data";
		public static string Prop_Tag = "Tag";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_EditStatus = "EditStatus";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _dynamicpermissionid;
		private string _name;
        private string _catalogCode;
        private string _authID;
        private string _authCatalogCode;
		private string _operation;
		private string _data;
		private string _tag;
		private int? _sortIndex;
		private string _editStatus;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;


		#endregion

		#region Constructors

		public DynamicPermission()
		{
		}

		public DynamicPermission(
			string p_dynamicpermissionid,
			string p_name,
			string p_catalogCode,
            string p_authID,
            string p_authCatalogCode,
			string p_operation,
			string p_data,
			string p_tag,
			int? p_sortIndex,
			string p_editStatus,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_dynamicpermissionid = p_dynamicpermissionid;
			_name = p_name;
			_catalogCode = p_catalogCode;
            _authID = p_authID;
			_operation = p_operation;
            _authCatalogCode = p_authCatalogCode;
			_data = p_data;
			_tag = p_tag;
			_sortIndex = p_sortIndex;
			_editStatus = p_editStatus;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("DynamicPermissionID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string DynamicPermissionID
		{
			get { return _dynamicpermissionid; }
            set
            {
                if ((_dynamicpermissionid == null) || (value == null) || (!value.Equals(_dynamicpermissionid)))
                {
                    _dynamicpermissionid = value;
                    NotifyPropertyChanged(DynamicPermission.Prop_DynamicPermissionID);
                }
            }
		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
					_name = value;
					NotifyPropertyChanged(DynamicPermission.Prop_Name);
				}
			}
		}

        [Property("CatalogCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
		public string CatalogCode
		{
			get { return _catalogCode; }
			set
			{
				if ((_catalogCode == null) || (value == null) || (!value.Equals(_catalogCode)))
				{
					_catalogCode = value;
					NotifyPropertyChanged(DynamicPermission.Prop_CatalogCode);
				}
			}
        }

        [Property("AuthID", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 36)]
        public string AuthID
        {
            get { return _authID; }
            set
            {
                if ((_authID == null) || (value == null) || (!value.Equals(_authID)))
                {
                    _authID = value;
                    NotifyPropertyChanged(DynamicPermission.Prop_AuthID);
                }
            }
        }

        [Property("AuthCatalogCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
        public string AuthCatalogCode
        {
            get { return _authCatalogCode; }
            set
            {
                if ((_authCatalogCode == null) || (value == null) || (!value.Equals(_authCatalogCode)))
                {
                    _authCatalogCode = value;
                    NotifyPropertyChanged(DynamicPermission.Prop_AuthCatalogCode);
                }
            }
        }

		[Property("Operation", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Operation
		{
			get { return _operation; }
			set
			{
				if ((_operation == null) || (value == null) || (!value.Equals(_operation)))
				{
					_operation = value;
					NotifyPropertyChanged(DynamicPermission.Prop_Operation);
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
					NotifyPropertyChanged(DynamicPermission.Prop_Data);
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
					NotifyPropertyChanged(DynamicPermission.Prop_Tag);
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
					NotifyPropertyChanged(DynamicPermission.Prop_SortIndex);
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
					NotifyPropertyChanged(DynamicPermission.Prop_EditStatus);
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
					NotifyPropertyChanged(DynamicPermission.Prop_CreaterID);
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
					NotifyPropertyChanged(DynamicPermission.Prop_CreaterName);
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
					NotifyPropertyChanged(DynamicPermission.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(DynamicPermission.Prop_CreatedDate);
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

	} // DynamicPermission
}

