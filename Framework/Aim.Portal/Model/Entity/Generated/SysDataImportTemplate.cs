// Business class SysDataImportTemplate generated from SysDataImportTemplate
// Creator: Ray
// Created Date: [2000-06-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("SysDataImportTemplate")]
	public partial class SysDataImportTemplate : EntityBase<SysDataImportTemplate> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_DataImportTemplateID = "DataImportTemplateID";
		public static string Prop_Name = "Name";
		public static string Prop_Code = "Code";
        public static string Prop_TemplateFileID = "TemplateFileID";
        public static string Prop_DownloadFileID = "DownloadFileID";
        public static string Prop_Config = "Config";
        public static string Prop_Description = "Description";
		public static string Prop_ConfigType = "ConfigType";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _dataimporttemplateid;
		private string _name;
		private string _code;
        private string _templateFileID;
        private string _downloadFileID;
        private string _config;
        private string _description;
		private string _configType;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;


		#endregion

		#region Constructors

		public SysDataImportTemplate()
		{
		}

		public SysDataImportTemplate(
			string p_dataimporttemplateid,
			string p_name,
			string p_code,
            string p_templateFileID,
            string p_downloadFileID,
			string p_config,
			string p_configType,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_dataimporttemplateid = p_dataimporttemplateid;
			_name = p_name;
			_code = p_code;
			_templateFileID = p_templateFileID;
            _downloadFileID = p_downloadFileID;
			_config = p_config;
			_configType = p_configType;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

        [PrimaryKey("DataImportTemplateID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string DataImportTemplateID
		{
			get { return _dataimporttemplateid; }
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_Name);
				}
			}
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_Code);
				}
			}
        }

        [Property("DownloadFileID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
        public string DownloadFileID
        {
            get { return _downloadFileID; }
            set
            {
                if ((_downloadFileID == null) || (value == null) || (!value.Equals(_downloadFileID)))
                {
                    _downloadFileID = value;
                    NotifyPropertyChanged(SysDataImportTemplate.Prop_DownloadFileID);
                }
            }
        }

        [Property("TemplateFileID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
        public string TemplateFileID
		{
			get { return _templateFileID; }
			set
			{
				if ((_templateFileID == null) || (value == null) || (!value.Equals(_templateFileID)))
				{
					_templateFileID = value;
					NotifyPropertyChanged(SysDataImportTemplate.Prop_TemplateFileID);
				}
			}
		}

		[Property("Config", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Config
		{
			get { return _config; }
			set
			{
				if ((_config == null) || (value == null) || (!value.Equals(_config)))
				{
					_config = value;
					NotifyPropertyChanged(SysDataImportTemplate.Prop_Config);
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
                    NotifyPropertyChanged(SysDataImportTemplate.Prop_Description);
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_CreaterID);
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_CreaterName);
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(SysDataImportTemplate.Prop_CreatedDate);
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

	} // SysDataImportTemplate
}

