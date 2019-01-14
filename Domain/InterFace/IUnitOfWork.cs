using System.Threading.Tasks;

namespace Domain.InterFace
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<bool> RegisterCreateAsync<TEntity>(TEntity entity)
            where TEntity : class;
        Task<bool> RegisterDeletedAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> RegisterModifiedAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> RegisterCleanAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> CommitAsync();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        bool RegisterCreate<TEntity>(TEntity entity)
            where TEntity : class;
        bool RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : class;

        bool RegisterModified<TEntity>(TEntity entity)
            where TEntity : class;

        bool RegisterClean<TEntity>(TEntity entity)
            where TEntity : class;

        bool Commit();


        void Rollback();
    }
}
