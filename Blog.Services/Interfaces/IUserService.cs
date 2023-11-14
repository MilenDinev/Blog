namespace Blog.Services.Interfaces
{
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Tool;

    public interface IUserService
    {
        Task<VoteViewModel> VoteAsync(bool type, string toolId, string userId);
        Task AddFavoriteToolAsync(string userId, string toolId);
        Task RemoveFavoritesToolAsync(string userId, string toolId);
        Task<ICollection<ToolPreviewModel>> GetFavoriteToolsAsync(string userId);
        Task<bool> IsFavoriteToolAsync(string userId, string toolId);
    }
}
