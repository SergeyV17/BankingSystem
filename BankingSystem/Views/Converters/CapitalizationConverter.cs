using System;
using System.Globalization;
using System.Windows.Data;

namespace BankingSystem.Views.Converters
{
    class CapitalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool capitalization = (bool)value;

            return capitalization ? "Подключена" : "Отсутствует";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
