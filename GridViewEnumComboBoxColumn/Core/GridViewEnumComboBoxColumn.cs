using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace GridViewEnumComboBoxColumn.Core
{
    internal class GridViewEnumComboBoxColumn : GridViewComboBoxColumn
    {
        public bool UseLocalizedValues { get; set; }

        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            if (UseLocalizedValues)
            {
                this.DisplayMemberPath = "Value";
                this.SelectedValueMemberPath = "Value";
                ItemsSourceBinding = new Binding() { Source = EnumDataSource.GetLocalizedValues(DataType) };
            }
            else
            {
                ItemsSource = EnumDataSource.GetAvailableValues(DataType);
            }

            return base.CreateCellElement(cell, dataItem);
        }

        public override Binding DataMemberBinding
        {
            get => base.DataMemberBinding;
            set
            {
                if (UseLocalizedValues)
                {
                    value.Converter = new EnumToLocConverter();
                }

                base.DataMemberBinding = value;
            }
        }

        public override FrameworkElement CreateFieldFilterEditor()
        {
            if (UseLocalizedValues)
            {
                var type = DataType;
                RadComboBox radComboBox = new RadComboBox
                {
                    SelectedValuePath = "Value",
                    DisplayMemberPath = "DisplayName"
                };

                List<EnumMemberViewModel> list = new List<EnumMemberViewModel>
                {
                    new EnumMemberViewModel(OperatorValueFilterDescriptorBase.UnsetValue, "Unset", string.Empty)
                };

                list.AddRange(EnumDataSource.GetLocalizedEnumFilters(type));
                radComboBox.ItemsSource = list;
                Binding binding = new Binding("Value")
                {
                    Mode = BindingMode.TwoWay
                };
                radComboBox.SetBinding(Selector.SelectedValueProperty, binding);
                return radComboBox;
            }

            return base.CreateFieldFilterEditor();
        }
    }
}