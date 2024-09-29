using System;

namespace ShopApp
{
    public class InStoreOrder : Order
    {
        public string StoreAddress { get; set; }
        public bool IsSpecialOrder { get; set; }

        public override void ProcessOrder()
        {
            Console.WriteLine($"Processing in-store order at: {StoreAddress}");
        }
    }
}
