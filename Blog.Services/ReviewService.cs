namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Review;
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
            var selectedTags = _dbContext.Tags.Where(t => reviewModel.AssignedTags.Contains(t.Id)).ToList();
            review.Tags = selectedTags;

            var selectedPricingStrategies = _dbContext.PricingStrategies
                .Where(t => reviewModel.AssignedPricingStrategies.Contains(t.Id))
                .ToList();
            review.PricingStrategies = selectedPricingStrategies;

            await CreateEntityAsync(review, userId);
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
                    CreationDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    LastModifiedOn = x.LastModifiedOn.ToString("dd MMMM hh:mm tt"),
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList(),
                    PricingStrategies = x.PricingStrategies
                    .Select(y => y.Strategy)
                    .ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewViewModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : reviewViewModel;
        }

        public async Task<ICollection<ReviewPreviewModel>> GetReviewPreviewModelBundleAsync()
        {
            var reviewPreviewModelBundle = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Tags = x.Tags
                        .Select(y => y.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.Strategy)
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
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Tags = x.Tags
                        .Select(y => y.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return reviewLatestPreviewModelBundle;
        }

        public async Task<ReviewEditViewModel> GetReviewEditViewModelByIdAsync(string id)
        {
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
                    AssignedTags = x.Tags.Select(x => x.Value).ToList(),
                    AssignedPricingStrategies = x.PricingStrategies.Select(x => x.Strategy).ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewEditViewModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : reviewEditViewModel;
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
                    CreationDate = x.CreationDate.ToString("dd/MM/yyyy"),
                    Creator = x.Creator.UserName ?? "Anonymous",
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return reviewDeleteViewModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : reviewDeleteViewModel;
        }
    }
}