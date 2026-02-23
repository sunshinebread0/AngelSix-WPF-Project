using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF.TreeView
{
    public class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drives on the computer and return them as a list of DirectoryItem objects.
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives() {
            //Get logical drives and add to tree view
            return Directory.GetLogicalDrives().Select(drive => (new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive})).ToList();
        }

        /// <summary>
        /// Get the directories top-level contents
        /// </summary>
        /// <param name="fullPath">The full path of the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();
            #region Get Folders
            //try to get folders from the folder
            try
            {
                var directories = Directory.GetDirectories(fullPath);
                if (directories.Length > 0)
                    items.AddRange(directories.Select(directory => new DirectoryItem { FullPath = directory, Type = DirectoryItemType.Folder }));
            }
            catch { }
            #endregion

            #region Get Files
            //try to get files from the folder
            try
            {
                var files = Directory.GetFiles(fullPath);
                if (files.Length > 0)
                    items.AddRange(files.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }
            #endregion
            return items;
        }

        #region Helper Methods

        /// <summary>
        /// 从给定的路径中提取最后一级的文件或文件夹名称。
        /// 例如："C:\Folder\Sub" -> "Sub"
        /// 该方法手动处理分隔符并返回最后一个分隔符之后的子串。
        /// 返回空字符串的情况：path 为 null 或 空字符串。
        /// </summary>
        public static string GetFileFolderName(string path)
        {
            // 如果 path 为空或 null，直接返回空字符串
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // 将可能出现的正斜杠 '/' 统一替换为反斜杠 '\'，便于后面查找最后一个分隔符位置
            var normalizedPath = path.Replace('/', '\\');

            // 查找最后一个反斜杠的位置（路径分隔符）
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // 如果没有找到分隔符（lastIndex == -1）或分隔符在字符串开始处（lastIndex == 0），
            // 则说明传入的 path 本身已经是最后一级名称或格式异常，直接返回原始 path
            if (lastIndex <= 0)
                return path;

            // 返回最后一个分隔符之后的子串，即文件或文件夹名
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
