using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.AppService.ArticleApp.Dto
{
    public class ArticleInputDto
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }

        public ArticleType ArticleType { get; set; }

        [MaxLength(80)]
        public string ImgUrl { get; set; }

        public string Content { get; set; }

        [MaxLength(100)]
        public string Summary { get; set; }

        public string Remark { get; set; }
    }
}
