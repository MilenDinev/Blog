namespace Blog.Services.Repository
{
    using System;
    using Blog.Services.Repository.Interfaces;
    using Data;
    using Data.Entities.Interfaces;

    public abstract class Repository<T> where T : class, IEntity
    {
        protected readonly ApplicationDbContext dbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task CreateEntityAsync(T entity, string creatorId)
        {
            await AddEntityAsync(entity, creatorId);
            await SaveModificationAsync(entity, creatorId);
        }

        protected async Task DeleteEntityAsync(T entity, string modifierId)
        {
            entity.Deleted = true;

            await SaveModificationAsync(entity, modifierId);
        }

        protected async Task AddEntityAsync(T entity, string creatorId)
        {
            entity.CreatorId = creatorId;
            entity.CreationDate = DateTime.UtcNow;

            await dbContext.AddAsync(entity);
        }

        protected async Task SaveModificationAsync(T entity, string modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }
    }
}
