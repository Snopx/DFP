﻿using Application.SUser;
using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SUser
{
    public class UserService : BaseService<User, IRepository<User>>, IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}
