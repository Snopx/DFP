using System;
using System.Collections.Generic;
using System.Text;
using Application.AutoMapper;
using Domain.Enum;
using Domain.Model;

namespace Application.ArticleApp.Dto
{
    public class ArticleOutputDto : IEntityDto
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public ArticleType ArticleType { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
