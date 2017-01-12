namespace Aim.Portal.Model
{
	// Business class WebPart generated from WebPart
	// Administrator [2010-03-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("WebPart")]
	public partial class WebPart 
		: EntityBase<WebPart> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_BlockName = "BlockName";
		public static string Prop_BlockKey = "BlockKey";
		public static string Prop_BlockTitle = "BlockTitle";
		public static string Prop_BlockType = "BlockType";
		public static string Prop_BlockImage = "BlockImage";
		public static string Prop_Remark = "Remark";
		public static string Prop_HeadHtml = "HeadHtml";
		public static string Prop_ColorValue = "ColorValue";
		public static string Prop_Color = "Color";
        public static string Prop_DefaultHeight = "DefaultHeight";
        public static string Prop_ContentColor = "ContentColor";
		public static string Prop_RepeatItemCount = "RepeatItemCount";
		public static string Prop_RepeatItemLength = "RepeatItemLength";
		public static string Prop_RepeatDataDataSql = "RepeatDataDataSql";
		public static string Prop_RepeatItemTemplate = "RepeatItemTemplate";
		public static string Prop_FootHtml = "FootHtml";
		public static string Prop_DelayLoadSecond = "DelayLoadSecond";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_RelateScript = "RelateScript";
		public static string Prop_IsHidden = "IsHidden";
		public static string Prop_TemplateId = "TemplateId";
		public static string Prop_AllowUserIds = "AllowUserIds";
		public static string Prop_AllowUserNames = "AllowUserNames";
		public static string Prop_AllowTypes = "AllowTypes";

		#endregion

		#region Private_Variables

		private string _id;
		private string _blockName;
		private string _blockKey;
		private string _blockTitle;
		private string _blockType;
		private string _blockImage;
		private string _remark;
		private string _headHtml;
		private string _colorValue;
		private string _color;
        private string _defaultHeight;
        private string _contentColor;
		private int? _repeatItemCount;
		private int? _repeatItemLength;
		private string _repeatDataDataSql;
		private string _repeatItemTemplate;
		private string _footHtml;
		private int? _delayLoadSecond;
		private float? _sortIndex;
		private string _relateScript;
		private string _isHidden;
		private string _templateId;
		private string _allowUserIds;
		private string _allowUserNames;
		private string _allowTypes;


		#endregion

		#region Constructors

		public WebPart()
		{
		}

		public WebPart(
			string p_id,
			string p_blockName,
			string p_blockKey,
			string p_blockTitle,
			string p_blockType,
			string p_blockImage,
			string p_remark,
			string p_headHtml,
			string p_colorValue,
			string p_color,
			int? p_repeatItemCount,
			int? p_repeatItemLength,
			string p_repeatDataDataSql,
			string p_repeatItemTemplate,
			string p_footHtml,
			int? p_delayLoadSecond,
			float? p_sortIndex,
			string p_relateScript,
			string p_isHidden,
			string p_templateId,
			string p_allowUserIds,
			string p_allowUserNames,
			string p_allowTypes)
		{
			_id = p_id;
			_blockName = p_blockName;
			_blockKey = p_blockKey;
			_blockTitle = p_blockTitle;
			_blockType = p_blockType;
			_blockImage = p_blockImage;
			_remark = p_remark;
			_headHtml = p_headHtml;
			_colorValue = p_colorValue;
			_color = p_color;
			_repeatItemCount = p_repeatItemCount;
			_repeatItemLength = p_repeatItemLength;
			_repeatDataDataSql = p_repeatDataDataSql;
			_repeatItemTemplate = p_repeatItemTemplate;
			_footHtml = p_footHtml;
			_delayLoadSecond = p_delayLoadSecond;
			_sortIndex = p_sortIndex;
			_relateScript = p_relateScript;
			_isHidden = p_isHidden;
			_templateId = p_templateId;
			_allowUserIds = p_allowUserIds;
			_allowUserNames = p_allowUserNames;
			_allowTypes = p_allowTypes;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("BlockName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BlockName
		{
			get { return _blockName; }
			set { _blockName = value; }
		}

		[Property("BlockKey", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string BlockKey
		{
			get { return _blockKey; }
			set { _blockKey = value; }
		}

		[Property("BlockTitle", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BlockTitle
		{
			get { return _blockTitle; }
			set { _blockTitle = value; }
		}

		[Property("BlockType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string BlockType
		{
			get { return _blockType; }
			set { _blockType = value; }
		}

		[Property("BlockImage", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BlockImage
		{
			get { return _blockImage; }
			set { _blockImage = value; }
		}

		[Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string Remark
		{
			get { return _remark; }
			set { _remark = value; }
		}

		[Property("HeadHtml", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string HeadHtml
		{
			get { return _headHtml; }
			set { _headHtml = value; }
		}

		[Property("ColorValue", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
		public string ColorValue
		{
			get { return _colorValue; }
			set { _colorValue = value; }
		}

		[Property("Color", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 15)]
		public string Color
		{
			get { return _color; }
			set { _color = value; }
		}
        [Property("DefaultHeight", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string DefaultHeight
		{
            get { return _defaultHeight; }
            set { _defaultHeight = value; }
		}
        
        [Property("ContentColor", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string ContentColor
        {
            get { return _contentColor; }
            set { _contentColor = value; }
        }

		[Property("RepeatItemCount", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? RepeatItemCount
		{
			get { return _repeatItemCount; }
			set { _repeatItemCount = value; }
		}

		[Property("RepeatItemLength", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? RepeatItemLength
		{
			get { return _repeatItemLength; }
			set { _repeatItemLength = value; }
		}

		[Property("RepeatDataDataSql", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string RepeatDataDataSql
		{
			get { return _repeatDataDataSql; }
			set { _repeatDataDataSql = value; }
		}

		[Property("RepeatItemTemplate", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string RepeatItemTemplate
		{
			get { return _repeatItemTemplate; }
			set { _repeatItemTemplate = value; }
		}

		[Property("FootHtml", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string FootHtml
		{
			get { return _footHtml; }
			set { _footHtml = value; }
		}

		[Property("DelayLoadSecond", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? DelayLoadSecond
		{
			get { return _delayLoadSecond; }
			set { _delayLoadSecond = value; }
		}

		[Property("SortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public float? SortIndex
		{
			get { return _sortIndex; }
			set { _sortIndex = value; }
		}

		[Property("RelateScript", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string RelateScript
		{
			get { return _relateScript; }
			set { _relateScript = value; }
		}

		[Property("IsHidden", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2)]
		public string IsHidden
		{
			get { return _isHidden; }
			set { _isHidden = value; }
		}

		[Property("TemplateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 8)]
		public string TemplateId
		{
			get { return _templateId; }
			set { _templateId = value; }
		}

		[Property("AllowUserIds", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string AllowUserIds
		{
			get { return _allowUserIds; }
			set { _allowUserIds = value; }
		}

		[Property("AllowUserNames", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string AllowUserNames
		{
			get { return _allowUserNames; }
			set { _allowUserNames = value; }
		}

		[Property("AllowTypes", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string AllowTypes
		{
			get { return _allowTypes; }
			set { _allowTypes = value; }
		}

		#endregion

	} // WebPart
}

