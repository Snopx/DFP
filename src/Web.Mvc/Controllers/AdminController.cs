using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.AppService.ArticleApp.Dto;
using Application.ArticleApp;
using AutoMapper;
using Domain.Interface;
using Domain.Model;
using Domain.QueryParameterFolder;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Mvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        public AdminController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }
        public IActionResult Dashboard()
        {
            return View();
        }


        public async Task<IActionResult> Article(ArticleParameter input)
        {
            input.PageSize = 2;
            var result = await _articleService.GetPageEntitys(input);
            ViewData["input"] = input;
            return View(result);
        }

        public IActionResult NewArticle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle(ArticleInputDto input)
        {
            var article = _mapper.Map<Article>(input);
            article.CreateTime = DateTime.Now;
            article.Author = User.Claims.Where(x => x.Type.Equals("FullName", StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
            var isSucceed = await _articleService.AddAsync(article);
            if (!isSucceed)
            {
                return BadRequest("Failed to add new article!!!");
            }
            return RedirectToAction(nameof(Article));
        }
    }
}
