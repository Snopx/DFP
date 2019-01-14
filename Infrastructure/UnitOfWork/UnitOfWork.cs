using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.InterFace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWorkFolder
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public void BeginTransaction()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public async Task<bool> CommitAsync()
        {
            if (_dbTransaction == null)
                return await _dbContext.SaveChangesAsync() > 0;
            else
                _dbTransaction.Commit();
            return true;
        }

        public async Task<bool> RegisterCleanAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
            if (_dbTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RegisterCreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (_dbTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RegisterDeletedAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (_dbTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RegisterModifiedAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            if (_dbTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public void Rollback()
        {
            if (_dbTransaction != null)
                _dbTransaction.Rollback();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return  _dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public bool RegisterCreate<TEntity>(TEntity entity) where TEntity : class
        {
             _dbContext.Set<TEntity>().Add(entity);
            if (_dbTransaction == null)
                return  _dbContext.SaveChanges() > 0;
            else
                _dbTransaction.Commit();
            return true;
        }

        public bool RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (_dbTransaction != null)
                return _dbContext.SaveChanges() > 0;
            return true;
        }

        public bool RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            if (_dbTransaction != null)
                return _dbContext.SaveChanges() > 0;
            return true;
        }

        public bool RegisterClean<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
            if (_dbTransaction != null)
                return _dbContext.SaveChanges() > 0;
            return true;
        }

        public bool Commit()
        {
            if (_dbTransaction == null)
                return _dbContext.SaveChanges() > 0;
            else
                _dbTransaction.Commit();
            return true;
        }
    }
}
