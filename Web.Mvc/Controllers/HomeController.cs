using Application.SUser;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View(userService.Table);
        }

        public IActionResult Create(User u)
        {
            userService.Add(u);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await userService.DeleteByIdAsync(id);
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
