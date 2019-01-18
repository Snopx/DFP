using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    public class AdminController: Controller
    {

        public IActionResult DashBord()
        {
            return View();
        }
    }
}
