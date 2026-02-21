using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Basics.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            foreach (var item in WorkCenters)
            {
                item.OnChanged -= UpdateLengthText;
                item.OnChanged += UpdateLengthText;
            }
            SelectedFinish = FinishOptions[0];
        }

        #region ChekBox
        public class WorkCenterItem : PropertyChangedBase
        {
            public string Name { get; set; }

            public event System.Action OnChanged;
            private bool _isChecked;
            public bool IsChecked
            {
                get => _isChecked;
                set
                {
                    if (Set(ref _isChecked, value))
                        OnChanged?.Invoke();
                }
            }
        }

        public BindableCollection<WorkCenterItem> WorkCenters { get; } = new BindableCollection<WorkCenterItem>
        {
            new WorkCenterItem { Name = "Weld" },
            new WorkCenterItem { Name = "Assembly" },
            new WorkCenterItem { Name = "Plasma" },
            new WorkCenterItem { Name = "Laser" },
            new WorkCenterItem { Name = "Purchase" },
            new WorkCenterItem { Name = "Lathe" },
            new WorkCenterItem { Name = "Drill" },
            new WorkCenterItem { Name = "Fold" },
            new WorkCenterItem { Name = "Roll" },
            new WorkCenterItem { Name = "Saw" }
        };
        #endregion

        #region Properties
        public BindableCollection<string> FinishOptions { get; } = new BindableCollection<string>
        {
            "Painted",
            "Not Painted",
        };

        private string _selectedFinish;

        public string SelectedFinish
        {
            get => _selectedFinish;
            set
            {
                if (Set(ref _selectedFinish, value))
                    UpdateNoteText();
            }
        }

        private string _noteText;

        public string NoteText
        {
            get => _noteText;
            set => Set(ref _noteText, value);
        }

        private string _massText;

        public string MassText
        {
            get => _massText;
            set => Set(ref _massText, value);
        }

        private string _supplierText;

        public string SupplierText
        {
            get => _supplierText;
            set
            {
                if (Set(ref _supplierText, value))
                    MassText = value;

            }
        }

        private string _description = "yamy";

        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _lengthText;

        public string LengthText
        {
            get => _lengthText;
            set => Set(ref _lengthText, value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show MessageBox with Description
        /// </summary>
        public void Apply()
        {
            MessageBox.Show($"Description: {Description}");
        }

        /// <summary>
        /// Reset CheckBox
        /// </summary>
        public void Reset()
        {
            foreach (var item in WorkCenters)
            {
                item.IsChecked = false;
            }
        }

        public void UpdateLengthText()
        {
            var selectedName = WorkCenters.Where(x => x.IsChecked).Select(x => x.Name);
            LengthText = string.Join(",", selectedName);
        }

        public void UpdateNoteText()
        {
            NoteText = SelectedFinish;
        }
        #endregion
    }
}
