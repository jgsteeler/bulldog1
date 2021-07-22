using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace ShoppingCart.Models
{
    public class ProductRepo : IProductRepo
    {

        private Dictionary<int, Product> items;

        public ProductRepo()
        {
            items = new Dictionary<int, Product>();

            string fileName = "./app_data/products.json";
            string jsonString = File.ReadAllText(fileName);
            JsonSerializer.Deserialize<IEnumerable<Product>>(jsonString).ToList().ForEach(p=> AddProduct(p));

          

        }

        public Product this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Product> Products => items.Values;

        public Product AddProduct(Product product)
        {
            if (product.Id == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                product.Id = key;
            }
            items[product.Id] = product;
            return product;
        }


        public IEnumerable<string> GetCategories() => new List<string> { "Sports", "Home", "Outdoors", "Automotive" };
    }



    public interface IProductRepo
    {
        IEnumerable<Product> Products { get; }
        Product this[int id] { get; }
        Product AddProduct(Product product);

        IEnumerable<string> GetCategories();
    }
}

