namespace Blog.Services.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities.Interfaces;
    using Interfaces;

    public class Finder : IFinder
    {
        private readonly ApplicationDbContext dbContext;

        public Finder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> FindByIdOrDefaultAsync<T>(string id) where T : class, IEntity
        {
            var entity = await this.dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            return entity;
        }

        public async Task<T> FindByStringOrDefaultAsync<T>(string tag) where T : class, IEntity
        {
            var entity = await this.dbContext.Set<T>().FirstOrDefaultAsync(e => e.NormalizedTag == tag.ToUpper() && !e.Deleted);

            return entity;
        }

        public async Task<bool> AnyByIdAsync<T>(string id) where T : class, IEntity
        {
            var any = await this.dbContext.Set<T>().AnyAsync(e => e.Id == id);

            return any;
        }

        public async Task<bool> AnyByStringAsync<T>(string tag) where T : class, IEntity
        {
            var any = await this.dbContext.Set<T>().AnyAsync(e => e.NormalizedTag == tag);

            return any;
        }

        public async Task<ICollection<T>> GetAllAsync<T>() where T : class, IEntity
        {
            var entities = await this.dbContext.Set<T>().ToArrayAsync();

            return entities;
        }
    }
}
