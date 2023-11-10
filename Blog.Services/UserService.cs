﻿namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;
    using Managers;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;

    public class UserService : IUserService
    {
        private readonly ReviewsFavoritesManager _favoriteReviewsManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(ReviewsFavoritesManager favoriteReviewsManager, ApplicationDbContext context)
        {
            _favoriteReviewsManager = favoriteReviewsManager;
            _dbContext = context;
        }

        public async Task AddFavoriteReviewAsync(string userId, string reviewId)
        {
            await _favoriteReviewsManager.AddReviewAsync(userId, reviewId);
        }

        public async Task RemoveFavoritesReviewAsync(string userId, string reviewId)
        {
            await _favoriteReviewsManager.RemoveReviewAsync(userId, reviewId);
        }

        public async Task<bool> IsFavoriteReviewAsync(string userId, string reviewId)
        {
            var isFavorite = await _dbContext.Set<UsersFavoriteReviews>()
                .AnyAsync(favorite => favorite.UserId == userId && favorite.ReviewId == reviewId);

            return isFavorite;
        }


        public async Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId)
        {
            return await _favoriteReviewsManager.GetFavoriteReviewsAsync(userId);
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
