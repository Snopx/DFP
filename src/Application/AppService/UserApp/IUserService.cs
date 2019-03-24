using Application.ServiceBaseInterface;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserApp
{
    public interface IUserService : IService<User>,IPaginate<UserDto>
    {
    }
}
