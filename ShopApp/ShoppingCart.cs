using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp
{
    public class ShoppingCart
    {

        public System.Collections.Generic.List<ShopApp.Product> Products
        {
            get => default;
            set
            {
            }
        }

        public void AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveProduct(string product)
        {
            throw new System.NotImplementedException();
        }
    }
}