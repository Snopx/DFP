using Application.SUser;
using Domain;
using Domain.InterFace;
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
        public UserService(IRepository<User> userRepository,IUnitOfWork unitOfWork) : base(userRepository, unitOfWork)
        {
        }
        
    }
}
