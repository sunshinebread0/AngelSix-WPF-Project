namespace WPF.TreeView
{
    /// <summary>
    /// Information about a directory item such as a drive, folder, or file.
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this directory item (drive, folder, or file)
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path of this directory item (e.g. "C:\", "C:\Folder", "C:\Folder\File.txt")
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath: DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
