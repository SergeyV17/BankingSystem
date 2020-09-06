using BankingSystem.Models.Implementations.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BankingSystem.Views.Converters
{
    /// <summary>
    /// Класс конвертера ширины колонки в listview ( для скрытия )
    /// </summary>
    class ColumnWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var node = (Node)value;
            var width = double.Parse(parameter as string);

            return node != null ? node.Type == NodeType.Entity || node.Type == NodeType.VIPEntity ? width : 0 : (object)0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
