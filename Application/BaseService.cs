using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public abstract class BaseService<TEntity, TPrimaryKey,TRepository> : IService<TEntity, TPrimaryKey> where TEntity : class, IEntity
        where TRepository: IRepository<TEntity, TPrimaryKey>
    {
        protected TRepository _repository;
        public BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<TEntity> Table => _repository.Table;

        public int Add(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        public Task<int> AddAsync(TEntity entity)
        {
            return _repository.InsertAsync(entity);
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            return _repository.InsertRange(entities);
        }

        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _repository.InsertRangeAsync(entities);
        }

        public int Delete(TEntity entity)
        {
            return _repository.Delete(entity);
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public int DeleteById(TPrimaryKey id)
        {
            return _repository.Delete(id);
        }

        public Task<int> DeleteByIdAsync(TPrimaryKey id)
        {
            return _repository.DeleteAsync(id);
        }

        public int DeleteRange(IEnumerable<TEntity> entities)
        {
            return _repository.Delete(entities);
        }

        public Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteRangeAsync(entities);
        }

        public Task<TEntity> Find(TPrimaryKey id)
        {
            return _repository.GetByKeyAsync(id);
        }

        public Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return _repository.GetByKeyAsync(id);
        }

        public TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public IQueryable<TEntity> FindRange(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Table.Where(predicate);
        }

        public TEntity GetById(TPrimaryKey primaryKey)
        {
            return _repository.GetByKey(primaryKey);
        }

        public int Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateRange(entities);
        }

        public Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateRangeAsync(entities);
        }

    }


    public abstract class BaseService<TEntity, TRepository> : BaseService<TEntity, long, TRepository> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        public BaseService(TRepository repository) : base(repository)
        {
        }
    }
}
