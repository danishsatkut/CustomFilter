using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomFilteringForRadGridView.CustomFilter
{
    public class StringToIntConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = value as string;

            int returnValue;
            var success = int.TryParse(id, out returnValue);

            return (success) ? returnValue : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
