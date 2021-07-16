using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
namespace basicApi
{
    public class Repository : IRepository{
         
         private Dictionary<int, Product> items;
         
        public Repository()
        {
            items = new Dictionary<int, Product>();
            new List<Product> {
                new Product{Id = 1, Name = "Soccer Ball", Description = "Small Soccer Ball", Price=15.99M, Category = CategoryEnum.Sports },
                new Product{Id = 2, Name = "Mizuno Helmet", Description = "Youth Catcher's Helmet", Price=45.99M, Category = CategoryEnum.Sports },
                new Product{Id = 3, Name = "Football", Description = "Full Sized Leather Football", Price=75.99M, Category = CategoryEnum.Sports },
                new Product{Id = 4, Name = "Hoop", Description = "Adjustable Height Basketball Hoop", Price=145.99M, Category = CategoryEnum.Sports },
                new Product{Id = 5, Name = "Motor Oil", Description = "5W20 synthetic motor oil", Price=4.99M, Category = CategoryEnum.Automotive },
                new Product{Id = 6, Name = "Spark Plugs", Description = "4 pack ceramic sparkplugs", Price=17.99M, Category = CategoryEnum.Automotive },
                new Product{Id = 7, Name = "Air Freshener", Description = "Pine Scented Hanging Air Freshner", Price=2.69M, Category = CategoryEnum.Automotive },
                new Product{Id = 8, Name = "Detail Spray", Description = "Quick detail spray", Price=12.99M, Category = CategoryEnum.Automotive },
                new Product{Id = 9, Name = "Seat Cover", Description = "Beaded Seat Cover, Brown", Price=17.99M, Category = CategoryEnum.Automotive },
                new Product{Id = 10, Name = "Swing", Description = "Ceder wood glider/swing", Price=158.99M, Category = CategoryEnum.OutDoors},
                new Product{Id = 14, Name = "Lawnmower", Description = "Zero Turn 60 inch deck", Price=3215.99M, Category = CategoryEnum.OutDoors},
                new Product{Id = 15, Name = "Hose", Description = "100 foot no crimp garden hose", Price=34.99M, Category = CategoryEnum.OutDoors},
                new Product{Id = 16, Name = "Birdfeeder", Description = "concrete art deco bird feeder", Price=59.99M, Category = CategoryEnum.OutDoors},
                new Product{Id = 17, Name = "Candle", Description = "Vanilla Scented Candle", Price=5.99M, Category = CategoryEnum.Home },
                new Product{Id = 18, Name = "Easy Chair", Description = "Leather Plush recliner Rocker", Price=599.99M, Category = CategoryEnum.Home },
                new Product{Id = 19, Name = "Microwave", Description = "1000 watt 1.3 cu ft stainless steel microwave", Price=155.99M, Category = CategoryEnum.Home },
                new Product{Id = 20, Name = "Cleaner", Description = "All Purpose multisurface cleaning solution", Price=2.99M, Category = CategoryEnum.Home },
                new Product{Id = 21, Name = "Fan", Description = "18 inch indoor ceiling fan", Price=45.99M, Category = CategoryEnum.Home },
                new Product{Id = 22, Name = "Coffee Table", Description = "Rustic Barnwood coffee table", Price=415.99M, Category = CategoryEnum.Home }
            }.ForEach(p=> AddProduct(p));
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
         
       
    }

    
    
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Product this[int id] {get;}
        Product AddProduct(Product product);
    }
}

