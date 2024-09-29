using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Specifications")]
        public string Specifications { get; set; }

        public Product(string name, decimal price, string imageUrl, string description, string specifications)
        {
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Description = description;
            Specifications = specifications;
        }
    }

}