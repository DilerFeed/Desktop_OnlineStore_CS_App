using ShopApp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShopApp
{
    public static class CartService
    {
        private static ObservableCollection<Product> _cartItems = new ObservableCollection<Product>();

        public static event EventHandler CartChanged;

        public static ObservableCollection<Product> GetCartItems()
        {
            return _cartItems;
        }

        public static void AddToCart(Product product)
        {
            _cartItems.Add(product);
        }

        public static void RemoveFromCart(Product product)
        {
            _cartItems.Remove(product);
        }

        public static void ClearCart()
        {
            _cartItems.Clear();
            // Calling event to notify about cart change
            CartChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
