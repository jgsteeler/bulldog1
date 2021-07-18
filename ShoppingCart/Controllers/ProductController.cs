using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;



namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
       
       private IProductRepo _productRepo;

        public ProductController( IProductRepo productRepo)
        {
            
            _productRepo = productRepo;
        }

        [HttpGet]
        public IEnumerable<Product> Get([FromQuery] string category){

            if (string.IsNullOrWhiteSpace(category)){
                return _productRepo.Products.ToList();
            }
            else{
                return _productRepo.Products.Where(x=> x.Category == category);
            }

        } 

        [HttpGet, Route("{id}")]
        public Product GetById(int id)=> _productRepo[id];

        [HttpGet, Route("categories")]
        public IEnumerable<string> GetCategories()=> _productRepo.GetCategories();
        
    }
}
