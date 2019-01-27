using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model;

namespace Application.ArticleApp
{
    public interface IArticleService: IService<Article>
    {
    }
}
