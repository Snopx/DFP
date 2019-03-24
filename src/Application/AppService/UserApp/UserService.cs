using Domain;
using Domain.Base;
using Domain.Interface;
using Domain.Model;

namespace Application.UserApp
{
    public class UserService : BaseService<User,IRepository<User>>, IUserService
    {
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public PaginatedList<UserDto> GetPageEntitys(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
