namespace Blog.Services.Interfaces
{
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;

    public interface IUserService
    {
        Task<VoteViewModel> VoteAsync(bool type, string reviewId, string userId);
        Task AddFavoriteReviewAsync(string userId, string reviewId);
        Task RemoveFavoritesReviewAsync(string userId, string reviewId);
        Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId);
        Task<bool> IsFavoriteReviewAsync(string userId, string reviewId);
    }
}
