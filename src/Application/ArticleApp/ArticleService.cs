using Domain.Interface;
using Domain.Model;

namespace Application.ArticleApp
{
    public class ArticleService : BaseService<Article, IRepository<Article>>, IArticleService
    {
        public ArticleService(IRepository<Article> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
