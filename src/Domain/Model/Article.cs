using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Article:BaseEntity
    {
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
