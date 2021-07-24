using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepo _repo;

        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<ProductCategory> Get()
        {

            return _repo.Categories.ToList();
        }

    }

}
