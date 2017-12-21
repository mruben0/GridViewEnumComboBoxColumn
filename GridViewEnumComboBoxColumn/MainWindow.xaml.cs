using GridViewEnumComboBoxColumn.Example;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GridViewEnumComboBoxColumn
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }
        public ProductType type { get; set; }

        public MainWindow()
        {
            var products = new List<Product>()
            {
                new Product(){Name="PR1", ProductType = ProductType.Egg},
                new Product() {Name = "PR2", ProductType = ProductType.Milks},
                new Product() {Name = "PR3", ProductType = ProductType.Milks},
                new Product() {Name = "PR4", ProductType = ProductType.Milks},
                new Product() {Name = "PR5", ProductType = ProductType.Egg},
                new Product() {Name = "PR6", ProductType = ProductType.Milks},
            };
            Products = new ObservableCollection<Product>(products);

            type = ProductType.Egg;

            DataContext = this;
            InitializeComponent();
        }
    }
}