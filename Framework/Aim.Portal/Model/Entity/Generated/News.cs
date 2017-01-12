namespace Aim.Portal.Model
{
	// Business class News generated from News
	// Administrator [2010-04-04] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("News")]
	public partial class News 
		: EntityBase<News> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_TypeId = "TypeId";
		public static string Prop_BelongDeptId = "BelongDeptId";
		public static string Prop_Title = "Title";
		public static string Prop_KeyWord = "KeyWord";
		public static string Prop_Content = "Content";
		public static string Prop_ContentType = "ContentType";
		public static string Prop_AuthorName = "AuthorName";
		public static string Prop_PostUserId = "PostUserId";
		public static string Prop_PostUserName = "PostUserName";
		public static string Prop_PostDeptId = "PostDeptId";
		public static string Prop_PostDeptName = "PostDeptName";
		public static string Prop_ReceiveDeptId = "ReceiveDeptId";
		public static string Prop_ReceiveDeptName = "ReceiveDeptName";
		public static string Prop_ReceiveUserId = "ReceiveUserId";
		public static string Prop_ReceiveUserName = "ReceiveUserName";
		public static string Prop_PostTime = "PostTime";
		public static string Prop_ExpireTime = "ExpireTime";
		public static string Prop_SaveTime = "SaveTime";
		public static string Prop_Pictures = "Pictures";
		public static string Prop_Attachments = "Attachments";
		public static string Prop_MHT = "MHT";
		public static string Prop_State = "State";
		public static string Prop_ImportantGrade = "ImportantGrade";
		public static string Prop_ReadCount = "ReadCount";
		public static string Prop_HomePagePopup = "HomePagePopup";
		public static string Prop_LinkPortalImage = "LinkPortalImage";
		public static string Prop_Class = "Class";
		public static string Prop_PopupIds = "PopupIds";
		public static string Prop_Grade = "Grade";
		public static string Prop_AuthorId = "AuthorId";

		#endregion

		#region Private_Variables

		private string _id;
		private string _typeId;
		private string _belongDeptId;
		private string _title;
		private string _keyWord;
		private string _content;
		private string _contentType;
		private string _authorName;
		private string _postUserId;
		private string _postUserName;
		private string _postDeptId;
		private string _postDeptName;
		private string _receiveDeptId;
		private string _receiveDeptName;
		private string _receiveUserId;
		private string _receiveUserName;
		private DateTime? _postTime;
		private DateTime? _expireTime;
		private DateTime? _saveTime;
		private string _pictures;
		private string _attachments;
		private string _mHT;
		private string _state;
		private string _importantGrade;
		private int? _readCount;
		private string _homePagePopup;
		private string _linkPortalImage;
		private string _class;
		private string _popupIds;
		private string _grade;
		private string _authorId;


		#endregion

		#region Constructors

		public News()
		{
		}

		public News(
			string p_id,
			string p_typeid,
			string p_belongDeptId,
			string p_title,
			string p_keyWord,
			string p_content,
			string p_contentType,
			string p_authorName,
			string p_postUserId,
			string p_postUserName,
			string p_postDeptId,
			string p_postDeptName,
			string p_receiveDeptId,
			string p_receiveDeptName,
			string p_receiveUserId,
			string p_receiveUserName,
			DateTime? p_postTime,
			DateTime? p_expireTime,
			DateTime? p_saveTime,
			string p_pictures,
			string p_attachments,
			string p_mHT,
			string p_state,
			string p_importantGrade,
			int? p_readCount,
			string p_homePagePopup,
			string p_linkPortalImage,
			string p_class,
			string p_popupIds,
			string p_grade,
			string p_authorId)
		{
			_id = p_id;
			_typeId = p_typeid;
			_belongDeptId = p_belongDeptId;
			_title = p_title;
			_keyWord = p_keyWord;
			_content = p_content;
			_contentType = p_contentType;
			_authorName = p_authorName;
			_postUserId = p_postUserId;
			_postUserName = p_postUserName;
			_postDeptId = p_postDeptId;
			_postDeptName = p_postDeptName;
			_receiveDeptId = p_receiveDeptId;
			_receiveDeptName = p_receiveDeptName;
			_receiveUserId = p_receiveUserId;
			_receiveUserName = p_receiveUserName;
			_postTime = p_postTime;
			_expireTime = p_expireTime;
			_saveTime = p_saveTime;
			_pictures = p_pictures;
			_attachments = p_attachments;
			_mHT = p_mHT;
			_state = p_state;
			_importantGrade = p_importantGrade;
			_readCount = p_readCount;
			_homePagePopup = p_homePagePopup;
			_linkPortalImage = p_linkPortalImage;
			_class = p_class;
			_popupIds = p_popupIds;
			_grade = p_grade;
			_authorId = p_authorId;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

        [Property("TypeId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string TypeId
		{
			get { return _typeId; }
            set { _typeId = value; }
		}

		[Property("BelongDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string BelongDeptId
		{
			get { return _belongDeptId; }
			set { _belongDeptId = value; }
		}

		[Property("Title", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		[Property("KeyWord", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string KeyWord
		{
			get { return _keyWord; }
			set { _keyWord = value; }
		}

		[Property("Content", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		[Property("ContentType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ContentType
		{
			get { return _contentType; }
			set { _contentType = value; }
		}

		[Property("AuthorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AuthorName
		{
			get { return _authorName; }
			set { _authorName = value; }
		}

		[Property("PostUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string PostUserId
		{
			get { return _postUserId; }
			set { _postUserId = value; }
		}

		[Property("PostUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string PostUserName
		{
			get { return _postUserName; }
			set { _postUserName = value; }
		}

		[Property("PostDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string PostDeptId
		{
			get { return _postDeptId; }
			set { _postDeptId = value; }
		}

		[Property("PostDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string PostDeptName
		{
			get { return _postDeptName; }
			set { _postDeptName = value; }
		}

        [Property("ReceiveDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ReceiveDeptId
		{
			get { return _receiveDeptId; }
			set { _receiveDeptId = value; }
		}

        [Property("ReceiveDeptName", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ReceiveDeptName
		{
			get { return _receiveDeptName; }
			set { _receiveDeptName = value; }
		}

        [Property("ReceiveUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ReceiveUserId
		{
			get { return _receiveUserId; }
			set { _receiveUserId = value; }
		}

        [Property("ReceiveUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ReceiveUserName
		{
			get { return _receiveUserName; }
			set { _receiveUserName = value; }
		}

		[Property("PostTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? PostTime
		{
			get { return _postTime; }
			set { _postTime = value; }
		}

		[Property("ExpireTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ExpireTime
		{
			get { return _expireTime; }
			set { _expireTime = value; }
		}

		[Property("SaveTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? SaveTime
		{
			get { return _saveTime; }
			set { _saveTime = value; }
		}

		[Property("Pictures", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string Pictures
		{
			get { return _pictures; }
			set { _pictures = value; }
		}

		[Property("Attachments", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string Attachments
		{
			get { return _attachments; }
			set { _attachments = value; }
		}

		[Property("MHT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MHT
		{
			get { return _mHT; }
			set { _mHT = value; }
		}

		[Property("State", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string State
		{
			get { return _state; }
			set { _state = value; }
		}

		[Property("ImportantGrade", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ImportantGrade
		{
			get { return _importantGrade; }
			set { _importantGrade = value; }
		}

		[Property("ReadCount", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? ReadCount
		{
			get { return _readCount; }
			set { _readCount = value; }
		}

		[Property("HomePagePopup", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string HomePagePopup
		{
			get { return _homePagePopup; }
			set { _homePagePopup = value; }
		}

		[Property("LinkPortalImage", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string LinkPortalImage
		{
			get { return _linkPortalImage; }
			set { _linkPortalImage = value; }
		}

		[Property("Class", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string Class
		{
			get { return _class; }
			set { _class = value; }
		}

		[Property("PopupIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string PopupIds
		{
			get { return _popupIds; }
			set { _popupIds = value; }
		}

		[Property("Grade", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string Grade
		{
			get { return _grade; }
			set { _grade = value; }
		}

		[Property("AuthorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 8)]
		public string AuthorId
		{
			get { return _authorId; }
			set { _authorId = value; }
		}

		#endregion

	} // News
}

