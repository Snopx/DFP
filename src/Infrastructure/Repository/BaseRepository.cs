using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Domain.Interface;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class BaseRepository<TEntity, PrimaryKey> : IRepository<TEntity, PrimaryKey> where TEntity : class, IEntity
    {
        protected readonly IDbContext context;
        public BaseRepository(IDbContext context)
        {
            this.context = context;
        }


        public virtual IQueryable<TEntity> Table => context.Set<TEntity>();

        public TEntity Get(PrimaryKey ID)
        {
            return context.Set<TEntity>().Find(ID);
        }

        public async ValueTask<TEntity> GetAsync(PrimaryKey ID)
        {
            return await context.Set<TEntity>().FindAsync(ID);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }


    }


    public class BaseRepository<TEntity> : BaseRepository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseRepository(DFDbContext context) : base(context)
        {
        }
    }

}
