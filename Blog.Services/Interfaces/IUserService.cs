namespace Blog.Services.Interfaces
{
    public interface IUserService
    {
        Task AddReviewToFavorite(string userId, string reviewId);

        Task RemoveReviewFromFavorites(string userId, string reviewId);

        Task VoteAsync(bool type, string reviewId, string userId);

        Task ViewReviewAsync(string reviewId, string userId);

        Task ViewVideoAsync(string videoId, string userId);

        Task ViewArticleAsync(string articleId, string userId);
    }
}
