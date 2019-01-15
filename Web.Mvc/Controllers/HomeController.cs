using Application.SUser;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.Table);
        }

        public IActionResult Create(User u)
        {
            _userService.Add(u);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(long id)
        {
            _userService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
