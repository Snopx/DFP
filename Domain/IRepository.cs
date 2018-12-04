using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        #region 属性
        IQueryable<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        #endregion
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);


        #region 异步
        Task<int> InsertAsync(TEntity entity);

        Task<int> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<int> DeleteAsync(TPrimaryKey id);

        Task<int> DeleteAsync(TEntity entity);

        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByKeyAsync(TPrimaryKey key);

        #endregion

        #region 同步
        int Insert(TEntity entity);

        int InsertRange(IEnumerable<TEntity> entities);

        int Delete(TPrimaryKey id);

        int Delete(TEntity entity);

        int Delete(IEnumerable<TEntity> entities);

        int Update(TEntity entity);

        int UpdateRange(IEnumerable<TEntity> entities);

        TEntity GetByKey(TPrimaryKey key);

        IQueryable<TEntity> GetModels(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }

    public interface IRepository<TEntity>: IRepository<TEntity,long> where TEntity : class, IEntity { }
}
