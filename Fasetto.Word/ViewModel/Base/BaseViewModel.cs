using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fasetto.Word
{

    public class BaseViewModel : INotifyPropertyChanged
    {
        // 属性更改事件，用于通知界面属性发生变化
        public event PropertyChangedEventHandler? PropertyChanged;
        public  void OnPropertyChange([CallerMemberName] string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }
    }
}
