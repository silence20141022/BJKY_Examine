using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Castle.ActiveRecord;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Common;

namespace Aim.Portal.FileSystem
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService
    {
        #region 文件记录相关操作

        /// <summary>
        /// 由FileItem获取文件FullID
        /// </summary>
        /// <param name="fitem"></param>
        /// <returns></returns>
        public static string GetFullID(string fileID, string fileName)
        {
            return String.Format("{0}_{1}", fileID, fileName);
        }

        /// <summary>
        /// 由文件全名获取文件名
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFileNameByFullID(string fullid)
        {
            string fname = fullid.Substring(fullid.IndexOf("_"));

            return fname;
        }

        /// <summary>
        /// 由文件全名获取文件标识
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFileIDByFullID(string fullid)
        {
            string fid = fullid.Substring(0, fullid.IndexOf("_"));

            return fid;
        }

        /// <summary>
        /// 由文件全名获取文件路径
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetFilePathByFullID(string fullid)
        {
            string fid = FileService.GetFileIDByFullID(fullid);

            return GetFilePathByFileID(fid);
        }

        // 当文件保存在本机时, 由文件Id获取文件路径
        public static string GetFilePathByFileID(string fileid)
        {
            FileItem fitem = FileItem.Find(fileid);

            return fitem.FilePath;
        }

        /// <summary>
        /// 获取临时上传文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetTemporaryFileName(string fileName)
        {
            return String.Format("{0}_temp", fileName);
        }

        /// <summary>
        /// 获取临时上传文件夹
        /// </summary>
        /// <returns></returns>
        public static string GetTemporaryFolder()
        {
            string folder = System.Configuration.ConfigurationSettings.AppSettings["UploadFolder"];

            if (string.IsNullOrEmpty(folder))
            {
                folder = "Upload";
            }

            return folder;
        }

        /// <summary>
        /// 由fullid获取FileItem
        /// </summary>
        /// <param name="fullId"></param>
        /// <returns></returns>
        public static FileItem GetFileItemByFullID(string fullId)
        {
            string fid = GetFileIDByFullID(fullId);

            FileItem fi = FileItem.Find(fid);

            return fi;
        }

        /// <summary>
        /// 由文件名和文件夹Key创建新的FileItem
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderKey"></param>
        /// <returns></returns>
        public static FileItem CreateFileItem(string filePath, string folderKey)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            FileItem fitem = CreateFileItem(fileInfo, folder);

            return fitem;
        }

        /// <summary>
        /// 创建新的FileItem
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static FileItem CreateFileItem(FileInfo fileInfo, FileFolder folder)
        {
            FileItem fitem = GetNewFileItem(fileInfo, folder);

            fitem.Create();
            fitem.FullId = String.Format("{0}.{1}", folder.FullId, fitem.Id);
            fitem.Update();

            return fitem;
        }

        /// <summary>
        /// 由文件名和文件夹Key创建新的FileItem
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderKey"></param>
        /// <returns></returns>
        public static FileItem GetNewFileItem(string filePath, string folderKey)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            FileItem fitem = GetNewFileItem(fileInfo, folder);

            return fitem;
        }

        /// <summary>
        /// 由FileInfo和FileFolder获取FileItem
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static FileItem GetNewFileItem(FileInfo fileInfo, FileFolder folder)
        {
            FileItem fitem = new FileItem();

            fitem.Name = fileInfo.Name;
            fitem.ExtName = fileInfo.Extension;
            fitem.FileSize = fileInfo.Length;

            fitem.Path = folder.Path;
            fitem.FolderId = folder.Id;
            fitem.ModuleId = folder.ModuleId;

            return fitem;
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 文件全路径
        /// </summary>
        /// <param name="fullid"></param>
        public static void DeleteFileByFullID(string fullid)
        {
            FileItem fi = GetFileItemByFullID(fullid);

            DeleteFile(fi);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fitem"></param>
        [ActiveRecordTransaction]
        public static void DeleteFile(FileItem fitem)
        {
            FileInfo fi = new FileInfo(fitem.FilePath);
            fi.Delete();
            fitem.Delete();
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="fitem"></param>
        /// <returns></returns>
        [ActiveRecordTransaction]
        public static void RenameFile(FileItem fitem, string newName)
        {
            FileInfo fi = new FileInfo(fitem.FilePath);

            fi.MoveTo(String.Format(@"{0}\{1}_{2}", fi.Directory.FullName, fitem.Id, newName));

            fitem.Name = newName;
            fitem.Update();
        }

        /// <summary>
        /// 保存文件流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="folderKey"></param>
        [ActiveRecordTransaction]
        public static FileItem SaveFile(ref Stream stream, string fileName, string folderKey)
        {
            FileItem fitem = null;

            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            // 若路径不存在则新建路径
            if (!Directory.Exists(folder.FolderPath))
            {
                Directory.CreateDirectory(folder.FolderPath);
            }

            FileInfo fileInfo = new FileInfo(Path.Combine(folder.FolderPath, fileName));

            using (FileStream fs = fileInfo.OpenWrite())
            {
                stream.Position = 0;

                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
            }

            fitem = CreateFileItem(fileInfo, folder);

            return fitem;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fitem"></param>
        /// <param name="folderKey"></param>
        [ActiveRecordTransaction]
        public static FileItem CopyFile(FileItem fitem, string folderKey)
        {
            FileItem newfitem = null;

            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            // 若路径不存在则新建路径
            if (!Directory.Exists(folder.FolderPath))
            {
                Directory.CreateDirectory(folder.FolderPath);
            }

            FileInfo fileInfo = new FileInfo(fitem.FilePath);

            newfitem = CreateFileItem(fileInfo, folder);

            File.Copy(fitem.FilePath, Path.Combine(folder.FolderPath, GetFullID(newfitem.Id, newfitem.Name)));

            return newfitem;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fitem"></param>
        /// <param name="folderKey"></param>
        [ActiveRecordTransaction]
        public static void MoveFile(FileItem fitem, string folderKey)
        {
            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            // 若路径不存在则新建路径
            if (!Directory.Exists(folder.FolderPath))
            {
                Directory.CreateDirectory(folder.FolderPath);
            }

            File.Move(fitem.FilePath, Path.Combine(folder.FolderPath, GetFullID(fitem.Id, fitem.Name)));

            fitem.Path = String.Format(@"{0}", folder.Path, folder.Name);
            fitem.FolderId = folder.Id;
            fitem.ModuleId = folder.ModuleId;
            fitem.FullId = String.Format("{0}.{1}", folder.FullId, fitem.Id);

            fitem.Update();
        }

        /// <summary>
        /// 转移临时文件到folderKey对应的文件夹
        /// </summary>
        [ActiveRecordTransaction]
        public static string MoveFile(string srcFilePath, string folderKey)
        {
            string filefullid = String.Empty;

            FileFolder folder = FileFolderRule.GetFolderByKey(folderKey);

            // 若路径不存在则新建路径
            if (!Directory.Exists(folder.FolderPath))
            {
                Directory.CreateDirectory(folder.FolderPath);
            }

            FileInfo fi = new FileInfo(srcFilePath);

            FileItem fitem = CreateFileItem(fi, folder);

            filefullid = GetFullID(fitem.Id, fitem.Name);

            File.Move(srcFilePath, Path.Combine(folder.FolderPath, filefullid));

            return filefullid;
        }

        #endregion
    }
}
