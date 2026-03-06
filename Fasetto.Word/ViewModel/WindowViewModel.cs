using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fasetto.Word.ViewModel
{
    /// <summary>
    /// 自定义平面窗口的ViewModel
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member

        private Window mwindow;

        private int mOuterMarginSize = 10;

        private int mWindowRadius = 10;

        #endregion

        #region Properties

        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder+OutMarginSize); } }

        public int OutMarginSize 
        {
            get
            {
                return mwindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OutMarginSizeThickness { get { return new Thickness(OutMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return mwindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight+ResizeBorder); } }

        #endregion

        #region Constructor

        public WindowViewModel(Window window)
        {
            mwindow = window;

            mwindow.StateChanged += (sender, e) =>
            {
                OnPropertyChange(nameof(ResizeBorderThickness));
                OnPropertyChange(nameof(OutMarginSize));
                OnPropertyChange(nameof(OutMarginSizeThickness));
                OnPropertyChange(nameof(WindowRadius));
                OnPropertyChange(nameof(WindowCornerRadius));
            };
        }

        #endregion
    }
}
