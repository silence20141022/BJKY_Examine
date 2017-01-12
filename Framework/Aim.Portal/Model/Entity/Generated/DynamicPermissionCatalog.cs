// Business class DynamicPermissionCatalog generated from DynamicPermissionCatalog
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
	[ActiveRecord("DynamicPermissionCatalog")]
    public partial class DynamicPermissionCatalog : EditSensitiveEntityBase<DynamicPermissionCatalog>, INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DynamicPermissionCatalogID = "DynamicPermissionCatalogID";
		public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_EditStatus = "EditStatus";
		public static string Prop_PositiveGrantUrl = "PositiveGrantUrl";
		public static string Prop_OppositeGrantUrl = "OppositeGrantUrl";
		public static string Prop_DefaultGrantType = "DefaultGrantType";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_Description = "Description";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _dynamicpermissioncatalogid;
		private string _code;
		private string _name;
		private string _editStatus;
		private string _positiveGrantUrl;
		private string _oppositeGrantUrl;
		private string _defaultGrantType;
		private int? _sortIndex;
		private string _description;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;


		#endregion

		#region Constructors

		public DynamicPermissionCatalog()
		{
		}

		public DynamicPermissionCatalog(
			string p_dynamicpermissioncatalogid,
			string p_code,
			string p_name,
			string p_editStatus,
			string p_positiveGrantUrl,
			string p_oppositeGrantUrl,
			string p_defaultGrantType,
			int? p_sortIndex,
			string p_description,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_dynamicpermissioncatalogid = p_dynamicpermissioncatalogid;
			_code = p_code;
			_name = p_name;
			_editStatus = p_editStatus;
			_positiveGrantUrl = p_positiveGrantUrl;
			_oppositeGrantUrl = p_oppositeGrantUrl;
			_defaultGrantType = p_defaultGrantType;
			_sortIndex = p_sortIndex;
			_description = p_description;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("DynamicPermissionCatalogID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string DynamicPermissionCatalogID
		{
			get { return _dynamicpermissioncatalogid; }
		}

        [Property("Code", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
		public string Code
		{
			get { return _code; }
			set
			{
				if ((_code == null) || (value == null) || (!value.Equals(_code)))
				{
					_code = value;
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_Code);
				}
			}
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_Name);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_EditStatus);
				}
			}
		}

		[Property("PositiveGrantUrl", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string PositiveGrantUrl
		{
			get { return _positiveGrantUrl; }
			set
			{
				if ((_positiveGrantUrl == null) || (value == null) || (!value.Equals(_positiveGrantUrl)))
				{
					_positiveGrantUrl = value;
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_PositiveGrantUrl);
				}
			}
		}

		[Property("OppositeGrantUrl", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string OppositeGrantUrl
		{
			get { return _oppositeGrantUrl; }
			set
			{
				if ((_oppositeGrantUrl == null) || (value == null) || (!value.Equals(_oppositeGrantUrl)))
				{
					_oppositeGrantUrl = value;
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_OppositeGrantUrl);
				}
			}
		}

		[Property("DefaultGrantType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string DefaultGrantType
		{
			get { return _defaultGrantType; }
			set
			{
				if ((_defaultGrantType == null) || (value == null) || (!value.Equals(_defaultGrantType)))
				{
					_defaultGrantType = value;
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_DefaultGrantType);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_SortIndex);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_Description);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_CreaterID);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_CreaterName);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(DynamicPermissionCatalog.Prop_CreatedDate);
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

	} // DynamicPermissionCatalog
}

