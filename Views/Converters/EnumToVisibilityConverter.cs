using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TrackMyMoney.Common;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.Converters
{
    internal class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum actualValue && parameter is Enum expectedValue))
            {
                return Binding.DoNothing;
            }

            if (Equals(actualValue, expectedValue))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
