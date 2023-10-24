namespace Blog.Services
{
    using Microsoft.AspNetCore.Identity;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
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

        public async Task ViewReviewAsync(string reviewId, string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            var userReviewView = user.UserReviewsViewed.FirstOrDefault(x => x.ReviewId == reviewId);

            if (userReviewView != null)
            {
                userReviewView.Counter++;
            }

            else
            {
                var reviewView = new UserReviewViews
                {
                    Id = Guid.NewGuid().ToString(),
                    ReviewId = reviewId,
                    UserId = userId,
                    Counter = 1
                };

                await _dbContext.UsersReviewsViews.AddAsync(reviewView);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ViewVideoAsync(string videoId, string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            var userVideoView = user.UserVideosViewed.FirstOrDefault(x => x.VideoId == videoId);

            if (userVideoView != null)
            {
                userVideoView.Counter++;
            }

            else
            {
                var videoView = new UserVideoViews
                {
                    Id = Guid.NewGuid().ToString(),
                    VideoId = videoId,
                    UserId = userId,
                    Counter = 1
                };

                await _dbContext.UsersVideosViews.AddAsync(videoView);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ViewArticleAsync(string articleId, string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            var userArticleView = user.UserArticlesViewed.FirstOrDefault(x => x.ArticleId == articleId);

            if (userArticleView != null)
            {
                userArticleView.Counter++;
            }

            else
            {
                var articleView = new UserArticleViews
                {
                    Id = Guid.NewGuid().ToString(),
                    ArticleId = articleId,
                    UserId = userId,
                    Counter = 1
                };

                await _dbContext.UsersArticlesViews.AddAsync(articleView);    
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
