using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Yin.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            string src = Configuration["urls"];
            var users = new List<User>
            {
                new User { Id = 1, Name = "jay", Source = src},
                new User { Id = 2, Name = "james", Source = src}
            };
            return users;
        }
    }
}
