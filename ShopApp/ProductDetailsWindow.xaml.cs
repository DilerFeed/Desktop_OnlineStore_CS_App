using ShopApp;
using System.Windows;

namespace ShopApp
{
    public partial class ProductDetailsWindow : Window
    {
        private Product _product;

        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            DataContext = product;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartService.AddToCart(_product);
            this.Close();
        }
    }
}