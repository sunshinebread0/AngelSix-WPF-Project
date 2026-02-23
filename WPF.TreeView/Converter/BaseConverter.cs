using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF.TreeView
{

    public abstract class BaseConverter<T> : MarkupExtension, IValueConverter
        where T : BaseConverter<T>, new()
    {
        public static T? _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
           return _instance ??= new T();
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("单项转换器，不支持ConvertBack");
        }
    }
}
