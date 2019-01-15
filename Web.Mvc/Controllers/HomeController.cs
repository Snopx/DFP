using System.Threading.Tasks;
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

        public async Task<IActionResult> Create(User u)
        {
            await _userService.AddAsync(u);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteByIdAsync(id);
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
