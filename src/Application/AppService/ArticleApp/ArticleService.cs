using Application.ArticleApp.Dto;
using Domain.Base;
using Domain.Interface;
using Domain.Model;

namespace Application.ArticleApp
{
    public class ArticleService : BaseService<Article, IRepository<Article>>, IArticleService
    {
        public ArticleService(IRepository<Article> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public PaginatedList<ArticleOutputDto> GetPageEntitys(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
