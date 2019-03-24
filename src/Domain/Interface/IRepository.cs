using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Table { get; }
        TEntity Get(TPrimaryKey ID);

        Task<TEntity> GetAsync(TPrimaryKey ID);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity { }
}
