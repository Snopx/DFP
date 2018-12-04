using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity, PrimaryKey> : IRepository<TEntity, PrimaryKey> where TEntity : class, IEntity
    {
        protected readonly DFDbContext context;
        public BaseRepository(DFDbContext context)
        {
            this.context = context;
        }


        public IQueryable<TEntity> Entities => context.Set<TEntity>();

        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }

        }

        public int Delete(PrimaryKey id)
        {
            var entity = GetByKey(id);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            context.Attach(entity);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();
        }

        public int Delete(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            context.RemoveRange(entities);
            return context.SaveChanges();
        }

        public Task<int> DeleteAsync(PrimaryKey id)
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            context.Attach(entity);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            context.Attach(entity);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            context.RemoveRange(entities);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            context.RemoveRange(entities);
            return context.SaveChangesAsync();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault<TEntity>();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault() as Task<TEntity>;
        }

        public TEntity GetByKey(PrimaryKey key)
        {
            return context.Set<TEntity>().Find(key);
        }

        public Task<TEntity> GetByKeyAsync(PrimaryKey key)
        {
            return context.Set<TEntity>().FindAsync(key);
        }

        public IQueryable<TEntity> GetModels(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public int Insert(TEntity entity)
        {
            context.AddAsync<TEntity>(entity);
            return context.SaveChanges();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            await context.AddAsync<TEntity>(entity);
            return await context.SaveChangesAsync();
        }

        public int InsertRange(IEnumerable<TEntity> entities)
        {
            context.AddRange(entities);
            return context.SaveChanges();
        }

        public async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.AddRangeAsync(entities);
            return await context.SaveChangesAsync();
        }

        public int Update(TEntity entity)
        {
            context.Attach<TEntity>(entity);
            context.Update<TEntity>(entity);
            return context.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            context.Attach<TEntity>(entity);
            context.Update<TEntity>(entity);
            return await context.SaveChangesAsync();
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            context.UpdateRange(entities);
            return context.SaveChanges();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            context.AttachRange(entities);
            context.UpdateRange(entities);
            return await context.SaveChangesAsync();
        }
    }


    public class BaseRepository<TEntity> : BaseRepository<TEntity, long>,IRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseRepository(DFDbContext context) : base(context)
        {
        }
    }

}
