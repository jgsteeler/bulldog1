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
        private const string CategoriesFile = "./app_data/category.json";
        private const string SalesFile = "./app_data/sales.json";
        private Dictionary<int, Product> _items;
        private Dictionary<int, ProductCategory> _categories;
        private Dictionary<int,Sale> _sales;

        public ProductRepo()
        {
            _items = new Dictionary<int, Product>();
            _categories = new Dictionary<int, ProductCategory>();
            _sales = new Dictionary<int, Sale>();

            JsonSerializer.Deserialize<IEnumerable<Product>>(File.ReadAllText(ProductsFile)).ToList().ForEach(p => AddProduct(p));
            JsonSerializer.Deserialize<IEnumerable<ProductCategory>>(File.ReadAllText(CategoriesFile)).ToList().ForEach(c => AddCategory(c));
            JsonSerializer.Deserialize<IEnumerable<Sale>>(File.ReadAllText(SalesFile)).ToList().ForEach(s=> LoadSale(s));


        }

        public Product this[int id] => _items.ContainsKey(id) ? _items[id] : null;
        public IEnumerable<ProductCategory> Categories => _categories.Values;

        public IEnumerable<Product> Products => _items.Values;

        public IEnumerable<Sale> Sales => _sales.Values;

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

        public Sale AddSale(Sale sale)
        {
            


            if (sale.Id == 0)
            {
                int key = _sales.Count;
                while (_sales.ContainsKey(key)) { key++; };
                sale.Id = key;
            }
            _sales[sale.Id] = sale;

             
            string jsonString = JsonSerializer.Serialize(_sales.Values);
            
            File.WriteAllText(SalesFile, jsonString);
            return sale;
        }
   public Sale LoadSale(Sale sale)
        {
            


            if (sale.Id == 0)
            {
                int key = _sales.Count;
                while (_sales.ContainsKey(key)) { key++; };
                sale.Id = key;
            }
            _sales[sale.Id] = sale;

            return sale;
        }

        public void AddCategory(ProductCategory category)
        {

            int key = _categories.Count + 1;
            _categories[key] = category;

        }

    }



    public interface IProductRepo
    {
        IEnumerable<Product> Products { get; }
        Product this[int id] { get; }
        Product AddProduct(Product product);
        Sale AddSale(Sale sale);

        IEnumerable<ProductCategory> Categories{get;}
        IEnumerable<Sale> Sales{get;}
    }
}

