namespace Blog.Services.Repository
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities.Interfaces;

     public abstract class RepositoryService<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext dbContext;

        protected RepositoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task CreateEntityAsync(TEntity entity, string creatorId)
        {
            await AddEntityAsync(entity, creatorId);
            await SaveModificationAsync(entity, creatorId);
        }

        protected async Task DeleteEntityAsync(TEntity entity, string modifierId)
        {
            entity.Deleted = true;

            await SaveModificationAsync(entity, modifierId);
        }

        private async Task AddEntityAsync(TEntity entity, string creatorId)
        {
            entity.CreatorId = creatorId;
            entity.CreationDate = DateTime.UtcNow;

            await dbContext.AddAsync(entity);
        }

        protected async Task SaveModificationAsync(TEntity entity, string modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn= DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }

    }
}
