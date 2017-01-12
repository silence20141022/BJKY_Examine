// Business class DynamicAuthCatalog generated from DynamicAuthCatalog
// Creator: Ray
// Created Date: [2000-05-23]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("DynamicAuthCatalog")]
    public partial class DynamicAuthCatalog : EditSensitiveEntityBase<DynamicAuthCatalog>, INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DynamicAuthCatalogID = "DynamicAuthCatalogID";
		public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_EditStatus = "EditStatus";
		public static string Prop_AllowGrantCatalogCode = "AllowGrantCatalogCode";
		public static string Prop_AllowGrantCatalogName = "AllowGrantCatalogName";
		public static string Prop_CustomGrantUrl = "CustomGrantUrl";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_Description = "Description";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _dynamicauthcatalogid;
		private string _code;
		private string _name;
		private string _editStatus;
		private string _allowGrantCatalogCode;
		private string _allowGrantCatalogName;
		private string _customGrantUrl;
		private int? _sortIndex;
		private string _description;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;

        private IList<DynamicOperation> _DynamicOperation = new List<DynamicOperation>();

		#endregion

		#region Constructors

		public DynamicAuthCatalog()
		{
		}

		public DynamicAuthCatalog(
			string p_dynamicauthcatalogid,
			string p_code,
			string p_name,
			string p_editStatus,
			string p_allowGrantCatalogCode,
			string p_allowGrantCatalogName,
			string p_customGrantUrl,
			int? p_sortIndex,
			string p_description,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_dynamicauthcatalogid = p_dynamicauthcatalogid;
			_code = p_code;
			_name = p_name;
			_editStatus = p_editStatus;
			_allowGrantCatalogCode = p_allowGrantCatalogCode;
			_allowGrantCatalogName = p_allowGrantCatalogName;
			_customGrantUrl = p_customGrantUrl;
			_sortIndex = p_sortIndex;
			_description = p_description;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("DynamicAuthCatalogID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string DynamicAuthCatalogID
		{
			get { return _dynamicauthcatalogid; }
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_Code);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_Name);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_EditStatus);
				}
			}
		}

		[Property("AllowGrantCatalogCode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string AllowGrantCatalogCode
		{
			get { return _allowGrantCatalogCode; }
			set
			{
				if ((_allowGrantCatalogCode == null) || (value == null) || (!value.Equals(_allowGrantCatalogCode)))
				{
					_allowGrantCatalogCode = value;
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_AllowGrantCatalogCode);
				}
			}
		}

		[Property("AllowGrantCatalogName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string AllowGrantCatalogName
		{
			get { return _allowGrantCatalogName; }
			set
			{
				if ((_allowGrantCatalogName == null) || (value == null) || (!value.Equals(_allowGrantCatalogName)))
				{
					_allowGrantCatalogName = value;
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_AllowGrantCatalogName);
				}
			}
		}

		[Property("CustomGrantUrl", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CustomGrantUrl
		{
			get { return _customGrantUrl; }
			set
			{
				if ((_customGrantUrl == null) || (value == null) || (!value.Equals(_customGrantUrl)))
				{
					_customGrantUrl = value;
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_CustomGrantUrl);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_SortIndex);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_Description);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_CreaterID);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_CreaterName);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(DynamicAuthCatalog.Prop_CreatedDate);
				}
			}
		}

		[JsonIgnore]
		[HasMany(typeof(DynamicOperation), Table="DynamicOperation", ColumnKey="CatalogID", Cascade = ManyRelationCascadeEnum.All)]
		public IList<DynamicOperation> DynamicOperation
		{
			get { return _DynamicOperation; }
			set { _DynamicOperation = value; }
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

	} // DynamicAuthCatalog
}

