using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basicApi
{
    public class HelloWorldModule
    {
        [HttpGet("/")]
        public Task Get(HttpContext context) => context.Response.WriteAsync("Hello World!");
    }
}