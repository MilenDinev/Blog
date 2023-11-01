namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Review;
    using Data.Models.RequestModels.Review;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

    public class ReviewService : Repository<Review>, IReviewService
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;    

        public ReviewService(
            ITagService tagService,
            IMapper mapper,
            ApplicationDbContext dbContext
            ) 
            : base(dbContext)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReviewCreateModel reviewModel, string userId)
        {
            var isAnyReview = await AnyByStringAsync(reviewModel.Title);
            if (isAnyReview)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Review).Name, reviewModel.Title));

            var review = _mapper.Map<Review>(reviewModel);
            var selectedTags = _dbContext.Tags.Where(t => reviewModel.AssignedTags.Contains(t.Id)).ToList();
            review.Tags = selectedTags;

            var selectedPricingStrategies = _dbContext.PricingStrategies.Where(t => reviewModel.AssignedPricingStrategies.Contains(t.Id)).ToList();
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
            review.TopPick = reviewModel.TopPick ?? review.TopPick;
            review.SpecialOffer = reviewModel.TopPick ?? review.SpecialOffer;
                      
            await SaveModificationAsync(review, modifierId);
        }

        public async Task DeleteAsync(string reviewId, string modifierId)
        {
            var review = await GetByIdAsync(reviewId);

            await DeleteEntityAsync(review, modifierId);
        }

        public async Task AssignTagAsync(string reviewId, string tagId, string modifierId)
        {
            var review = await GetByIdAsync(reviewId);

            var isTagAlreadyAssigned = review.Tags.Any(x => x.Id == tagId);
            if (isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagAlreadyAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);
            
            review.Tags.Add(tag);

            await SaveModificationAsync(review, modifierId);
        }

        public async Task RemoveTag(string reviewId, string tagId, string modifierId)
        {
            var review = await GetByIdAsync(reviewId);

            var isTagAlreadyAssigned = review.Tags.Any(x => x.Id == tagId);
            if (!isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagNotAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);

            review.Tags.Remove(tag);

            await SaveModificationAsync(review, modifierId);
        }

        public async Task<ReviewPreviewModel> GetReviewPreviewModelByIdAsync(string id)
        {
            var reviewPreviewModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    CreationDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList()

                })
                .SingleOrDefaultAsync();

            return reviewPreviewModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : reviewPreviewModel;
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
                    UpVotes = x.Votes.Where(x=> x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    DownVotes = x.Votes.Where(x => !x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    LastModifiedOn = x.LastModifiedOn.ToString("dd MMMM hh:mm tt"),
                })
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
                    .ToList()
                })
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
                })
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
                .SingleOrDefaultAsync();

            return reviewDeleteViewModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : reviewDeleteViewModel;
        }

        public async Task<VoteViewModel> GetVoteResponseModelAsync(string reviewId)
        {
            var votesResponseModel = await _dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == reviewId)
                .Select(x => new VoteViewModel
                {
                  UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                  DownVotes = x.Votes.Count(x => !x.Type && !x.Deleted),
                }).SingleOrDefaultAsync();

            return votesResponseModel is null
                ? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name))
                : votesResponseModel;
        }
    }
}
