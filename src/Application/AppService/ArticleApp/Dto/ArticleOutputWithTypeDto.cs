using Application.ArticleApp.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppService.ArticleApp.Dto
{
    public class ArticleOutputWithTypeDto
    {
        public List<ArticleOutputDto> Yatter { get; set; }
        public List<ArticleOutputDto> Life { get; set; }

    }
}
