using System;
using System.Collections.ObjectModel;

namespace ShopApp
{
    public abstract class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public abstract void ProcessOrder();
        public virtual void ProcessPayment(string paymentMethod)
        {
            Console.WriteLine($"Processing {paymentMethod} payment...");
        }
    }
}
