using System.Collections.Generic;
using System.Threading.Tasks;
using Application.AppService.UserApp.dto;
using AutoMapper;
using Domain.Base;
using Domain.Extension;
using Domain.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.UserApp
{
    public class UserService : BaseService<UserModel, IRepository<UserModel>>, IUserService
    {
        private IMapper _mapper;
        public UserService(IRepository<UserModel> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDto>> GetPageEntitys(PaginationQueryParameters queryParameters)
        {
            var query = Table;

            int count = await query.CountAsync();
            query = query.PageBy(queryParameters);

            var users = await query.ToListAsync();

            var data = _mapper.Map<List<UserDto>>(users);

            return new PaginatedList<UserDto>(queryParameters.PageIndex, queryParameters.PageSize, count, data);
        }
    }
}
