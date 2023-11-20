namespace Blog.Services.Interfaces
{
    using Data.ViewModels.Vote;
    using Data.ViewModels.Tool;

    public interface IUserService
    {
        Task<VoteViewModel> VoteAsync(bool type, string toolId, string userId);
        Task AddFavoriteToolAsync(string userId, string toolId);
        Task RemoveFavoritesToolAsync(string userId, string toolId);
        Task<ICollection<ToolPreviewModel>> GetFavoriteToolsAsync(string userId);
        Task<bool> IsFavoriteToolAsync(string userId, string toolId);
    }
}
