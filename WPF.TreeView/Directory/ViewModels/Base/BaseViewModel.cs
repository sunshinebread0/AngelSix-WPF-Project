
using PropertyChanged;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WPF.TreeView
{

    // 基础视图模型类，提供属性更改通知功能
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        // 属性更改事件，用于通知界面属性发生变化
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
