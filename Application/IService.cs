using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// 服务接口
    /// </summary>
    public interface IService<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Table { get; }

        Task<bool> AddAsync(TEntity entity);

        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Add(TEntity entity);

        bool AddRange(IEnumerable<TEntity> entities);

        Task<bool> DeleteByIdAsync(TPrimaryKey id);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        bool DeleteById(TPrimaryKey id);

        bool Delete(TEntity entity);

        bool DeleteRange(IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        bool UpdateRange(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(TPrimaryKey ID);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IQueryable<TEntity>> GetAsync();

        TEntity Get();

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

    }

    public interface IService<TEntity> : IService<TEntity, long> where TEntity : class, IEntity
    {
    }


}
