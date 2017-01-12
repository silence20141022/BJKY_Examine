// Business class AddressList generated from AddressList
// Creator: Ray
// Created Date: [2010-05-01]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("AddressList")]
	public partial class AddressList : EntityBase<AddressList> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_ParentId = "ParentId";
		public static string Prop_FullId = "FullId";
		public static string Prop_CompanyId = "CompanyId";
		public static string Prop_CompanyName = "CompanyName";
		public static string Prop_UserId = "UserId";
		public static string Prop_UserName = "UserName";
		public static string Prop_Duty = "Duty";
		public static string Prop_Mobile = "Mobile";
		public static string Prop_Phone = "Phone";
		public static string Prop_Fax = "Fax";
        public static string Prop_EMail = "EMailList";

		#endregion

		#region Private_Variables

		private string _id;
		private string _parentId;
		private string _fullId;
		private string _companyId;
		private string _companyName;
		private string _userId;
		private string _userName;
		private string _duty;
		private string _mobile;
		private string _phone;
		private string _fax;
        private string _eMailList;


		#endregion

		#region Constructors

		public AddressList()
		{
		}

		public AddressList(
			string p_id,
			string p_parentId,
			string p_fullId,
			string p_companyId,
			string p_companyName,
			string p_userId,
			string p_userName,
			string p_duty,
			string p_mobile,
			string p_phone,
			string p_fax,
			string p_eMail)
		{
			_id = p_id;
			_parentId = p_parentId;
			_fullId = p_fullId;
			_companyId = p_companyId;
			_companyName = p_companyName;
			_userId = p_userId;
			_userName = p_userName;
			_duty = p_duty;
			_mobile = p_mobile;
			_phone = p_phone;
			_fax = p_fax;
			_eMailList = p_eMail;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
            set { _id = value; }
		}

		[Property("ParentId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ParentId
		{
			get { return _parentId; }
			set
			{
				if ((_parentId == null) || (value == null) || (!value.Equals(_parentId)))
				{
					_parentId = value;
					NotifyPropertyChanged(AddressList.Prop_ParentId);
				}
			}
		}

		[Property("FullId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string FullId
		{
			get { return _fullId; }
			set
			{
				if ((_fullId == null) || (value == null) || (!value.Equals(_fullId)))
				{
					_fullId = value;
					NotifyPropertyChanged(AddressList.Prop_FullId);
				}
			}
		}

		[Property("CompanyId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CompanyId
		{
			get { return _companyId; }
			set
			{
				if ((_companyId == null) || (value == null) || (!value.Equals(_companyId)))
				{
					_companyId = value;
					NotifyPropertyChanged(AddressList.Prop_CompanyId);
				}
			}
		}

		[Property("CompanyName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 80)]
		public string CompanyName
		{
			get { return _companyName; }
			set
			{
				if ((_companyName == null) || (value == null) || (!value.Equals(_companyName)))
				{
					_companyName = value;
					NotifyPropertyChanged(AddressList.Prop_CompanyName);
				}
			}
		}

		[Property("UserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string UserId
		{
			get { return _userId; }
			set
			{
				if ((_userId == null) || (value == null) || (!value.Equals(_userId)))
				{
					_userId = value;
					NotifyPropertyChanged(AddressList.Prop_UserId);
				}
			}
		}

		[Property("UserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string UserName
		{
			get { return _userName; }
			set
			{
				if ((_userName == null) || (value == null) || (!value.Equals(_userName)))
				{
					_userName = value;
					NotifyPropertyChanged(AddressList.Prop_UserName);
				}
			}
		}

		[Property("Duty", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Duty
		{
			get { return _duty; }
			set
			{
				if ((_duty == null) || (value == null) || (!value.Equals(_duty)))
				{
					_duty = value;
					NotifyPropertyChanged(AddressList.Prop_Duty);
				}
			}
		}

		[Property("Mobile", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Mobile
		{
			get { return _mobile; }
			set
			{
				if ((_mobile == null) || (value == null) || (!value.Equals(_mobile)))
				{
					_mobile = value;
					NotifyPropertyChanged(AddressList.Prop_Mobile);
				}
			}
		}

		[Property("Phone", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Phone
		{
			get { return _phone; }
			set
			{
				if ((_phone == null) || (value == null) || (!value.Equals(_phone)))
				{
					_phone = value;
					NotifyPropertyChanged(AddressList.Prop_Phone);
				}
			}
		}

		[Property("Fax", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Fax
		{
			get { return _fax; }
			set
			{
				if ((_fax == null) || (value == null) || (!value.Equals(_fax)))
				{
					_fax = value;
					NotifyPropertyChanged(AddressList.Prop_Fax);
				}
			}
		}

		[Property("EMailList", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string EMailList
		{
			get { return _eMailList; }
			set
			{
				if ((_eMailList == null) || (value == null) || (!value.Equals(_eMailList)))
				{
					_eMailList = value;
					NotifyPropertyChanged(AddressList.Prop_EMail);
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

	} // AddressList
}

