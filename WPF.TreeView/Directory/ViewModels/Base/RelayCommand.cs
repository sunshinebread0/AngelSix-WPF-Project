using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.TreeView
{
    // 实现ICommand接口的命令类，用于WPF命令绑定
    public class RelayCommand : ICommand
    {
        // 执行命令的委托
        private readonly Action _execute;

        // 命令可执行状态变化事件
        public event EventHandler? CanExecuteChanged;

        // 构造函数，传入执行方法和可执行判断方法
        public RelayCommand(Action action)
        {
            _execute = action;
        }

        // 判断命令是否可执行
        public bool CanExecute(object? parameter)
            =>  true;

        // 执行命令
        public void Execute(object? parameter)
            => _execute();
    }
}
