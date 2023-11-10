namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
    using Data.Models.ViewModels.Review;
    using Data.Models.ViewModels.Tag;
    using Data.Models.ViewModels.PricingStrategy;
    using Data.Models.RequestModels.Review;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

    public class ReviewService : Repository<Review>, IReviewService
    {
        private readonly IMapper _mapper;

        public ReviewService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(ReviewCreateModel reviewModel, string userId)
        {
            await ValidateCreateInputAsync(reviewModel.Title);

            var review = _mapper.Map<Review>(reviewModel);

            await CreateEntityAsync(review, userId);

            var selectedTags = _dbContext.Tags.Where(t => reviewModel.AssignedTags.Contains(t.Id)).ToList();

            review.Tags = selectedTags.Select(x => new ReviewsTags
            {
                TagId = x.Id,
                ReviewId = review.Id,
            }).ToList();

            var selectedPricingStrategies = _dbContext.PricingStrategies
                .Where(t => reviewModel.AssignedPricingStrategies.Contains(t.Id))
                .ToList();

            review.PricingStrategies = selectedPricingStrategies.Select(x => new ReviewsPricingStrategies
            {
                PricingStrategyId = x.Id,
                ReviewId = review.Id
            }).ToList();

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(ReviewEditModel reviewModel, string reviewId, string modifierId)
        {
            var review = await GetByIdAsync(reviewId);

            review.Title = reviewModel.Title ?? review.Title;
            review.Description = reviewModel.Description ?? review.Description;
            review.Content = reviewModel.Content ?? review.Content;
            review.ImageUrl = reviewModel.ImageUrl ?? review.ImageUrl;
            review.VideoUrl = reviewModel.VideoUrl ?? review.VideoUrl;
            review.ExternalArticleUrl = reviewModel.ExternalArticleUrl ?? review.ExternalArticleUrl;
            review.TopPick = reviewModel.TopPick != review.TopPick ? reviewModel.TopPick : review.TopPick;
            review.SpecialOffer = reviewModel.SpecialOffer != review.SpecialOffer ? reviewModel.SpecialOffer : review.SpecialOffer;

            await SaveModificationAsync(review, modifierId);
        }

        public async Task DeleteAsync(string reviewId, string modifierId)
        {
            var review = await GetByIdAsync(reviewId);

            await DeleteEntityAsync(review, modifierId);
        }

        public async Task<ReviewViewModel> GetReviewViewModelByIdAsync(string id)
        {
            var reviewViewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ReviewViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    DownVotes = x.Votes.Where(x => !x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    LastModifiedOn = x.LastModifiedOn.ToString(StringFormats.CreationDate),
                    PricingStrategies = x.PricingStrategies
                    .Select(y => y.PricingStrategy.Strategy)
                    .ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
        }

        public async Task<ICollection<ReviewPreviewModel>> GetReviewPreviewModelBundleAsync()
        {
            var reviewPreviewModelBundle = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return reviewPreviewModelBundle;
        }

        public async Task<ICollection<ReviewPreviewModel>> GetTodaysReviewPreviewModelBundleAsync()
        {
            var currentDate = DateTime.UtcNow.Date;

            var reviewLatestPreviewModelBundle = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => !x.Deleted && x.CreationDate.Date == currentDate)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return reviewLatestPreviewModelBundle;
        }

        public async Task<ReviewEditViewModel> GetReviewEditViewModelByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ResourceNotFoundException(string.Format(
                                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var reviewEditViewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ReviewEditViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    ImageUrl = x.ImageUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    VideoUrl = x.VideoUrl,
                    SpecialOffer = x.SpecialOffer,
                    TopPick = x.TopPick,
                    AssignedTags = x.Tags.Select(x => x.Tag.Value).ToList(),
                    AssignedPricingStrategies = x.PricingStrategies.Select(x => x.PricingStrategy.Strategy).ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewEditViewModel
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
        }

        public async Task<ReviewDeleteViewModel> GetReviewDeleteViewModelByIdAsync(string id)
        {
            var reviewDeleteViewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ReviewDeleteViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Creator = x.Creator.UserName ?? "Anonymous",
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewDeleteViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
        }

        public async Task<CreatedReviewsViewModel> GetCreatedReviewsCountAsync(string title)
        {
            var createdReviewsCount = await _dbContext.Reviews.CountAsync(x => !x.Deleted);

            var reviewsCountViewModel = new CreatedReviewsViewModel
            {
                Title = title,
                Count = createdReviewsCount,
            };

            return reviewsCountViewModel;
        }

        public async Task<AssignedTagsViewModel> GetReviewAssignedTagsAsync(string reviewId)
        {
            var assignedTagsViewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == reviewId && !x.Deleted)
                .Select(x => new AssignedTagsViewModel
                {
                     Tags = x.Tags
                    .Select(y => y.Tag.Value)
                    .ToList(),
                })
                .SingleOrDefaultAsync();

            return assignedTagsViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
        }

        public async Task<AssignedPricingStrategyViewModel> GetReviewAssignedPricingStrategiesAsync(string reviewId)
        {
            var assignedPricingStrategiesViewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == reviewId && !x.Deleted)
                .Select(x => new AssignedPricingStrategyViewModel
                {
                    Strategies = x.PricingStrategies
                    .Select(y => y.PricingStrategy.Strategy)
                    .ToList(),
                })
                .SingleOrDefaultAsync();

            return assignedPricingStrategiesViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));
        }
    }
}