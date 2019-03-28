using System.Threading.Tasks;
using Application.ServiceBaseInterface;
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

        public Task<PaginatedList<UserDto>> GetPageEntitys(PaginationQueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
