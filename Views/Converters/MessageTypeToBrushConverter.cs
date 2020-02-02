using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.Views.Converters
{
    public class MessageTypeToBrushConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var messageTypeColors = new Dictionary<MessageTypes, Brush>()
            {
                {  MessageTypes.SUCCESS, Brushes.DarkGreen },
                {  MessageTypes.ERROR, Brushes.DarkRed }
            };

            if (!(value is MessageTypes messageType) || !messageTypeColors.ContainsKey(messageType))
            {
                return null;
            }
            
            return messageTypeColors[messageType];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
