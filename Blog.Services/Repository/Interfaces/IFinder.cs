namespace Blog.Services.Repository.Interfaces
{
    using Data.Entities.Interfaces;

    public interface IFinder
    {
        Task<T> FindByIdOrDefaultAsync<T>(string id) where T : class, IEntity;
        Task<T> FindByStringOrDefaultAsync<T>(string stringValue) where T : class, IEntity;
        Task<bool> AnyByIdAsync<T>(string id) where T : class, IEntity;
        Task<bool> AnyByStringAsync<T>(string tag) where T : class, IEntity;
        Task<ICollection<T>> GetAllAsync<T>() where T : class, IEntity;
    }
}
