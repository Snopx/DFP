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

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public void BeginTransaction()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public bool Commit()
        {
            if (_dbTransaction == null)
                return _dbContext.SaveChanges() > 0;
            else
                _dbTransaction.Commit();
            return true;
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
            return await SaveAsync();
        }

        public async Task<bool> RegisterCreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> RegisterDeletedAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> RegisterModifiedAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            return await SaveAsync();
        }

        public void Rollback()
        {
            if (_dbTransaction != null)
                _dbTransaction.Rollback();
        }



        public bool RegisterCreate<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            return Save();
        }

        public bool RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return Save();
        }

        public bool RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            return Save();
        }

        public bool RegisterClean<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
            return Save();
        }



        public bool RegisterCreateRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRange(entitys);
            return Save();
        }

        public bool RegisterDeletedRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class
        {
            _dbContext.Set<TEntity>().RemoveRange(entitys);
            return Save();
        }

        public bool RegisterModifiedRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class
        {
            _dbContext.Set<TEntity>().AttachRange(entitys);
            _dbContext.Set<TEntity>().UpdateRange(entitys);
            return Save();
        }

        private bool Save()
        {
            if (_dbTransaction != null)
                return _dbContext.SaveChanges() > 0;
            return true;
        }
        private async Task<bool> SaveAsync()
        {
            if (_dbTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

    }
}
