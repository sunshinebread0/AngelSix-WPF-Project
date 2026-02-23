using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.TreeView
{
    /// <summary>
    /// The type of a directory item
    /// </summary>
    public enum DirectoryItemType
    {
        /// <summary>
        /// A logical drive such as "C:\"
        /// </summary>
        Drive,

        /// <summary>
        /// A directory such as "C:\Folder"
        /// </summary>
        Folder,

        /// <summary>
        /// A phyiscal file such as "C:\Folder\File.txt"
        /// </summary>
        File
    }
}
