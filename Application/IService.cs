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
    public interface IService<TEntity,TPrimaryKey> where TEntity:class,IEntity
    {
        IQueryable<TEntity> Table { get; }

        Task<int> AddAsync(TEntity entity);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<int> DeleteByIdAsync(TPrimaryKey id);

        Task<int> DeleteAsync(TEntity entity);

        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> FindAsync(TPrimaryKey id);

        #region
        int Add(TEntity entity);

        int AddRange(IEnumerable<TEntity> entities);

        int DeleteById(TPrimaryKey id);

        int Delete(TEntity entity);

        int DeleteRange(IEnumerable<TEntity> entities);

        int Update(TEntity entity);

        int UpdateRange(IEnumerable<TEntity> entities);

        #endregion

        TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindRange(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(TPrimaryKey primaryKey);
    }

    public interface IService<TEntity>: IService<TEntity, long> where TEntity:class,IEntity
    {
    }


}
