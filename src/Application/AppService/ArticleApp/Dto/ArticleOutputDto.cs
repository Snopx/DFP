using System;
using Application.ServiceBaseInterface;
using Domain.Enum;

namespace Application.ArticleApp.Dto
{
    public class ArticleOutputDto : IEntityDto
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }


        public ArticleType ArticleType { get; set; }

        public DateTime CreateTime { get; set; }

        public string Author { get; set; }
    }
}
