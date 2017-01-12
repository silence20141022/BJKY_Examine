// Business class SysApplication generated from SysApplication
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
	[ActiveRecord("SysApplication")]
	public partial class SysApplication : EntityBase<SysApplication> , INotifyPropertyChanged 	
	{

		#region Property_Names

        public static string Prop_ApplicationID = "ApplicationID";
        public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_Description = "Description";
		public static string Prop_Url = "Url";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreateDate = "CreateDate";

		#endregion

		#region Private_Variables

        private string _applicationid;
        private string _code;
		private string _name;
		private string _description;
		private string _url;
		private int? _sortIndex;
		private DateTime? _lastModifiedDate;
		private DateTime? _createDate;


		#endregion

		#region Constructors

		public SysApplication()
		{
		}

		public SysApplication(
            string p_applicationid,
            string p_code,
			string p_name,
			string p_description,
			string p_url,
			int? p_sortIndex,
			DateTime? p_lastModifiedDate,
			DateTime? p_createDate)
		{
			_applicationid = p_applicationid;
            _name = p_name;
            _code = p_code;
			_description = p_description;
			_url = p_url;
			_sortIndex = p_sortIndex;
			_lastModifiedDate = p_lastModifiedDate;
			_createDate = p_createDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("ApplicationID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ApplicationID
		{
			get { return _applicationid; }
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
					NotifyPropertyChanged(SysApplication.Prop_Name);
				}
			}
		}

		[Property("Description", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string Description
		{
			get { return _description; }
			set
			{
				if ((_description == null) || (value == null) || (!value.Equals(_description)))
				{
					_description = value;
					NotifyPropertyChanged(SysApplication.Prop_Description);
				}
			}
		}

		[Property("Url", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Url
		{
			get { return _url; }
			set
			{
				if ((_url == null) || (value == null) || (!value.Equals(_url)))
				{
					_url = value;
					NotifyPropertyChanged(SysApplication.Prop_Url);
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
					NotifyPropertyChanged(SysApplication.Prop_SortIndex);
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
					NotifyPropertyChanged(SysApplication.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(SysApplication.Prop_CreateDate);
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

	} // SysApplication
}

