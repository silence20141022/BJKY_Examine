namespace Aim.Portal.Model
{
	// Business class FileItem generated from FileItem
	// Administrator [2010-04-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("FileItem")]
	public partial class FileItem 
		: EntityBase<FileItem> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_Name = "Name";
		public static string Prop_ExtName = "ExtName";
		public static string Prop_FileSize = "FileSize";
		public static string Prop_Path = "Path";
		public static string Prop_FullId = "FullId";
		public static string Prop_GroupId = "GroupId";
		public static string Prop_FolderId = "FolderId";
		public static string Prop_ModuleId = "ModuleId";
		public static string Prop_IsReference = "IsReference";
		public static string Prop_ReferenceId = "ReferenceId";
		public static string Prop_IsRelate = "IsRelate";
		public static string Prop_HasRelate = "HasRelate";
		public static string Prop_RelateType = "RelateType";
		public static string Prop_RelateId = "RelateId";
		public static string Prop_IsCompressed = "IsCompressed";
		public static string Prop_IsEncrypted = "IsEncrypted";
		public static string Prop_IsActiveVersion = "IsActiveVersion";
		public static string Prop_HasVersion = "HasVersion";
		public static string Prop_Version = "Version";
		public static string Prop_VersionMemo = "VersionMemo";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_CreatorId = "CreatorId";
		public static string Prop_CreatorName = "CreatorName";

		#endregion

		#region Private_Variables

		private string _id;
		private string _name;
		private string _extName;
		private long? _fileSize;
		private string _path;
		private string _fullId;
		private string _groupId;
		private string _folderId;
		private string _moduleId;
		private string _isReference;
		private string _referenceId;
		private string _isRelate;
		private string _hasRelate;
		private string _relateType;
		private string _relateId;
		private string _isCompressed;
		private string _isEncrypted;
		private string _isActiveVersion;
		private string _hasVersion;
		private string _version;
		private string _versionMemo;
		private DateTime? _createTime;
		private string _creatorId;
		private string _creatorName;


		#endregion

		#region Constructors

		public FileItem()
		{
		}

		public FileItem(
			string p_id,
			string p_name,
			string p_extName,
			long? p_fileSize,
			string p_path,
			string p_fullId,
			string p_groupId,
			string p_folderId,
			string p_moduleId,
			string p_isReference,
			string p_referenceId,
			string p_isRelate,
			string p_hasRelate,
			string p_relateType,
			string p_relateId,
			string p_isCompressed,
			string p_isEncrypted,
			string p_isActiveVersion,
			string p_hasVersion,
			string p_version,
			string p_versionMemo,
			DateTime? p_createTime,
			string p_creatorId,
			string p_creatorName)
		{
			_id = p_id;
			_name = p_name;
			_extName = p_extName;
			_fileSize = p_fileSize;
			_path = p_path;
			_fullId = p_fullId;
			_groupId = p_groupId;
			_folderId = p_folderId;
			_moduleId = p_moduleId;
			_isReference = p_isReference;
			_referenceId = p_referenceId;
			_isRelate = p_isRelate;
			_hasRelate = p_hasRelate;
			_relateType = p_relateType;
			_relateId = p_relateId;
			_isCompressed = p_isCompressed;
			_isEncrypted = p_isEncrypted;
			_isActiveVersion = p_isActiveVersion;
			_hasVersion = p_hasVersion;
			_version = p_version;
			_versionMemo = p_versionMemo;
			_createTime = p_createTime;
			_creatorId = p_creatorId;
			_creatorName = p_creatorName;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[Property("ExtName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ExtName
		{
			get { return _extName; }
			set { _extName = value; }
		}

		[Property("FileSize", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public long? FileSize
		{
			get { return _fileSize; }
			set { _fileSize = value; }
		}

		[Property("Path", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 800)]
		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		[Property("FullId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string FullId
		{
			get { return _fullId; }
			set { _fullId = value; }
		}

		[Property("GroupId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string GroupId
		{
			get { return _groupId; }
			set { _groupId = value; }
		}

		[Property("FolderId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string FolderId
		{
			get { return _folderId; }
			set { _folderId = value; }
		}

		[Property("ModuleId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string ModuleId
		{
			get { return _moduleId; }
			set { _moduleId = value; }
		}

		[Property("IsReference", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsReference
		{
			get { return _isReference; }
			set { _isReference = value; }
		}

		[Property("ReferenceId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string ReferenceId
		{
			get { return _referenceId; }
			set { _referenceId = value; }
		}

		[Property("IsRelate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsRelate
		{
			get { return _isRelate; }
			set { _isRelate = value; }
		}

		[Property("HasRelate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string HasRelate
		{
			get { return _hasRelate; }
			set { _hasRelate = value; }
		}

		[Property("RelateType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string RelateType
		{
			get { return _relateType; }
			set { _relateType = value; }
		}

		[Property("RelateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string RelateId
		{
			get { return _relateId; }
			set { _relateId = value; }
		}

		[Property("IsCompressed", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsCompressed
		{
			get { return _isCompressed; }
			set { _isCompressed = value; }
		}

		[Property("IsEncrypted", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsEncrypted
		{
			get { return _isEncrypted; }
			set { _isEncrypted = value; }
		}

		[Property("IsActiveVersion", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string IsActiveVersion
		{
			get { return _isActiveVersion; }
			set { _isActiveVersion = value; }
		}

		[Property("HasVersion", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string HasVersion
		{
			get { return _hasVersion; }
			set { _hasVersion = value; }
		}

		[Property("Version", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Version
		{
			get { return _version; }
			set { _version = value; }
		}

		[Property("VersionMemo", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string VersionMemo
		{
			get { return _versionMemo; }
			set { _versionMemo = value; }
		}

		[Property("CreateTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; }
		}

		[Property("CreatorId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string CreatorId
		{
			get { return _creatorId; }
			set { _creatorId = value; }
		}

		[Property("CreatorName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CreatorName
		{
			get { return _creatorName; }
			set { _creatorName = value; }
		}

		#endregion

	} // FileItem
}

