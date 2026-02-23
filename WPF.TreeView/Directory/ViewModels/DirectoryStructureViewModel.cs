using System.Collections.ObjectModel;

namespace WPF.TreeView
{
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region Constructor

        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();

            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        #endregion
    }
}
