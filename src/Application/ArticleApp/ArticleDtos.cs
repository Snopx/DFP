using System;
using System.Collections.Generic;
using System.Text;
using Application.AutoMapper;
using Domain.Model;

namespace Application.ArticleApp
{
    public class ArticleDtos: EntityDto<long>
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Auther { get; set; }

    }
}
