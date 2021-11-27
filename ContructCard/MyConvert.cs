using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace ContructCard
{
    public class MyConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Value = value.ToString();
            int Count = Int32.Parse(parameter.ToString());
            for (int i = 0; i < Count - 1; i++)
                if(Value.Length < Count)
                    Value = "0" + Value;

            return Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public class MyConverterSharp : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Value = value.ToString();
            for (int i = 0; i < 2; i++)
                if (Value.Length < 3)
                    Value = "0" + Value;

            return "#" + Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
