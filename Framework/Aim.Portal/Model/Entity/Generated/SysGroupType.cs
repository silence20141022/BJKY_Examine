// Business class SysGroupType generated from SysGroupType
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SysGroupType")]
	public partial class SysGroupType : EntityBase<SysGroupType> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_GroupTypeID = "GroupTypeID";
		public static string Prop_Name = "Name";

		#endregion

		#region Private_Variables

		private int _grouptypeid;
		private string _name;


		#endregion

		#region Constructors

		public SysGroupType()
		{
		}

		public SysGroupType(
            int p_grouptypeid,
			string p_name)
		{
            _grouptypeid = p_grouptypeid;
			_name = p_name;
		}

		#endregion

		#region Properties

		[PrimaryKey("GroupTypeID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public int GroupTypeID
		{
            get { return _grouptypeid; }
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
					NotifyPropertyChanged(SysGroupType.Prop_Name);
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

	} // SysGroupType
}

