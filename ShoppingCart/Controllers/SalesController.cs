using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private IProductRepo _productRepo;

        public SalesController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IEnumerable<Sale> Get()
        {

            return _productRepo.Sales.ToList();
        }

        [HttpPost]
        public HttpResponseMessage Add(Sale item){

            _productRepo.AddSale(item);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
            
        }

    }

}