using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Fasetto.Word
{
    /// <summary>
    /// Converter基类，提供单例实例和抽象的Convert方法，方便子类实现值转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseConverter<T> : MarkupExtension, IValueConverter
        where T : BaseConverter<T>, new()
    {
        //泛型单例
        public static T? _instance;

        //实现MarkupExtension的ProvideValue方法，返回单例实例
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
           return _instance ??= new T();
        }

        //强制子类实现IValueConverter的Convert方法，调用抽象的Convert方法
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        //子类可以选择性实现ConvertBack方法，默认抛出异常表示不支持双向转换
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("单项转换器，不支持ConvertBack");
        }
    }
}
