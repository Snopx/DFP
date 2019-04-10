using Domain.Base;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Extension
{
    public static class QueryableExtension
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> source, PaginationQueryParameters pagePara) where T : class, IEntity
        {
            return source.Skip((pagePara.PageIndex * pagePara.PageSize)).Take(pagePara.PageSize);
        }


        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate) where T : class, IEntity
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
