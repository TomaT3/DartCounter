using System;
using System.Globalization;
using Xamarin.Forms;

namespace DartCounter.View.Converters
{
    public class StringIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int)
            {
                return value.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string stringValue)
            {
                if (Int32.TryParse(stringValue, out int val))
                {
                    return val;
                }
            }

            return 0;
        }
    }
}
