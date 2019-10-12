using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.Converters
{
    internal class IsCategorySelectionEditableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ManageCategoriesMenuState menuState))
            {
                return Binding.DoNothing;
            }

            return menuState != ManageCategoriesMenuState.RENAME && menuState != ManageCategoriesMenuState.DELETE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
