using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity, PrimaryKey> : IRepository<TEntity, PrimaryKey> where TEntity : class, IEntity
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

        public Task<TEntity> GetAsync(PrimaryKey ID)
        {
            return context.Set<TEntity>().FindAsync(ID);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate);
        }

        public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate) as Task<IQueryable<TEntity>>;
        }
    }


    public class BaseRepository<TEntity> : BaseRepository<TEntity, long>, IRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseRepository(DFDbContext context) : base(context)
        {
        }
    }

}
