using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.UserApp;
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
        private readonly IUserService _userService;
        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public IActionResult Login(bool id = true, string ReturnUrl = "/")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(!id);
        }

        public async Task<IActionResult> SignIn(string Account, string Password, string ReturnUrl)
        {
            var user = _userService.Get(x => x.Account.Equals(Account, StringComparison.OrdinalIgnoreCase));
            if (user == null || user.Password != SecurityOfCrypt.Encode(Password))
                return RedirectToAction(nameof(Login), new { id = false, ReturnUrl });
            user.Roles = new List<Role> { new Role { ID = 1, RoleName = "Administrator" } };
            await HttpContext.SignInAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme,
                        new ClaimsPrincipal(CookieBaseaAuth.GetClaimsPrincipal(user)), CookieBaseaAuth.AuthenticationProperties);
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                return RedirectToAction("dashboard", "admin");
            else
                return LocalRedirect(ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme);
            return Redirect(nameof(Login));
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AccessDeny()
        {
            return View();
        }
    }
}
