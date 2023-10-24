namespace Blog.Services.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Data;
    using Data.Entities.Interfaces;
    using Constants;
    using Handlers.Exceptions;

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

        public async Task<bool> AnyByIdAsync(string id)
        {
            var any = await this.dbContext.Set<T>()
                .AnyAsync(e => e.Id == id && !e.Deleted);

            return any;
        }

        public async Task<bool> AnyByStringAsync(string tag)
        {
            var any = await this.dbContext.Set<T>()
                .AnyAsync(e => e.NormalizedTag == tag && !e.Deleted);

            return any;
        }

        public async Task<T> FindByIdOrDefaultAsync(string id)
        {
            var entity = await this.dbContext.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id && !e.Deleted);

            return entity;
        }

        public async Task<T> GetByIdAsync(string Id)
        {
            var entity = await this.FindByIdOrDefaultAsync(Id);

            if (entity != null)
                return entity;

            throw new ResourceNotFoundException(string.Format(
                ErrorMessages.EntityDoesNotExist, typeof(T).Name));
        }
    }
}
