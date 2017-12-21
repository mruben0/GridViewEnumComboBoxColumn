using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace GridViewEnumComboBoxColumn.Core
{
    public class EnumComboBox : RadComboBox
    {
        public EnumComboBox()
        {
            DefaultStyleKey = typeof(EnumComboBox);
            this.Loaded += new RoutedEventHandler(OnLoaded);
        }

        private static Dictionary<object, string> dictionary { get; set; }
        private static Array Array { get; set; }
        private static object bindedValue { get; set; }

        public static void EnumAdded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            object value = d.GetValue(EnumBindingProperty);
            bindedValue = value;
            dictionary = EnumDataSource.GetLocalizedValues(value.GetType());
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (UseLocalizedValues)
            {
                SetValue(ItemsSourceProperty, dictionary);
                SetValue(DisplayMemberPathProperty, "Value");
                SetValue(SelectedItemProperty, dictionary.First(d => d.Key.Equals(bindedValue)));

                System.Diagnostics.Debug.WriteLine("SelectedItem");
                System.Diagnostics.Debug.WriteLine(SelectedItem);
                System.Diagnostics.Debug.WriteLine(SelectedValue);
            }
            else
            {
                SetValue(ItemsSourceProperty, dictionary);
                SetValue(DisplayMemberPathProperty, "Key");
                SetValue(SelectedItemProperty, dictionary.First(d => d.Key.Equals(bindedValue)));
            }
        }

        #region Properies

        public bool UseLocalizedValues { get; set; }

        public Enum EnumBinding
        {
            get { return (Enum)GetValue(EnumBindingProperty); }
            set { SetValue(EnumBindingProperty, value); }
        }

        public static readonly DependencyProperty EnumBindingProperty =
            DependencyProperty.Register("EnumBinding", typeof(Enum), typeof(EnumComboBox), new PropertyMetadata(default(Enum), EnumAdded));

        #endregion Properies
    }
}

////