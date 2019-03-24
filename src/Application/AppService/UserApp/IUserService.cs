using Application.ServiceBaseInterface;
using Domain.Model;

namespace Application.UserApp
{
    public interface IUserService : IService<User>,IPaginate<UserDto>
    {
    }
}
