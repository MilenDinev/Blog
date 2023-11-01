namespace Blog.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;
    using Interfaces;
    using Constants;
    using Handlers.Exceptions;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _dbContext = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddReviewToFavorite(string userId, string reviewId)
        {
            var user = await _userManager.FindByIdAsync(userId);
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
            var review = await _dbContext.Reviews
                .Include(r => r.FavoriteByUsers)
                .FirstOrDefaultAsync(r => r.Id == reviewId && !r.Deleted && r.FavoriteByUsers.Any(x => x.Id == userId))
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
             
            var user = review.FavoriteByUsers.FirstOrDefault(x => x.Id == userId);
            review.FavoriteByUsers.Remove(user);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<VoteViewModel> VoteAsync(bool type, string reviewId, string userId)
        {
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

        public async Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var favoriteReviewsModel = _mapper.Map<ICollection<ReviewPreviewModel>>(user.FavoriteReviews.Where(x=> !x.Deleted));

            return favoriteReviewsModel;
        }
    }
}
