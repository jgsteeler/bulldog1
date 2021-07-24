using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace ShoppingCart.Models
{
    public class SaleRepo : ISaleRepo
    {
        private const string SalesFile = "./app_data/sales.json";
        private Dictionary<int, Sale> _sales;
        public IEnumerable<Sale> Sales => _sales.Values;
        public Sale this[int id] => _sales.ContainsKey(id) ? _sales[id] : null;

        public SaleRepo()
        {
            _sales = new Dictionary<int, Sale>();
            JsonSerializer.Deserialize<IEnumerable<Sale>>(File.ReadAllText(SalesFile)).ToList().ForEach(s => LoadSale(s));
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


       
    }

    public interface ISaleRepo
    {
        IEnumerable<Sale> Sales { get; }
        Sale AddSale(Sale sale);
        Sale this[int id] { get; }

    }
}

