using Domain.Base;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryParameterFolder
{
    public class ArticleParameter : PaginationQueryParameters
    {
        public string Title { get; set; }

        public ArticleType? ArticleType { get; set; }
    }
}
