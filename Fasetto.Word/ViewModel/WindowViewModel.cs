using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fasetto.Word.ViewModel
{
    /// <summary>
    /// 自定义平面窗口的ViewModel
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member

        //主窗口对象，用于获取窗口状态等信息
        private Window _window;

        //窗口外边距尺寸
        private int _OuterMarginSize = 10;

        //窗口圆角半径
        private int _WindowRadius = 10;

        #endregion

        #region Public Properties

        /// <summary>
        /// 窗口的最小宽度
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// 窗口的最小高度
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// 窗口可调整大小的边框宽度。
        /// 用于控制窗口边缘可拖拽调整大小的区域。
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        ///用于 XAML 绑定使用的 Thickness 类型边框属性，
        ///用于在保证拖拽区域的同时，叠加主窗口的外边距以实现更好的交互效果。
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder+OutMarginSize); } }

        /// <summary>
        /// 主窗口内部内容的边距，等于调整大小边框宽度加上外边距尺寸，
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder + OutMarginSize); } }

        /// <summary>
        /// 最外层Border的内边距，也就是主窗口的外边距。
        /// 当窗口最大化时返回 0，否则返回设置的边距值。
        /// 用于去除边距以避免贴边时出现留白
        /// </summary>
        public int OutMarginSize 
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _OuterMarginSize;
            }
            set
            {
                _OuterMarginSize = value;
            }
        }

        /// <summary>
        ///用于 XAML 绑定使用的 Thickness 类型边距属性
        /// </summary>
        public Thickness OutMarginSizeThickness { get { return new Thickness(OutMarginSize); } }

        /// <summary>
        /// 主窗口圆角半径。
        /// 当窗口最大化时返回 0，否则返回设置的圆角值。
        /// 用于去除圆角以避免贴边时出现视觉问题。
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _WindowRadius;
            }
            set
            {
                _WindowRadius = value;
            }
        }

        /// <summary>
        /// 用于 XAML 绑定的 CornerRadius 属性
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// 用于 XAML 绑定的 GridLength 类型标题栏高度属性
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight+ResizeBorder); } }

        #endregion

        #region Commands

        /// <summary>
        /// 最小化命令
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// 最大化命令
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// 菜单命令
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 当窗口尺寸改变时，通知相关属性更新，以便界面能够正确调整边距和圆角等视觉效果。
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            _window = window;

            _window.StateChanged += (sender, e) => RefreshWindowProperties();

            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() =>GetMousePosition());
        }

        #endregion

        #region

        /// <summary>
        /// 当窗口状态改变时，刷新相关属性以确保界面正确更新。
        /// </summary>
        private void RefreshWindowProperties()
        {
            OnPropertyChange(nameof(ResizeBorderThickness));
            OnPropertyChange(nameof(OutMarginSize));
            OnPropertyChange(nameof(OutMarginSizeThickness));
            OnPropertyChange(nameof(WindowRadius));
            OnPropertyChange(nameof(WindowCornerRadius));
        }

        /// <summary>
        /// 获取当前鼠标在窗口中的位置，考虑到窗口最大化时的坐标转换，以确保在不同窗口状态下都能正确获取鼠标位置。
        /// </summary>
        /// <returns></returns>
        private void GetMousePosition()
        {
            // 获取光标相对于窗口左上角的X、Y坐标
            var MaxPos = Mouse.GetPosition(_window);

            //_window.Left和_window.Top分别是窗口Normal状态左上角相对于屏幕的坐标，所以在Normal时需要加上这两个值来转换为相对于屏幕的坐标
            var NormalPos = new Point(MaxPos.X+_window.Left,MaxPos.Y+_window.Top);

            //最大化时坐标是相对于窗口的，所以直接使用；非最大化时坐标是相对于屏幕的，所以需要加上窗口的左上角坐标进行转换
            if (_window.WindowState == WindowState.Maximized)
            {
                //显示窗口的系统菜单，传入 (窗口, 相对于屏幕左上角的位置) 以确保菜单在正确的位置弹出
                SystemCommands.ShowSystemMenu(_window, MaxPos);
            }
            else
            {
                SystemCommands.ShowSystemMenu(_window, NormalPos);
            }
        }

        #endregion
    }
}
