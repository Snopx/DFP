using Domain.Base;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Article:BaseEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }

        public ArticleType ArticleType { get; set; }

        [MaxLength(80)]
        public string ImgUrl { get; set; }

        public string Content { get; set; }

        [MaxLength(100)]
        public string Summary { get; set; }

        [DisplayFormat(DataFormatString="yyyy-MM-dd HH:mm")]
        public DateTime CreateTime { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm")]
        public DateTime UpdateTime { get; set; }

        [MaxLength(30)]
        public string Author { get; set; }
        public string Remark { get; set; }

    }
}
