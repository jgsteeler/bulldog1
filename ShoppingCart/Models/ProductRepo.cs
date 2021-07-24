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
        private const string ProductsFile = "./app_data/products.json";
        
        
        private Dictionary<int, Product> _items;
       
        

        public ProductRepo()
        {
            _items = new Dictionary<int, Product>();
           
            

            JsonSerializer.Deserialize<IEnumerable<Product>>(File.ReadAllText(ProductsFile)).ToList().ForEach(p => AddProduct(p));
            
            


        }

        public Product this[int id] => _items.ContainsKey(id) ? _items[id] : null;
        
        public IEnumerable<Product> Products => _items.Values;

       

        public Product AddProduct(Product product)
        {


            if (product.Id == 0)
            {
                int key = _items.Count;
                while (_items.ContainsKey(key)) { key++; };
                product.Id = key;
            }
            _items[product.Id] = product;
            return product;
        }

        
        

    }

  

    public interface IProductRepo
    {
        IEnumerable<Product> Products { get; }
        Product this[int id] { get; }
        Product AddProduct(Product product);
       

       
       
    }

   
}

