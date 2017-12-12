using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GridViewEnumComboBoxColumn.Core
{
    class EnumToLocConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumType = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

            if (!enumType.IsEnum)
                throw new ArgumentException("Type must be for an Enum.");

            var resource = Application.Current.Resources[enumType] as ArrayList ?? throw new Exception($"The resource {enumType.Name} is not of type {typeof(ArrayList)}");
            var resourceDict = resource.Cast<DictionaryEntry>() ?? throw new Exception($"The resource doesn't contain elements of type {typeof(DictionaryEntry)}");

            var valueString = resourceDict.FirstOrDefault(x => x.Key.Equals(value)).Value.ToString();

            return valueString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
