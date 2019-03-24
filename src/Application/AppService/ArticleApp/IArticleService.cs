using System;
using System.Collections.Generic;
using System.Text;
using Application.ArticleApp.Dto;
using Application.ServiceBaseInterface;
using Domain.Model;
using Domain.QueryParameterFolder;

namespace Application.ArticleApp
{
    public interface IArticleService: IService<Article>,IPaginate<ArticleOutputDto, ArticleParameter>
    {
    }
}
