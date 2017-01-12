namespace Aim.Portal.Model
{
	// Business class FileModule generated from FileModule
	// Administrator [2010-04-08] Created

	using System;
	using System.ComponentModel;
	using Castle.ActiveRecord;
	using Aim.Data;

	[ActiveRecord("FileModule")]
	public partial class FileModule 
		: EntityBase<FileModule> 
	{

		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_Name = "Name";
		public static string Prop_FsKey = "FsKey";
		public static string Prop_Description = "Description";
		public static string Prop_StoreMode = "StoreMode";
		public static string Prop_RootPath = "RootPath";
		public static string Prop_Server = "Server";
		public static string Prop_Port = "Port";
		public static string Prop_FolderId = "FolderId";

		#endregion

		#region Private_Variables

		private string _id;
		private string _name;
		private string _fsKey;
		private string _description;
		private string _storeMode;
		private string _rootPath;
		private string _server;
		private string _port;
		private string _folderId;


		#endregion

		#region Constructors

		public FileModule()
		{
		}

		public FileModule(
			string p_id,
			string p_name,
			string p_fsKey,
			string p_description,
			string p_storeMode,
			string p_rootPath,
			string p_server,
			string p_port,
			string p_folderId)
		{
			_id = p_id;
			_name = p_name;
			_fsKey = p_fsKey;
			_description = p_description;
			_storeMode = p_storeMode;
			_rootPath = p_rootPath;
			_server = p_server;
			_port = p_port;
			_folderId = p_folderId;
		}

		#endregion

		#region Properties

        [PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[Property("FsKey", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string FsKey
		{
			get { return _fsKey; }
			set { _fsKey = value; }
		}

		[Property("Description", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		[Property("StoreMode", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string StoreMode
		{
			get { return _storeMode; }
			set { _storeMode = value; }
		}

		[Property("RootPath", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string RootPath
		{
			get { return _rootPath; }
			set { _rootPath = value; }
		}

		[Property("Server", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Server
		{
			get { return _server; }
			set { _server = value; }
		}

		[Property("Port", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string Port
		{
			get { return _port; }
			set { _port = value; }
		}

		[Property("FolderId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 8)]
		public string FolderId
		{
			get { return _folderId; }
			set { _folderId = value; }
		}

		#endregion

	} // FileModule
}

