namespace Blog.Services.Managers
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Review;
    using Constants;
    using Handlers.Exceptions;

    public class ReviewsFavoritesManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReviewsFavoritesManager(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddReviewAsync(ICollection<Review> favorites, string reviewId)
        {

            if (!favorites.Any(x => x.Id == reviewId))
            {
                var reviewToAdd = new Review { Id = reviewId };
                _dbContext.Reviews.Attach(reviewToAdd);
                favorites.Add(reviewToAdd);
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

        public async Task RemoveReviewAsync(ICollection<Review> favorites, string reviewId)
        {
            var review = favorites.FirstOrDefault(x => x.Id == reviewId && !x.Deleted);

            if (review == null)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            favorites.Remove(review);
            await _dbContext.SaveChangesAsync();
        }

        public ICollection<ReviewPreviewModel> GetFavoriteReviewsAsync(ICollection<Review> favorites)
        {
            var favoriteReviewsModel = _mapper.Map<ICollection<ReviewPreviewModel>>(favorites.Where(x => !x.Deleted));

            return favoriteReviewsModel;
        }
    }
}
