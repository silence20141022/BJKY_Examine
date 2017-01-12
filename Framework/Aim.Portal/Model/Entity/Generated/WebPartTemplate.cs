namespace Aim.Portal.Model
{
	// Business class WebPartTemplate generated from WebPartTemplate
	// Administrator [2010-03-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("WebPartTemplate")]
	public partial class WebPartTemplate 
		: EntityBase<WebPartTemplate> 
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
		public static string Prop_BaseTemplateId = "BaseTemplateId";

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
		private string _baseTemplateId;


		#endregion

		#region Constructors

		public WebPartTemplate()
		{
		}

		public WebPartTemplate(
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
			string p_baseTemplateId)
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
			_baseTemplateId = p_baseTemplateId;
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

		[Property("BaseTemplateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string BaseTemplateId
		{
			get { return _baseTemplateId; }
			set { _baseTemplateId = value; }
		}

		#endregion

	} // WebPartTemplate
}

