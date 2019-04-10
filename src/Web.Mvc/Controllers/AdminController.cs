using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.AppService.ArticleApp.Dto;
using Application.ArticleApp;
using Application.ArticleApp.Dto;
using Application.UserApp;
using AutoMapper;
using Domain.Base;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AdminController(IArticleService articleService, IMapper mapper, IUserService userService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _userService = userService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }


        public async Task<IActionResult> Article(ArticleParameter input)
        {
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

        public async Task<IActionResult> ModifyArticle(int id)
        {
            var result = await _articleService.GetAsync(id);
            if (result != null)
            {
                var dto = _mapper.Map<ArticleOutputDto>(result);
                return View(dto);
            }
            else
            {
                return NotFound("this article has been deleted,refresh please");
            }
        }

        public async Task<IActionResult> UpdateArticle(ArticleInputDto input)
        {
            var article = await _articleService.GetAsync(input.ID);
            if (article == null)
                return BadRequest("This Article May Have Been Deleted");
            article = _mapper.Map(input, article);

            article.UpdateTime = DateTime.Now;
            article.Author = User.Claims.Where(x => x.Type.Equals("FullName", StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
            var result = await _articleService.UpdateAsync(article);
            if (result)
                return RedirectToAction(nameof(Article));
            else
                return BadRequest("Failed to Update;");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteByIdAsync(id);
            return Json(new { success = result, id = id });
        }



        public async Task<IActionResult> UserAdmin(UserParameter input)
        {
            var users = await _userService.GetPageEntitys(input);
            ViewData["input"] = input;
            return View(users);
        }
    }
}
