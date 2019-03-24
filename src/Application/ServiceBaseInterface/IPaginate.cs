using Application.AutoMapper;
using Domain.Base;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceBaseInterface
{
    public interface IPaginate<T,TParameter> where T : class,IEntityDto where TParameter: QueryParameters
    {
        Task<PaginatedList<T>> GetPageEntitys(TParameter queryParameters);
    }

    public interface IPaginate<T> : IPaginate<T, QueryParameters> where T :class,IEntityDto 
    {
    }
}
