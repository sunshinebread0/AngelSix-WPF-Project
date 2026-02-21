using WPF.Basics.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Basics
{
    public class Bootstrapper : BootstrapperBase
    {
        //依赖注入容器
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        //注册服务
        protected override void Configure()
        {
            _container.Instance(_container);
            _container
              .Singleton<IWindowManager, WindowManager>()
              .Singleton<IEventAggregator, EventAggregator>();

            //批量注册所有ViewModel
            foreach (var assembly in SelectAssemblies())
            {
                assembly.GetTypes()
                  .Where(type => type.IsClass)
                  .Where(type => type.Name.EndsWith("ViewModel"))
                  .ToList()
                  .ForEach(viewModeltype =>
                  {
                      Debug.WriteLine($"Registering ViewModel: {viewModeltype.Name}");
                      _container.RegisterPerRequest(
                  viewModeltype, viewModeltype.ToString(), viewModeltype);
                  });
            }
        }

        //程序入口
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            IoC.Get<SimpleContainer>();
            await DisplayRootViewForAsync(typeof(MainViewModel));
        }

        //获取单个实例
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        //获取多个实例
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        //为已创建的实例补充注入依赖
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
