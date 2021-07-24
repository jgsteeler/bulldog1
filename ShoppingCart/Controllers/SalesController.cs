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
        private ISaleRepo _repo;

        public SalesController(ISaleRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Sale> Get()
        {

            return _repo.Sales.ToList();
        }

        [HttpPost]
        public HttpResponseMessage Add(Sale item){

            _repo.AddSale(item);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
            
        }

    }

}