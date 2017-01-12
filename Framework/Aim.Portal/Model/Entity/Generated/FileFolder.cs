namespace Aim.Portal.Model
{
	// Business class FileFolder generated from FileFolder
	// Administrator [2010-04-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("FileFolder")]
	public partial class FileFolder 
		: EntityBase<FileFolder> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_Name = "Name";
		public static string Prop_FolderKey = "FolderKey";
		public static string Prop_Path = "Path";
		public static string Prop_FullId = "FullId";
		public static string Prop_ParentId = "ParentId";
		public static string Prop_ModuleId = "ModuleId";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_RootPath = "RootPath";

		#endregion

		#region Private_Variables

		private string _id;
		private string _name;
		private string _folderKey;
		private string _path;
		private string _fullId;
		private string _parentId;
		private string _moduleId;
		private DateTime? _createTime;
		private string _rootPath;


		#endregion

		#region Constructors

		public FileFolder()
		{
		}

		public FileFolder(
			string p_id,
			string p_name,
			string p_folderKey,
			string p_path,
			string p_fullId,
			string p_parentId,
			string p_moduleId,
			DateTime? p_createTime,
			string p_rootPath)
		{
			_id = p_id;
			_name = p_name;
			_folderKey = p_folderKey;
			_path = p_path;
			_fullId = p_fullId;
			_parentId = p_parentId;
			_moduleId = p_moduleId;
			_createTime = p_createTime;
			_rootPath = p_rootPath;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[Property("FolderKey", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string FolderKey
		{
			get { return _folderKey; }
			set { _folderKey = value; }
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

		[Property("ParentId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		[Property("ModuleId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 32)]
		public string ModuleId
		{
			get { return _moduleId; }
			set { _moduleId = value; }
		}

		[Property("CreateTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; }
		}

		[Property("RootPath", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string RootPath
		{
			get { return _rootPath; }
			set { _rootPath = value; }
		}

		#endregion

	} // FileFolder
}

