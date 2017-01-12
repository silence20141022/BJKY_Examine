namespace Aim.Portal.Model
{
	// Business class NewsType generated from NewsType
	// Administrator [2010-04-04] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("NewsType")]
	public partial class NewsType 
		: EntityBase<NewsType> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_BelongDeptId = "BelongDeptId";
		public static string Prop_TypeName = "TypeName";
		public static string Prop_IsEfficient = "IsEfficient";
		public static string Prop_AllowQueryId = "AllowQueryId";
		public static string Prop_AllowQueryName = "AllowQueryName";
		public static string Prop_AllowManageId = "AllowManageId";
		public static string Prop_AllowManageName = "AllowManageName";
		public static string Prop_SortIndex = "SortIndex";
		public static string Prop_Class = "Class";
		public static string Prop_AllowSubmitId = "AllowSubmitId";
		public static string Prop_AllowSubmitName = "AllowSubmitName";
		public static string Prop_AllowAuditId = "AllowAuditId";
		public static string Prop_AllowAuditName = "AllowAuditName";
		public static string Prop_SubCatalogName = "SubCatalogName";

		#endregion

		#region Private_Variables

		private string _id;
		private string _belongDeptId;
		private string _typeName;
		private string _isEfficient;
		private string _allowQueryId;
		private string _allowQueryName;
		private string _allowManageId;
		private string _allowManageName;
		private int? _sortIndex;
		private string _class;
		private string _allowSubmitId;
		private string _allowSubmitName;
		private string _allowAuditId;
		private string _allowAuditName;
		private string _subCatalogName;


		#endregion

		#region Constructors

		public NewsType()
		{
		}

		public NewsType(
			string p_id,
			string p_belongDeptId,
			string p_typeName,
			string p_isEfficient,
			string p_allowQueryId,
			string p_allowQueryName,
			string p_allowManageId,
			string p_allowManageName,
			int? p_sortIndex,
			string p_class,
			string p_allowSubmitId,
			string p_allowSubmitName,
			string p_allowAuditId,
			string p_allowAuditName,
			string p_subCatalogName)
		{
			_id = p_id;
			_belongDeptId = p_belongDeptId;
			_typeName = p_typeName;
			_isEfficient = p_isEfficient;
			_allowQueryId = p_allowQueryId;
			_allowQueryName = p_allowQueryName;
			_allowManageId = p_allowManageId;
			_allowManageName = p_allowManageName;
			_sortIndex = p_sortIndex;
			_class = p_class;
			_allowSubmitId = p_allowSubmitId;
			_allowSubmitName = p_allowSubmitName;
			_allowAuditId = p_allowAuditId;
			_allowAuditName = p_allowAuditName;
			_subCatalogName = p_subCatalogName;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("BelongDeptId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string BelongDeptId
		{
			get { return _belongDeptId; }
			set { _belongDeptId = value; }
		}

		[Property("TypeName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string TypeName
		{
			get { return _typeName; }
			set { _typeName = value; }
		}

		[Property("IsEfficient", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsEfficient
		{
			get { return _isEfficient; }
			set { _isEfficient = value; }
		}

		[Property("AllowQueryId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowQueryId
		{
			get { return _allowQueryId; }
			set { _allowQueryId = value; }
		}

		[Property("AllowQueryName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowQueryName
		{
			get { return _allowQueryName; }
			set { _allowQueryName = value; }
		}

		[Property("AllowManageId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowManageId
		{
			get { return _allowManageId; }
			set { _allowManageId = value; }
		}

		[Property("AllowManageName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowManageName
		{
			get { return _allowManageName; }
			set { _allowManageName = value; }
		}

		[Property("SortIndex", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public int? SortIndex
		{
			get { return _sortIndex; }
			set { _sortIndex = value; }
		}

		[Property("Class", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string Class
		{
			get { return _class; }
			set { _class = value; }
		}

		[Property("AllowSubmitId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowSubmitId
		{
			get { return _allowSubmitId; }
			set { _allowSubmitId = value; }
		}

		[Property("AllowSubmitName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowSubmitName
		{
			get { return _allowSubmitName; }
			set { _allowSubmitName = value; }
		}

		[Property("AllowAuditId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowAuditId
		{
			get { return _allowAuditId; }
			set { _allowAuditId = value; }
		}

		[Property("AllowAuditName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string AllowAuditName
		{
			get { return _allowAuditName; }
			set { _allowAuditName = value; }
		}

		[Property("SubCatalogName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string SubCatalogName
		{
			get { return _subCatalogName; }
			set { _subCatalogName = value; }
		}

		#endregion

	} // NewsType
}

