using DartCounter.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DartCounter.View.Converters
{
    public class MultiplikatorBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Multiplikator multiplikatorParameter = (Multiplikator)parameter;
            if (value is Multiplikator buttonValue)
            {
                if (buttonValue == multiplikatorParameter)
                {
                    return Color.Green;
                }
            }

            return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
