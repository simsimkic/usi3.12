using HealthcareInstitution.Core.Equipment.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthcareInstitution.Utilities.Converters
{
    public class EquipmentQuantityConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return null;

            if (value.ToString() == "Any")
                return null;

            if (value.ToString() == "Not available")
                return new QuantityRange(0, 0);

            return value.ToString() == "10+" ? new QuantityRange(10, int.MaxValue) : new QuantityRange(0, 10);
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.ToString() == "Any")
                return null;

            if (value.ToString() == "Not available")
                return new QuantityRange(0, 0);

            return value.ToString() == "10+" ? new QuantityRange(10, int.MaxValue) : new QuantityRange(0, 10);
        }
    }
}
