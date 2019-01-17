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
        public IActionResult LoginWithAccount(string Account, string Password, string returnUrl)
        {
            if (!(Account == "admin" && Password == "123qwe"))
            {
                return BadRequest();
            }
            HttpContext.Request.Headers.Add("Authorization", "Bearer " + JwtTokenHelper.CreateTokenByHandler("Bearer", "admin",20));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
