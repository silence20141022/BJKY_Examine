// Business class DynamicOperation generated from DynamicOperation
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
	[ActiveRecord("DynamicOperation")]
    public partial class DynamicOperation : EditSensitiveEntityBase<DynamicOperation>, INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DynamicOperationID = "DynamicOperationID";
		public static string Prop_CatalogID = "CatalogID";
		public static string Prop_Name = "Name";
        public static string Prop_Code = "Code";
        public static string Prop_EditStatus = "EditStatus";
        public static string Prop_IsDeafult = "IsDeafult";
        public static string Prop_Disabled = "Disabled";
		public static string Prop_Tag = "Tag";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_Description = "Description";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_CreatedDate = "CreatedDate";
		public static string Prop_LastModifiedDate = "LastModifiedDate";

		#endregion

		#region Private_Variables

		private string _dynamicoperationid;
		private string _catalogID;
		private string _name;
        private string _code;
        private string _editStatus;
        private bool? _isDeafult;
        private bool? _disabled;
		private string _tag;
		private int? _sortIndex;
		private string _description;
		private string _createrID;
		private string _createrName;
		private DateTime? _createdDate;
		private DateTime? _lastModifiedDate;


		#endregion

		#region Constructors

		public DynamicOperation()
		{
		}

		public DynamicOperation(
			string p_dynamicoperationid,
            string p_catalogID,
			string p_name,
            string p_code,
            string p_editStatus,
			bool? p_isDeafult,
            bool? p_disabled,
			string p_tag,
			int? p_sortIndex,
			string p_description,
			string p_createrID,
			string p_createrName,
			DateTime? p_createdDate,
			DateTime? p_lastModifiedDate)
		{
			_dynamicoperationid = p_dynamicoperationid;
			_catalogID = p_catalogID;
			_name = p_name;
            _code = p_code;
            _editStatus = p_editStatus;
            _isDeafult = p_isDeafult;
            _disabled = p_disabled;
			_tag = p_tag;
			_sortIndex = p_sortIndex;
			_description = p_description;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_createdDate = p_createdDate;
			_lastModifiedDate = p_lastModifiedDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("DynamicOperationID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string DynamicOperationID
		{
			get { return _dynamicoperationid; }
		}

        [Property("CatalogID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36, NotNull = true)]
		public string CatalogID
		{
			get { return _catalogID; }
			set
            {
                if ((_catalogID == null) || (value == null) || (!value.Equals(_catalogID)))
                {
                    _catalogID = value;
                    NotifyPropertyChanged(DynamicOperation.Prop_CatalogID);
                }
			}
		}

        [Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50, NotNull = true)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
					_name = value;
					NotifyPropertyChanged(DynamicOperation.Prop_Name);
				}
			}
		}

        [Property("Code", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50, NotNull = true)]
		public string Code
		{
			get { return _code; }
			set
			{
				if ((_code == null) || (value == null) || (!value.Equals(_code)))
				{
					_code = value;
					NotifyPropertyChanged(DynamicOperation.Prop_Code);
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

		[Property("IsDeafult", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public bool? IsDeafult
		{
			get { return _isDeafult; }
			set
			{
				if (value != _isDeafult)
				{
					_isDeafult = value;
					NotifyPropertyChanged(DynamicOperation.Prop_IsDeafult);
				}
			}
        }

        [Property("Disabled", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool? Disabled
        {
            get { return _disabled; }
            set
            {
                if (value != _disabled)
                {
                    _disabled = value;
                    NotifyPropertyChanged(DynamicOperation.Prop_Disabled);
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
					NotifyPropertyChanged(DynamicOperation.Prop_Tag);
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
					NotifyPropertyChanged(DynamicOperation.Prop_SortIndex);
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
					NotifyPropertyChanged(DynamicOperation.Prop_Description);
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
					NotifyPropertyChanged(DynamicOperation.Prop_CreaterID);
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
					NotifyPropertyChanged(DynamicOperation.Prop_CreaterName);
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
					NotifyPropertyChanged(DynamicOperation.Prop_CreatedDate);
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
					NotifyPropertyChanged(DynamicOperation.Prop_LastModifiedDate);
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

	} // DynamicOperation
}

