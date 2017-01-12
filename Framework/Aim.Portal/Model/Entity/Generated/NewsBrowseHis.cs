namespace Aim.Portal.Model
{
	// Business class NewsBrowseHis generated from NewsBrowseHis
	// Administrator [2010-04-04] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("NewsBrowseHis")]
	public partial class NewsBrowseHis 
		: EntityBase<NewsBrowseHis> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_TypeId = "TypeId";
		public static string Prop_NewsId = "NewsId";
		public static string Prop_BrowseTime = "BrowseTime";
		public static string Prop_UserId = "UserId";
		public static string Prop_UserName = "UserName";
		public static string Prop_ReadCount = "ReadCount";

		#endregion

		#region Private_Variables

		private int _id;
		private string _typeid;
		private string _newsId;
		private DateTime? _browseTime;
		private string _userId;
		private string _userName;
		private int? _readCount;


		#endregion

		#region Constructors

		public NewsBrowseHis()
		{
		}

		public NewsBrowseHis(
			int p_id,
			string p_typeid,
			string p_newsId,
			DateTime? p_browseTime,
			string p_userId,
			string p_userName,
			int? p_readCount)
		{
			_id = p_id;
			_typeid = p_typeid;
			_newsId = p_newsId;
			_browseTime = p_browseTime;
			_userId = p_userId;
			_userName = p_userName;
			_readCount = p_readCount;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id",Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public int Id
		{
			get { return _id; }
		}

        [Property("TypeId", Access = PropertyAccess.NosetterLowercaseUnderscore, Length = 36)]
		public string TypeId
		{
			get { return _typeid; }
		}

        [Property("NewsId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string NewsId
		{
			get { return _newsId; }
			set { _newsId = value; }
		}

		[Property("BrowseTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? BrowseTime
		{
			get { return _browseTime; }
			set { _browseTime = value; }
		}

        [Property("UserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		[Property("UserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		[Property("ReadCount", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? ReadCount
		{
			get { return _readCount; }
			set { _readCount = value; }
		}

		#endregion

	} // NewsBrowseHi
}

