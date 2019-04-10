using System.Threading.Tasks;
using Application.AppService.ArticleApp.Dto;
using Application.ArticleApp;
using Domain.Enum;
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
            ArticleOutputWithTypeDto model = new ArticleOutputWithTypeDto();
            model.Life = await _articleService.GetTop5(ArticleType.Life);
            model.Yatter = await _articleService.GetTop5(ArticleType.Yatter);
            return View(model);
        }


    }
}
