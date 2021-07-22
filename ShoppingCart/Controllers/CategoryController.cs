using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private IProductRepo _productRepo;

        public CategoryController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {

            return _productRepo.GetCategories();
        }

    }

}
