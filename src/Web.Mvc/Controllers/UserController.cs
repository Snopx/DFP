using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authentication;
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
        public IActionResult Login(bool id=true)
        {
            return View(!id);
        }
        public async Task<IActionResult> LoginWithAccount(string Account, string Password, string returnUrl)
        {
            //var Role = new List<Role>() { new Role { ID=1,RoleName="Admin"}};
            //var user = new User { ID = "admin", Password = "123qwe", Gender = Domain.Enum.Gender.Male, Name = "Darrenfang", Roles=Role };
            //if (!(Account == user.ID && Password == user.Password))
            //{
            //    return RedirectToAction(nameof(Login),new { id=false});
            //}
            //await HttpContext.SignInAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme,
            //        new ClaimsPrincipal(CookieBaseaAuth.GetClaimsPrincipal(user)));
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme);
            return Redirect("/");
        }

        public IActionResult AccessDeny()
        {
            return View();
        }
    }
}
