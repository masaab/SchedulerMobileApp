using System;
using System.Globalization;
using Xamarin.Forms;

namespace Scheduler.Converters
{
    public class SelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SelectedItemChangedEventArgs arg)
                return arg.SelectedItem;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
