using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthcareInstitution.Utilities.Converters
{
    public class DimensionsConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            if (value.ToString() == string.Empty)
                return 0;

            int parsedValue;
            if (int.TryParse(value.ToString(), out parsedValue))
                return parsedValue;

            return 0;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            if (value.ToString() == string.Empty)
                return 0;

            int parsedValue;
            if (int.TryParse(value.ToString(), out parsedValue))
                return parsedValue;

            return 0;
        }
    }
}
