namespace Blog.Data.Common.Repositories.Interfaces
{
    using Entities.Interfaces;

    public interface ISoftDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, ISoftDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
