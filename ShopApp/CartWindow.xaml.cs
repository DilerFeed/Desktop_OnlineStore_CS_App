using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            UpdateCart();

            // Subscribe on cart change event
            CartService.CartChanged += CartService_CartChanged;
        }

        private void CartService_CartChanged(object sender, EventArgs e)
        {
            // Update UI in main thread
            Dispatcher.Invoke(() =>
            {
                UpdateCart();
            });
        }

        // Update cart content
        private void UpdateCart()
        {
            CartListView.ItemsSource = CartService.GetCartItems();
            UpdateTotalPrice();
        }

        // Update total cart products price
        public void UpdateTotalPrice()
        {
            var totalPrice = CartService.GetCartItems().Sum(item => item.Price);
            if (totalPrice > 0)
            {
                TotalPriceTextBlock.Text = $"Total: {totalPrice.ToString("C", new System.Globalization.CultureInfo("en-US"))}";
            }
            else
            {
                TotalPriceTextBlock.Text = "Your cart is empty";
            }
        }

        // Order checkouting logic
        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Open checkout window
            var checkoutWindow = new CheckoutWindow();
            checkoutWindow.ShowDialog();
        }

        // Product from cart deleting logic
        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.Tag;
            CartService.RemoveFromCart(product);
            UpdateCart();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CartService.CartChanged -= CartService_CartChanged;
        }
    }
}
