﻿using Domain;
using Domain.InterFace;
using Domain.Model;

namespace Application.UserApp
{
    public class UserService : BaseService<User, IRepository<User>>, IUserService
    {
        public UserService(IRepository<User> userRepository,IUnitOfWork unitOfWork) : base(userRepository, unitOfWork)
        {
        }
        
    }
}