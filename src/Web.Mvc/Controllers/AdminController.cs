using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.AppService.ArticleApp.Dto;
using Application.ArticleApp;
using AutoMapper;
using Domain.Interface;
using Domain.Model;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private IRepository<Article> _repository;
        public AdminController(IArticleService articleService, IMapper mapper, IRepository<Article> repository)
        {
            _articleService = articleService;
            _mapper = mapper;
            _repository = repository;
        }
        public IActionResult Dashboard()
        {
            return View();
        }


        public async Task<IActionResult> Article()
        {
            var result = _repository.Table.ToList() ;
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
            var isSucceed = await _articleService.AddAsync(article);
            if (!isSucceed)
            {
                return BadRequest("Failed to add new article!!!");
            }
            return RedirectToAction(nameof(Article));
        }
    }
}
