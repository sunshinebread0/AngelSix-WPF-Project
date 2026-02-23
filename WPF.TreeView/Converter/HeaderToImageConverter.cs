using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace WPF.TreeView
{
    /// <summary>
    /// 将树视图项的 Header（文件/文件夹路径）转换为 BitmapImage。
    /// 用于在 TreeView 中显示不同的图标（驱动器、文件、文件夹）。
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : BaseConverter<HeaderToImageConverter>
    {
        /// <summary>
        /// 将传入的路径字符串转换为对应的图标 BitmapImage。
        /// value: 期望为 string（完整路径），例如 "C:\\" 或 "C:\Users\file.txt"。
        /// targetType/parameter/culture: 按 IValueConverter 约定传入（本实现未使用）。
        /// 返回：BitmapImage 或 null（当输入为空时）。
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = (DirectoryItemType)value switch
            {
                DirectoryItemType.Drive  => "Images/drive.png",
                DirectoryItemType.Folder => "Images/folder-closed.png",
                _                        => "Images/file.png"
            };
            // 使用 WPF 的 pack URI 语法从应用程序资源中加载图像并返回 BitmapImage
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }
    }
}
