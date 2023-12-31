﻿namespace Blog.Services.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Data;
    using Data.Entities.Interfaces;
    using Common.Constants;
    using Common.ExceptionHandlers;

    public abstract class Repository<T> where T : class, IEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

            await _dbContext.AddAsync(entity);
        }

        protected async Task SaveModificationAsync(T entity, string modifierId)
        {
            entity.LastModifierId = modifierId;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        protected async Task ValidateCreateInputAsync(string tag)
        {
            var isAny = await AnyByStringAsync(tag);
            if (isAny)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(T).Name, tag));
        }

        protected async Task<bool> AnyByIdAsync(string id)
        {
            var any = await _dbContext.Set<T>()
                .AnyAsync(e => e.Id == id && !e.Deleted);

            return any;
        }

        protected async Task<bool> AnyByStringAsync(string tag)
        {
            var any = await _dbContext.Set<T>()
                .AnyAsync(e => e.NormalizedTag == tag && !e.Deleted);

            return any;
        }

        protected async Task<T> GetByIdAsync(string Id)
        {
            var entity = await this.FindByIdOrDefaultAsync(Id);

            if (entity != null)
                return entity;

            throw new ResourceNotFoundException(string.Format(
                ErrorMessages.EntityDoesNotExist, typeof(T).Name));
        }

        private async Task<T> FindByIdOrDefaultAsync(string id)
        {
            var entity = await _dbContext.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id && !e.Deleted);

            return entity;
        }
    }
}
