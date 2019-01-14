using Domain;
using Domain.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application
{
    public abstract class BaseService<TEntity, TPrimaryKey, TRepository> : IService<TEntity, TPrimaryKey> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity, TPrimaryKey>
    {
        protected TRepository _repository;
        protected IUnitOfWork _unitOfWork;
        public BaseService(TRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> Table => _repository.Table;

        public bool Add(TEntity entity)
        {
            _unitOfWork.RegisterCreate(entity);
            return _unitOfWork.Commit();
        }

        public Task<bool> AddAsync(TEntity entity)
        {
            _unitOfWork.RegisterCreateAsync(entity);
            return _unitOfWork.CommitAsync();

        }

        public bool AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Get()
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(TPrimaryKey ID)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }


    public abstract class BaseService<TEntity, TRepository> : BaseService<TEntity, long, TRepository> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        public BaseService(TRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
