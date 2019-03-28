using System.Threading.Tasks;
using Application.ArticleApp;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _articleService.GetTop5();
            return View(model);
        }


    }
}
