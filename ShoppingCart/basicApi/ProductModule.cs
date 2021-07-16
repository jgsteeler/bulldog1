using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basicApi
{
    public class ProductModule
    {
        private IRepository _repo;

        public ProductModule(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("/api/products")]
        public Task Get(HttpContext context) {
            var retVal = new JsonResult(_repo.Products);
        context.Response.WriteAsync("Hello World!");
        return null;
        }
    }
}