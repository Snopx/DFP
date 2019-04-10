using Application.ServiceBaseInterface;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
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

        public virtual bool Add(TEntity entity)
        {
            _unitOfWork.RegisterCreate(entity);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _unitOfWork.RegisterCreateAsync(entity);
            return await _unitOfWork.CommitAsync();

        }

        public virtual bool AddRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterCreateRange(entities);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterCreateRange(entities);
            return await _unitOfWork.CommitAsync();
        }

        public virtual bool Delete(TEntity entity)
        {
            _unitOfWork.RegisterDeleted(entity);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            await _unitOfWork.RegisterDeletedAsync(entity);
            return await _unitOfWork.CommitAsync();
        }

        public virtual bool DeleteById(TPrimaryKey id)
        {
            _unitOfWork.RegisterDeleted(Get(id));
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> DeleteByIdAsync(TPrimaryKey id)
        {
            await _unitOfWork.RegisterDeletedAsync(Get(id));
            return await _unitOfWork.CommitAsync();
        }

        public virtual bool DeleteRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterDeletedRange(entities);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterDeletedRange(entities);
            return await _unitOfWork.CommitAsync();
        }

        public virtual TEntity Get(TPrimaryKey ID)
        {
            return _repository.Get(ID);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Table.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _repository.Table.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey ID)
        {
            return await _repository.GetAsync(ID);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Table.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetRange(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public virtual bool Update(TEntity entity)
        {
            _unitOfWork.RegisterModified(entity);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            await _unitOfWork.RegisterModifiedAsync(entity);
            return await _unitOfWork.CommitAsync();
        }

        public virtual bool UpdateRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterModifiedRange(entities);
            return _unitOfWork.Commit();
        }

        public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _unitOfWork.RegisterModifiedRange(entities);
            return await _unitOfWork.CommitAsync();
        }
    }


    public abstract class BaseService<TEntity, TRepository> : BaseService<TEntity, int, TRepository> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        public BaseService(TRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
