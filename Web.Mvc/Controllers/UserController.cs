using System.Security.Claims;
using System.Security.Cryptography;
using Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginWithAccount(string account, string pwd,string returnUrl)
        {
            if (!(account == "admin" && pwd == "123qwe"))
            {
                return BadRequest();
            }
            return Ok(JwtTokenHelper.CreateTokenByHandler(20));
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
