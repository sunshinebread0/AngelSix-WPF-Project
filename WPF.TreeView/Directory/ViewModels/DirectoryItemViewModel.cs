using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Windows.Input;

namespace WPF.TreeView
{
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

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
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        public bool IsExpanded
        {
            get { return this.Children?.Count(f => f != null) > 0; }
            set
            {
                if (value == true)
                {
                    Expand();
                }
                else
                {
                    this.ClearChildren();
                }
            }
        }

        #endregion

        #region Public Commands

        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.FullPath = fullPath;
            this.Type = type;

            this.ClearChildren();
        }

        #endregion

        #region Helper Methods
        /// <summary>
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            if (this.Type != DirectoryItemType.File)
            {
                this.Children.Add(null);
            }
        }

        #endregion

        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            var contents = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                contents .Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
