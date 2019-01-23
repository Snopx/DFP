using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<bool> RegisterCreateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<bool> RegisterDeletedAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<bool> RegisterModifiedAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<bool> RegisterCleanAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<bool> CommitAsync();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        bool RegisterCreate<TEntity>(TEntity entity) where TEntity : class, IEntity;
        bool RegisterDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity;

        bool RegisterModified<TEntity>(TEntity entity) where TEntity : class, IEntity;

        bool RegisterClean<TEntity>(TEntity entity) where TEntity : class, IEntity;

        bool RegisterCreateRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class, IEntity;
        bool RegisterDeletedRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class, IEntity;

        bool RegisterModifiedRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class, IEntity;

        bool Commit();


        void Rollback();
    }
}
