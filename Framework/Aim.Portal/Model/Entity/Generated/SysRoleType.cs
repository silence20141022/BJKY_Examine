// Business class SysRoleType generated from SysRoleType
// Creator: Ray
// Created Date: [2000-04-23]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SysRoleType")]
	public partial class SysRoleType : EntityBase<SysRoleType> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_RoleTypeID = "RoleTypeID";
		public static string Prop_Name = "Name";

		#endregion

		#region Private_Variables

		private int _roletypeid;
		private string _name;


		#endregion

		#region Constructors

		public SysRoleType()
		{
		}

		public SysRoleType(
			int p_roletypeid,
			string p_name)
		{
			_roletypeid = p_roletypeid;
			_name = p_name;
		}

		#endregion

		#region Properties

        [PrimaryKey("RoleTypeID", Generator = PrimaryKeyType.Increment, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public int RoleTypeID
		{
			get { return _roletypeid; }
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
					NotifyPropertyChanged(SysRoleType.Prop_Name);
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

	} // SysRoleType
}

