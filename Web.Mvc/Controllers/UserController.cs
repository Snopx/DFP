﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login(string account, string pwd,string returnUrl)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
