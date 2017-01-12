
using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
	
namespace Aim.Portal.Model
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
	public partial class FileItem
    {
        #region 私有成员

        private FileModule _FileModule = null;

        private FileFolder _Folder = null;

        #endregion

        #region 成员属性

        /// <summary>
        /// 文件的所在模块
        /// </summary>
        [JsonIgnore]
        public FileModule FileModule
        {
            get
            {
                if (_FileModule == null && !String.IsNullOrEmpty(this.ModuleId))
                {
                    _FileModule = FileModule.Find(this.ModuleId);
                }

                return _FileModule;
            }
        }

        /// <summary>
        /// 文件所在文件夹
        /// </summary>
        [JsonIgnore]
        public FileFolder Folder
        {
            get
            {
                if (_Folder == null && !String.IsNullOrEmpty(this.FolderId))
                {
                    _Folder = FileFolder.Find(this.FolderId);
                }

                return _Folder;
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        [JsonIgnore]
        public string FilePath
        {
            get
            {
                string filePath = String.Format(@"{0}\{1}\{2}_{3}", FileModule.RootPath.TrimEnd('\\'), this.Path.Trim('\\'), this.Id, this.Name);

                return filePath;
            }
        }
        
        #endregion

        #region 重载

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            /*
            // 检查是否存在重复键
            if (!this.IsPropertyUnique("FolderKey"))
            {
                throw new RepeatedKeyException("存在重复的 键 “" + this.FolderKey + "”");
            }
             * */
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();

            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();

            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            base.Delete();
        }

        #endregion

    } // FileFolder
}


