// Business class SysUser generated from SysUser
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
	[ActiveRecord("SysUser")]
	public partial class SysUser : EntityBase<SysUser> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_UserID = "UserID";
        public static string Prop_LoginName = "LoginName";
        public static string Prop_WorkNo = "WorkNo";
		public static string Prop_Password = "Password";
		public static string Prop_Name = "Name";
        public static string Prop_Email = "Email";
        public static string Prop_Phone = "Phone";
        public static string Prop_HomePhone = "HomePhone";
        public static string Prop_Fax = "Fax";
        public static string Prop_Sex = "Sex";
        public static string Prop_Server_IAGUID = "Server_IAGUID";
        public static string Prop_Server_Seed = "Server_Seed";
        public static string Prop_ThreeDESKEY = "ThreeDESKEY";
        public static string Prop_Ext1 = "Ext1";
        public static string Prop_Ext2 = "Ext2";
        public static string Prop_Remark = "Remark";
		public static string Prop_Status = "Status";
		public static string Prop_LastLogIP = "LastLogIP";
		public static string Prop_LastLogDate = "LastLogDate";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreateDate = "CreateDate";

		#endregion

		#region Private_Variables

		private string _userid;
        private string _loginName;
        private string _workNo;
		private string _password;
		private string _name;
        private string _email;
        private string _phone;
        private string _homePhone;
        private string _fax;
        private string _sex;
        private string _server_IAGUID;
        private string _server_Seed;
        private string _threeDESKEY;
        private string _remark;
        private string _ext1;
        private string _ext2;
		private int? _status;
		private string _lastLogIP;
		private DateTime? _lastLogDate;
		private int? _sortIndex;
		private DateTime? _lastModifiedDate;
        private DateTime? _createDate;

		#endregion

		#region Constructors

		public SysUser()
		{
		}

		public SysUser(
			string p_userid,
            string p_loginName,
            string p_workNo,
			string p_password,
			string p_name,
			string p_email,
			string p_remark,
			byte? p_status,
			string p_lastLogIP,
			DateTime? p_lastLogDate,
			int? p_sortIndex,
			DateTime? p_lastModifiedDate,
			DateTime? p_createDate)
		{
			_userid = p_userid;
            _loginName = p_loginName;
            _workNo = p_workNo;
			_password = p_password;
			_name = p_name;
			_email = p_email;
			_remark = p_remark;
			_status = p_status;
			_lastLogIP = p_lastLogIP;
			_lastLogDate = p_lastLogDate;
			_sortIndex = p_sortIndex;
			_lastModifiedDate = p_lastModifiedDate;
			_createDate = p_createDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("UserID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string UserID
		{
			get { return _userid; }
		}

		[Property("LoginName", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
		public string LoginName
		{
			get { return _loginName; }
			set
			{
				if ((_loginName == null) || (value == null) || (!value.Equals(_loginName)))
				{
					_loginName = value;
					NotifyPropertyChanged(SysUser.Prop_LoginName);
				}
			}
		}

		[Property("WorkNo", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 16)]
        public string WorkNo
		{
			get { return _workNo; }
			set
			{
                if ((_workNo == null) || (value == null) || (!value.Equals(_workNo)))
				{
                    _workNo = value;
                    NotifyPropertyChanged(SysUser.Prop_WorkNo);
				}
			}
		}

        [JsonIgnore]
		[Property("Password", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string Password
		{
			get { return _password; }
			set
			{
				if ((_password == null) || (value == null) || (!value.Equals(_password)))
				{
					_password = value;
					NotifyPropertyChanged(SysUser.Prop_Password);
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
					NotifyPropertyChanged(SysUser.Prop_Name);
				}
			}
		}

		[Property("Email", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string Email
		{
			get { return _email; }
			set
			{
				if ((_email == null) || (value == null) || (!value.Equals(_email)))
				{
					_email = value;
					NotifyPropertyChanged(SysUser.Prop_Email);
				}
			}
		}


        [Property("Phone", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Phone
        {
            get { return _phone; }
            set
            {
                if ((_phone == null) || (value == null) || (!value.Equals(_phone)))
                {
                    object oldValue = _phone;
                    _phone = value;
                    NotifyPropertyChanged(SysUser.Prop_Phone);
                }
            }

        }

        [Property("HomePhone", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                if ((_homePhone == null) || (value == null) || (!value.Equals(_homePhone)))
                {
                    object oldValue = _homePhone;
                    _homePhone = value;
                    NotifyPropertyChanged(SysUser.Prop_HomePhone);
                }
            }

        }

        [Property("Fax", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Fax
        {
            get { return _fax; }
            set
            {
                if ((_fax == null) || (value == null) || (!value.Equals(_fax)))
                {
                    object oldValue = _fax;
                    _fax = value;
                    NotifyPropertyChanged(SysUser.Prop_Fax);
                }
            }

        }

        [Property("Sex", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Sex
        {
            get { return _sex; }
            set
            {
                if ((_sex == null) || (value == null) || (!value.Equals(_sex)))
                {
                    object oldValue = _sex;
                    _sex = value;
                    NotifyPropertyChanged(SysUser.Prop_Sex);
                }
            }

        }

        [Property("Server_IAGUID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Server_IAGUID
        {
            get { return _server_IAGUID; }
            set
            {
                if ((_server_IAGUID == null) || (value == null) || (!value.Equals(_server_IAGUID)))
                {
                    object oldValue = _server_IAGUID;
                    _server_IAGUID = value;
                    NotifyPropertyChanged(SysUser.Prop_Server_IAGUID);
                }
            }

        }

        [Property("Server_Seed", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Server_Seed
        {
            get { return _server_Seed; }
            set
            {
                if ((_server_Seed == null) || (value == null) || (!value.Equals(_server_Seed)))
                {
                    object oldValue = _server_Seed;
                    _server_Seed = value;
                    NotifyPropertyChanged(SysUser.Prop_Server_Seed);
                }
            }

        }

        [Property("ThreeDESKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string ThreeDESKEY
        {
            get { return _threeDESKEY; }
            set
            {
                if ((_threeDESKEY == null) || (value == null) || (!value.Equals(_threeDESKEY)))
                {
                    object oldValue = _threeDESKEY;
                    _threeDESKEY = value;
                }
            }

        }

        [Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if ((_remark == null) || (value == null) || (!value.Equals(_remark)))
                {
                    object oldValue = _remark;
                    _remark = value;
                    RaisePropertyChanged(SysUser.Prop_Remark, oldValue, value);
                }
            }

        }

        [Property("Ext1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string Ext1
        {
            get { return _ext1; }
            set
            {
                if ((_ext1 == null) || (value == null) || (!value.Equals(_ext1)))
                {
                    object oldValue = _ext1;
                    _ext1 = value;
                    NotifyPropertyChanged(SysUser.Prop_Ext1);
                }
            }

        }

        [Property("Ext2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string Ext2
        {
            get { return _ext2; }
            set
            {
                if ((_ext2 == null) || (value == null) || (!value.Equals(_ext2)))
                {
                    object oldValue = _ext2;
                    _ext2 = value;
                    NotifyPropertyChanged(SysUser.Prop_Ext2);
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
					NotifyPropertyChanged(SysUser.Prop_Status);
				}
			}
		}

		[Property("LastLogIP", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string LastLogIP
		{
			get { return _lastLogIP; }
			set
			{
				if ((_lastLogIP == null) || (value == null) || (!value.Equals(_lastLogIP)))
				{
					_lastLogIP = value;
					NotifyPropertyChanged(SysUser.Prop_LastLogIP);
				}
			}
		}

		[Property("LastLogDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? LastLogDate
		{
			get { return _lastLogDate; }
			set
			{
				if (value != _lastLogDate)
				{
					_lastLogDate = value;
					NotifyPropertyChanged(SysUser.Prop_LastLogDate);
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
					NotifyPropertyChanged(SysUser.Prop_SortIndex);
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
					NotifyPropertyChanged(SysUser.Prop_LastModifiedDate);
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
					NotifyPropertyChanged(SysUser.Prop_CreateDate);
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

	} // SysUser
}

