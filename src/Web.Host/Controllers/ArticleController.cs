using Application.ArticleApp;
using Application.ArticleApp.Dto;
using AutoMapper;
using Domain.Base;
using Domain.QueryParameterFolder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Host.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor of Ariticle
        /// </summary>
        /// <param name="articleService"></param>
        /// <param name="mapper"></param>
        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }
        /// <summary>
        /// get page of Articles
        /// </summary>
        /// <param name="input">ArticleParameter</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Get(ArticleParameter input)
        {
            var result = await _articleService.GetPageEntitys(input);
            return Ok(result);
        }
    }
}
