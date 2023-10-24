namespace Blog.Services
{
    using Microsoft.AspNetCore.Identity;
    using Data;
    using Data.Entities;
    using Interfaces;

    public class UserService : IUserService
    {
        ApplicationDbContext _dbContext;
        UserManager<User> _userManager;

        public UserService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public async Task AddReviewToFavorite(string userId, string reviewId)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            var isReviewAlreadyFavorite = user.FavoriteReviews.Any(x => x.Id == reviewId);

            if (!isReviewAlreadyFavorite)
            {
                var review = await _dbContext.Reviews.FindAsync(reviewId);
                user.FavoriteReviews.Add(review);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task RemoveReviewFromFavorites(string userId, string reviewId)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            var isReviewAlreadyFavorite = user.FavoriteReviews.Any(x => x.Id == reviewId);

            if (isReviewAlreadyFavorite)
            {
                var review = await _dbContext.Reviews.FindAsync(reviewId);
                user.FavoriteReviews.Remove(review);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task VoteAsync(bool type, string reviewId, string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            var vote = user.Votes.FirstOrDefault(x => x.ReviewId == reviewId);

            if (vote != null)
            {
                if (vote.Type == type)
                {
                    return;
                }

                vote.Type = type;
                vote.ChangedVoteOn = DateTime.UtcNow;
            }
            else
            {
                vote = new Vote
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = type,
                    ReviewId = reviewId,
                    UserId = userId
                };

                await _dbContext.Votes.AddAsync(vote);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
