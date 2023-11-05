namespace Blog.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;
    using Managers;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ReviewsFavoritesManager _favoriteReviewsManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(UserManager<User> userManager, ReviewsFavoritesManager favoriteReviewsManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _favoriteReviewsManager = favoriteReviewsManager;
            _dbContext = context;
        }

        public async Task AddFavoriteReviewAsync(string userId, string reviewId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException(string.Format(
                    ErrorMessages.Unauthorized));

            await _favoriteReviewsManager.AddReviewAsync(user.FavoriteReviews, reviewId);
        }

        public async Task RemoveFavoritesReviewAsync(string userId, string reviewId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException(string.Format(
                    ErrorMessages.Unauthorized));

            await _favoriteReviewsManager.RemoveReviewAsync(user.FavoriteReviews, reviewId);
        }

        public async Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException(string.Format(
                    ErrorMessages.Unauthorized));

            return _favoriteReviewsManager.GetFavoriteReviewsAsync(user.FavoriteReviews);
        }

        public async Task<VoteViewModel> VoteAsync(bool type, string reviewId, string userId)
        {
            // Refactoring is needed 
            var review = await _dbContext.Reviews
                .Include(r => r.Votes)
                .FirstOrDefaultAsync(r => r.Id == reviewId && !r.Deleted)
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var existingVote = review.Votes.FirstOrDefault(v => v.UserId == userId);

            if (existingVote != null)
            {
                if (existingVote.Type == type)
                {
                    // User clicked on the same vote type again, so remove their vote
                    review.Votes.Remove(existingVote);
                }
                else
                {
                    // User is changing their vote type
                    existingVote.Type = type;
                }
            }
            else
            {
                // The user hasn't voted before, so create a new vote
                var newVote = new Vote
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = type,
                    ReviewId = review.Id,
                    UserId = userId,
                };
                review.Votes.Add(newVote);
            }

            await _dbContext.SaveChangesAsync();

            var upVotes = review.Votes.Count(v => v.Type && !v.Deleted);
            var downVotes = review.Votes.Count(v => !v.Type && !v.Deleted);

            return new VoteViewModel
            {
                UpVotes = upVotes,
                DownVotes = downVotes,
            };
        }
    }
}
