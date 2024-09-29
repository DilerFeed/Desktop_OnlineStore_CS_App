using System;

namespace ShopApp
{
    public class OnlineOrder : Order
    {
        public string DeliveryAddress { get; set; }
        public string DeliveryCompany { get; set; }
        public string PostOfficeNumber { get; set; }

        public override void ProcessOrder()
        {
            Console.WriteLine($"Processing online order to address: {DeliveryAddress}, {DeliveryCompany}, Office No: {PostOfficeNumber}");
        }
    }
}
