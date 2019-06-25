using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtCoreIdentity.Models;
using JwtCoreIdentity.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtCoreIdentity.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private TokenProvider _tokenProvider;

        public AccountController(IConfiguration config)
        {
            _config = config;
            _tokenProvider = new TokenProvider(_config);
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            if (user.UserName=="Shahid" && user.Password == "123")
            {
                var tokenString = _tokenProvider.GenerateJwt();
                return Ok(new {token = tokenString});
            }
            else
            {
                return Unauthorized();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
