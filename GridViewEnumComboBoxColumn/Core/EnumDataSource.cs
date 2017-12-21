using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;

namespace GridViewEnumComboBoxColumn.Core
{
    public static class EnumDataSource
    {
        public static Array GetAvailableValues(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var enumType = Nullable.GetUnderlyingType(type) ?? type;
            if (!enumType.IsEnum)
                throw new ArgumentException("Type must be for an Enum.");

            Array parsedValues;
            var actualEnumType = Nullable.GetUnderlyingType(enumType) ?? enumType;
            var enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == enumType)
            {
                parsedValues = enumValues;
            }
            else
            {
                var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
                enumValues.CopyTo(tempArray, 1);
                parsedValues = tempArray;
            }

            return parsedValues;
        }

        public static Dictionary<object, string> GetLocalizedValues(Type type)
        {
            var resource = Application.Current.Resources[type] as ArrayList ?? throw new Exception($"The resource {type.Name} is not of type {typeof(ArrayList)}");
            var resourceDict = resource.Cast<DictionaryEntry>() ?? throw new Exception($"The resource doesn't contain elements of type {typeof(DictionaryEntry)}");

            var result = new Dictionary<object, string>();

            var availableValues = GetAvailableValues(type);
            foreach (var item in availableValues)
            {
                var dictionaryEntry = resourceDict.FirstOrDefault(x => x.Key.Equals(item));
                var displayValue = dictionaryEntry.Value?.ToString() ?? item.ToString();
                result.Add(item, displayValue);
            }

            return result;
        }

        public static List<EnumMemberViewModel> GetLocalizedEnumFilters(Type type)
        {
            var localizedValues = GetLocalizedValues(type);
            var result = new List<EnumMemberViewModel>();
            foreach (var locValue in localizedValues)
            {
                result.Add(new EnumMemberViewModel(locValue.Key, locValue.Key.ToString(), locValue.Value));
            }

            return result;
        }
    }
}