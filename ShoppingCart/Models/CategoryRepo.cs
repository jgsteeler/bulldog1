using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace ShoppingCart.Models
{
    public class CategoryRepo : ICategoryRepo
    {
        private const string CategoriesFile = "./app_data/category.json";

        private Dictionary<int, ProductCategory> _categories;
        public IEnumerable<ProductCategory> Categories => _categories.Values;


        public CategoryRepo()
        {
            _categories = new Dictionary<int, ProductCategory>();
            JsonSerializer.Deserialize<IEnumerable<ProductCategory>>(File.ReadAllText(CategoriesFile)).ToList().ForEach(c => AddCategory(c));
        }

        public void AddCategory(ProductCategory category)
        {

            int key = _categories.Count + 1;
            _categories[key] = category;

        }
    }

    public interface ICategoryRepo
    {
        IEnumerable<ProductCategory> Categories { get; }
    }


}

