﻿using Domain;
using Domain.Interface;
using Domain.Model;

namespace Application.UserApp
{
    public class UserService : BaseService<User,IRepository<User>>, IUserService
    {
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
