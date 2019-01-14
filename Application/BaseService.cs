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
            _unitOfWork.RegisterCreateRange(entities);
            return _unitOfWork.Commit();
        }

        public Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterCreateRange(entities);
            return _unitOfWork.CommitAsync();
        }

        public bool Delete(TEntity entity)
        {
            _unitOfWork.RegisterDeleted(entity);
            return _unitOfWork.Commit();
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            _unitOfWork.RegisterDeletedAsync(entity);
            return _unitOfWork.CommitAsync();
        }

        public bool DeleteById(TPrimaryKey id)
        {
            var entity = _repository.Get(id);
            return _unitOfWork.RegisterDeleted(entity);
        }

        public Task<bool> DeleteByIdAsync(TPrimaryKey id)
        {
            var entity = _repository.Get(id);
            return _unitOfWork.RegisterDeletedAsync(entity);
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterDeletedRange(entities);
            return _unitOfWork.Commit();
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterDeletedRange(entities);
            return _unitOfWork.CommitAsync();
        }

        public TEntity Get(TPrimaryKey ID)
        {
            return _repository.Get(ID);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Table.Where(predicate).FirstOrDefault();
        }

        public Task<TEntity> GetAsync(TPrimaryKey ID)
        {
            return _repository.GetAsync(ID);
        }

        public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.GetAsync(predicate);
        }

        public bool Update(TEntity entity)
        {
            _unitOfWork.RegisterModified(entity);
            return _unitOfWork.Commit();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            _unitOfWork.RegisterModifiedAsync(entity);
            return _unitOfWork.CommitAsync();
        }

        public bool UpdateRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterModifiedRange(entities);
            return _unitOfWork.Commit();
        }

        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterModifiedRange(entities);
            return _unitOfWork.CommitAsync();
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
