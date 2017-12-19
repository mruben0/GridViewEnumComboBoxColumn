using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace GridViewEnumComboBoxColumn.Core
{
    public class EnumComboBox : RadComboBox
    {
        //public Enum EnumType
        //{
        //    get { return (Enum)GetValue(EnumTypeProperty); }
        //    set { SetValue(EnumTypeProperty, value); }
        //}

        //public static readonly DependencyProperty EnumTypeProperty =
        //    DependencyProperty.Register("EnumType", typeof(Type), typeof(EnumComboBox), new PropertyMetadata(default(Enum), PropertyChangedCallback));

        //private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var combobox = d as EnumComboBox;
        //    if (combobox.UseLocalizedValues)
        //    {
        //        if (!(e.NewValue.GetType()).IsEnum) throw new ArgumentException("Type must be enum");

        //        combobox.DisplayMemberPath = "Value";
        //        combobox.SelectedValuePath = "Key";

        //        combobox.ItemsSource = EnumDataSource.GetLocalizedValues(e.NewValue.GetType());
        //    }
        //}

        public EnumComboBox()
        {
            DefaultStyleKey = typeof(EnumComboBox);
        }

        public bool UseLocalizedValues { get; set; }

        private bool _busy = false;

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (!_busy && UseLocalizedValues)
            {
                _busy = true;

                // if (!(newValue.GetType()).IsEnum) throw new ArgumentException("Type must be enum");
                Type type;

                List<Type> list = new List<Type>();

                foreach (var item in newValue)
                {
                    list.Add(item.GetType().GetProperty(DisplayMemberPath).PropertyType);
                }

                DisplayMemberPath = "Value";
                SelectedValuePath = "Key";

                ItemsSource = EnumDataSource.GetLocalizedValues(list.ElementAt(0));
            }
            else
                base.OnItemsSourceChanged(oldValue, newValue);
        }
    }
}