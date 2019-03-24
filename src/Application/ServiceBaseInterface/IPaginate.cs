using Application.AutoMapper;
using Domain.Base;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ServiceBaseInterface
{
    public interface IPaginate<T> where T : class,IEntityDto //理论上，所有的数据都需要dto
    {
        PaginatedList<T> GetPageEntitys(QueryParameters queryParameters);
    }
}
