using Application.AppService.UserApp.dto;
using Application.ServiceBaseInterface;
using Domain.Model;

namespace Application.UserApp
{
    public interface IUserService : IService<UserModel>,IPaginate<UserDto>
    {
    }
}
