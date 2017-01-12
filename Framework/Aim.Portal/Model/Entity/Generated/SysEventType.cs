// Business class SysEventType generated from SysEventType
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
	[ActiveRecord("SysEventType")]
	public partial class SysEventType : EntityBase<SysEventType> , INotifyPropertyChanged 	
	{

		#region Property_Names

		public static string Prop_EventTypeID = "EventTypeID";
		public static string Prop_Name = "Name";

		#endregion

		#region Private_Variables

		private int _eventtypeid;
		private string _name;


		#endregion

		#region Constructors

		public SysEventType()
		{
		}

		public SysEventType(
			int p_eventtypeid,
			string p_name)
		{
			_eventtypeid = p_eventtypeid;
			_name = p_name;
		}

		#endregion

		#region Properties

		[PrimaryKey("EventTypeID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public int EventTypeID
		{
			get { return _eventtypeid; }
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
					NotifyPropertyChanged(SysEventType.Prop_Name);
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

	} // SysEventType
}

