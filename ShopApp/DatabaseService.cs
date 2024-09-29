using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ShopApp
{
    public class DatabaseService
    {
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<Order> _ordersCollection;

        public DatabaseService()
        {
            // Connection to MongoDB
            var client = new MongoClient("");
            var database = client.GetDatabase("online_shop");

            // Get collectrion of products
            _productsCollection = database.GetCollection<Product>("products");

            // Get collection of order
            _ordersCollection = database.GetCollection<Order>("orders");
        }

        // Method to get all products
        public List<Product> GetProducts()
        {
            return _productsCollection.Find(product => true).ToList();
        }

        // Method to add new product to DB
        public void AddProduct(Product product)
        {
            _productsCollection.InsertOne(product);
        }

        // Method to add new order to DB
        public void AddOrder(Order order)
        {
            _ordersCollection.InsertOne(order);
        }
    }
}
