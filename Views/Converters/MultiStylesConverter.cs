using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TrackMyMoney.Views.Converters
{
    /// <summary>
    /// Combines multiple styles by adding/overwriting their setters.
    /// </summary>
    internal class MultiStylesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var styles = values.OfType<Style>();

            Style result = new Style();
            foreach (var style in styles)
            {
                foreach (var setter in style.Setters)
                {
                    if (!(setter is Setter propertySetter))
                    {
                        continue;
                    }

                    var addedSetterForCurrentProperty = result.Setters.OfType<Setter>()
                        .FirstOrDefault(x => x.Property == propertySetter.Property);

                    if (addedSetterForCurrentProperty == null)
                    {
                        result.Setters.Add(new Setter(propertySetter.Property, propertySetter.Value));
                    }
                    else
                    {
                        addedSetterForCurrentProperty.Value = propertySetter.Value;
                    }
                }
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
