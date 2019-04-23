using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.AppService.UserApp.dto;
using Application.UserApp;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserController(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
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
            await CookieBaseaAuth.Login(HttpContext, user);
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                return RedirectToAction("dashboard", "admin");
            else
                return LocalRedirect(ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await CookieBaseaAuth.LogOut(HttpContext);
            return Redirect(nameof(Login));
        }


        public IActionResult Register()
        {
            return View();
        }


        public async Task<IActionResult> RegisterAccount(UserRegisterDto input)
        {
            try
            {
                input.Password = SecurityOfCrypt.Encode(input.Password);
                var user = _mapper.Map<UserModel>(input);
                var result = await _userService.AddAsync(user);

                return result ? Redirect(nameof(Login)) : Redirect(nameof(Register));
            }
            catch
            {
                throw new ArgumentException("account had been used,change it please;");
            }
        }


        public IActionResult AccessDeny()
        {
            return View();
        }
    }
}
