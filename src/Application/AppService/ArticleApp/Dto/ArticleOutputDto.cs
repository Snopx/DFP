using System;
using System.Collections.Generic;
using System.Text;
using Application.AutoMapper;
using Domain.Model;

namespace Application.ArticleApp.Dto
{
    public class ArticleOutputDto : IEntityDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Auther { get; set; }
        public string Remark { get; set; }
    }
}
