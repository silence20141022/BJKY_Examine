namespace Aim.Portal.Model
{
	// Business class WebPartBaseTemplate generated from WebPartBaseTemplate
	// Administrator [2010-03-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("WebPartBaseTemplate")]
	public partial class WebPartBaseTemplate 
		: EntityBase<WebPartBaseTemplate> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_Type = "Type";
		public static string Prop_UserId = "UserId";
		public static string Prop_UserName = "UserName";
		public static string Prop_IsDefault = "IsDefault";
		public static string Prop_TemplateColWidth = "TemplateColWidth";
		public static string Prop_TemplateString = "TemplateString";
		public static string Prop_TemplateXml = "TemplateXml";
		public static string Prop_GlobalColor = "GlobalColor";
		public static string Prop_GlobalColorValue = "GlobalColorValue";
		public static string Prop_BlockType = "BlockType";
		public static string Prop_Title = "Title";
		public static string Prop_BaseType = "BaseType";
		public static string Prop_DBIds = "DBIds";
		public static string Prop_AllowQueryIds = "AllowQueryIds";
		public static string Prop_AllowQueryNames = "AllowQueryNames";
		public static string Prop_AllowQueryTypes = "AllowQueryTypes";
		public static string Prop_AllowManageIds = "AllowManageIds";
		public static string Prop_AllowManageNames = "AllowManageNames";
		public static string Prop_AllowManageTypes = "AllowManageTypes";

		#endregion

		#region Private_Variables

		private string _id;
		private string _type;
		private string _userId;
		private string _userName;
		private string _isDefault;
		private string _templateColWidth;
		private string _templateString;
		private string _templateXml;
		private string _globalColor;
		private string _globalColorValue;
		private string _blockType;
		private string _title;
		private string _baseType;
		private string _dBIds;
		private string _allowQueryIds;
		private string _allowQueryNames;
		private string _allowQueryTypes;
		private string _allowManageIds;
		private string _allowManageNames;
		private string _allowManageTypes;


		#endregion

		#region Constructors

		public WebPartBaseTemplate()
		{
		}

		public WebPartBaseTemplate(
			string p_id,
			string p_type,
			string p_userId,
			string p_userName,
			string p_isDefault,
			string p_templateColWidth,
			string p_templateString,
			string p_templateXml,
			string p_globalColor,
			string p_globalColorValue,
			string p_blockType,
			string p_title,
			string p_baseType,
			string p_dBIds,
			string p_allowQueryIds,
			string p_allowQueryNames,
			string p_allowQueryTypes,
			string p_allowManageIds,
			string p_allowManageNames,
			string p_allowManageTypes)
		{
			_id = p_id;
			_type = p_type;
			_userId = p_userId;
			_userName = p_userName;
			_isDefault = p_isDefault;
			_templateColWidth = p_templateColWidth;
			_templateString = p_templateString;
			_templateXml = p_templateXml;
			_globalColor = p_globalColor;
			_globalColorValue = p_globalColorValue;
			_blockType = p_blockType;
			_title = p_title;
			_baseType = p_baseType;
			_dBIds = p_dBIds;
			_allowQueryIds = p_allowQueryIds;
			_allowQueryNames = p_allowQueryNames;
			_allowQueryTypes = p_allowQueryTypes;
			_allowManageIds = p_allowManageIds;
			_allowManageNames = p_allowManageNames;
			_allowManageTypes = p_allowManageTypes;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("Type", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}

		[Property("UserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 8)]
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

		[Property("IsDefault", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

		[Property("TemplateColWidth", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TemplateColWidth
		{
			get { return _templateColWidth; }
			set { _templateColWidth = value; }
		}

		[Property("TemplateString", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string TemplateString
		{
			get { return _templateString; }
			set { _templateString = value; }
		}

		[Property("TemplateXml", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string TemplateXml
		{
			get { return _templateXml; }
			set { _templateXml = value; }
		}

		[Property("GlobalColor", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string GlobalColor
		{
			get { return _globalColor; }
			set { _globalColor = value; }
		}

		[Property("GlobalColorValue", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string GlobalColorValue
		{
			get { return _globalColorValue; }
			set { _globalColorValue = value; }
		}

		[Property("BlockType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BlockType
		{
			get { return _blockType; }
			set { _blockType = value; }
		}

		[Property("Title", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		[Property("BaseType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BaseType
		{
			get { return _baseType; }
			set { _baseType = value; }
		}

		[Property("DBIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string DBIds
		{
			get { return _dBIds; }
			set { _dBIds = value; }
		}

		[Property("AllowQueryIds", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowQueryIds
		{
			get { return _allowQueryIds; }
			set { _allowQueryIds = value; }
		}

		[Property("AllowQueryNames", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowQueryNames
		{
			get { return _allowQueryNames; }
			set { _allowQueryNames = value; }
		}

		[Property("AllowQueryTypes", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowQueryTypes
		{
			get { return _allowQueryTypes; }
			set { _allowQueryTypes = value; }
		}

		[Property("AllowManageIds", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowManageIds
		{
			get { return _allowManageIds; }
			set { _allowManageIds = value; }
		}

		[Property("AllowManageNames", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowManageNames
		{
			get { return _allowManageNames; }
			set { _allowManageNames = value; }
		}

		[Property("AllowManageTypes", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string AllowManageTypes
		{
			get { return _allowManageTypes; }
			set { _allowManageTypes = value; }
		}

		#endregion

	} // WebPartBaseTemplate
}

