using ShopApp;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopApp
{
    public partial class MainWindow : Window
    {
        private DatabaseService _databaseService;
        public List<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            // Load products from MongoDB
            Products = _databaseService.GetProducts();
            ProductsListView.ItemsSource = Products;

        }

        private void ViewProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                ProductDetailsWindow detailsWindow = new ProductDetailsWindow(selectedProduct);
                detailsWindow.ShowDialog();
            }
        }

        // Double click on list element handler
        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                ProductDetailsWindow detailsWindow = new ProductDetailsWindow(selectedProduct);
                detailsWindow.ShowDialog();
            }
        }

        private void ViewCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow();
            cartWindow.ShowDialog();
        }

    }

}