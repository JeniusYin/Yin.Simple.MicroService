using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Yin.ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            string src = Configuration["urls"];
            var users = new List<Product>
            {
                new Product { Id = 1, Name = "MI", Source = src},
                new Product { Id = 2, Name = "iPhone", Source = src}
            };
            return users;
        }
    }
}
