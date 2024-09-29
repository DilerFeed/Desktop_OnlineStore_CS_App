using System;

namespace ShopApp
{
    public class SpecialOrder : InStoreOrder
    {
        public string SpecialRequest { get; set; }

        public override void ProcessOrder()
        {
            Console.WriteLine($"Processing special in-store order at: {StoreAddress} with request: {SpecialRequest}");
        }
    }
}
