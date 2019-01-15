using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Table { get; }
        TEntity Get(TPrimaryKey ID);


        Task<TEntity> GetAsync(TPrimaryKey ID);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity { }
}
