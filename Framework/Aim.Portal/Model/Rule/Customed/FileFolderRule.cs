// Business class FileFolderRule generated from FileFolderRule
// Creator: Ray
// Created Date: [2010-04-10]

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using Newtonsoft.Json.Linq;
using Aim.Data;
using Aim.Portal.Model;
	
namespace Aim.Portal.Model
{
	public partial class FileFolderRule
	{
        #region 静态成员

        /// <summary>
        /// 由键获取文件夹
        /// </summary>
        /// <param name="folderKey"></param>
        /// <returns></returns>
        public static FileFolder GetFolderByKey(string folderKey)
        {
            FileFolder folder = FileFolder.FindFirstByProperties("FolderKey", folderKey);

            return folder;
        }

		#endregion

    } // FileFolderRule
}

