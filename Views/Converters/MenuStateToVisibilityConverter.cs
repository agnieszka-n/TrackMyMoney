using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TrackMyMoney.Common;

namespace TrackMyMoney.Views.Converters
{
    internal class MenuStateToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CostsListMenuState actualState && parameter is CostsListMenuState expectedState))
            {
                return Binding.DoNothing;
            }

            if (actualState == expectedState)
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
