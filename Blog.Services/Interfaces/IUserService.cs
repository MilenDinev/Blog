namespace Blog.Services.Interfaces
{
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;

    public interface IUserService
    {
        Task AddReviewToFavorite(string userId, string reviewId);

        Task RemoveReviewFromFavorites(string userId, string reviewId);

        Task<VoteResponseModel> VoteAsync(bool type, string reviewId, string userId);
        Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId);
    }
}
