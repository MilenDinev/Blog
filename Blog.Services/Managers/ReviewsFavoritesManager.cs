namespace Blog.Services.Managers
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Review;
    using Constants;
    using Handlers.Exceptions;
    using Blog.Data.Entities.Shared;

    public class ReviewsFavoritesManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReviewsFavoritesManager(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddReviewAsync(string userId, string reviewId)
        {
            var isFavorite = await _dbContext.Set<UsersFavoriteReviews>()
                .AnyAsync(favorite => favorite.UserId == userId && favorite.ReviewId == reviewId);

            if (!isFavorite)
            {
                var userFavoriteReview = new UsersFavoriteReviews {
                UserId = userId,
                ReviewId = reviewId
                };

                await _dbContext.AddAsync(userFavoriteReview);
                await _dbContext.SaveChangesAsync();
            }

            else
            {
                var isReviewExists = await _dbContext.Reviews.AnyAsync(r => r.Id == reviewId);
                if (!isReviewExists)
                {
                    throw new ResourceNotFoundException(string.Format(
                        ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
                }
            }
        }

        public async Task RemoveReviewAsync(string userId, string reviewId)
        {
            var userFavoriteReview = _dbContext.Set<UsersFavoriteReviews>().FirstOrDefault(x => x.UserId == userId && x.ReviewId == reviewId);

            if (userFavoriteReview == null)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            _dbContext.Remove(userFavoriteReview);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ReviewPreviewModel>> GetFavoriteReviewsAsync(string userId)
        {
            var favoriteReviewsModel = await  _dbContext.Set<UsersFavoriteReviews>()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.ReviewId,
                    Title = x.Review.Title,
                    ImageUrl = x.Review.ImageUrl,
                    TopPick = x.Review.TopPick,
                    SpecialOffer = x.Review.SpecialOffer,
                    Description = x.Review.Description,
                    CreationDate = x.Review.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Tags = x.Review.Tags
                        .Where(x=> !x.Tag.Deleted)
                        .Select(x => x.Tag.Value).ToList(),
                    PricingStrategies = x.Review.PricingStrategies
                        .Where(x => !x.PricingStrategy.Deleted)
                        .Select(x => x.PricingStrategy.Strategy).ToList(),
                    UpVotes = x.Review.Votes
                        .Count(x => x.Type == true && !x.Deleted)               
                })
                .AsSplitQuery()
                .ToListAsync();

            return favoriteReviewsModel;
        }
    }
}
