using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtCoreIdentity.Models;
using JwtCoreIdentity.Provider;
using JwtCoreIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtCoreIdentity.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly TokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        public AccountController(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            this._userManager = userManager;
            _tokenProvider = new TokenProvider(_config);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userViewModel.Username);
                if (user == null)
                {
                    return Unauthorized();
                }

                var userResult = await _userManager.CheckPasswordAsync(user, userViewModel.Password);
                if (userResult)
                {
                    var tokenString = _tokenProvider.GenerateJwt();
                    return Ok(new {token = tokenString});
                }

                return NotFound("invalid password");
            }

            return BadRequest(ModelState);
        }

        // POST api/<controller>
        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User()
            {
                UserName = userViewModel.Username,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName
            };

            var identityResult = await _userManager.CreateAsync(user, userViewModel.Password);
            if (identityResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(userViewModel.Email))
                {
                    if ((await _userManager.SetEmailAsync(user, userViewModel.Email)).Succeeded)
                    {
                        return Ok(user.Id);
                    }
                }
                return Ok();
            }

            return BadRequest(String.Join("#", identityResult.Errors.Select(x => x.Description).ToList()));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}