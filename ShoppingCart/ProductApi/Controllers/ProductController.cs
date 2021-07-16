using System;
using System.Collections.Generic;
using ProductApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace ProductApi.Controllers{

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase {
private IRepository repository;
        public ProductController(IRepository repo) => repository = repo;
 
        [HttpGet]
        public IEnumerable<Product> Get() => repository.Products;
 
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed in the request body.");
            return Ok(repository[id]);
        }

        [HttpGet("category")]
  public IEnumerable<Product> GetProducts(CategoryEnum category) => repository.Products.Where(p=> p.Category == category);
}

}

