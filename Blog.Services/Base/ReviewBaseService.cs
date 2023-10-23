namespace Blog.Services.Base
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Review;
    using Constants;
    using Repository;
    using Handlers.Exceptions;

    public abstract class ReviewBaseService : Repository<Review>
    {

        protected ReviewBaseService(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> AnyByTagAsync(string tag)
        {
            var any = await AnyByStringAsync(tag)
            || await this.AnyByIdAsync(tag);

            return any;
        }

        public async Task<bool> AnyByTitleAsync(string title)
        {
            var any = await this.AnyByStringAsync(title);

            return any;
        }

        public async Task<ReviewPreviewModel> GetReviewPreviewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var reviewPreviewModel = await this.dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    CreationDate = x.CreationDate.ToString("dddd MMM hh:mm tt"),
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    PricingStrategies = x.PricingStrategies.
                    Select(y => y.Model)
                    .ToList(),
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList()

                }).SingleOrDefaultAsync();

            return reviewPreviewModel;
        }

        public async Task<ReviewViewModel> GetReviewViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var reviewViewModel = await this.dbContext.Reviews
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ReviewViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    DownVotes = x.Votes.Count(x => !x.Type && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    Creator = x.Creator.UserName ?? "Anonymous",
                    LastModifier = x.LastModifier.UserName ?? "Anonymous",
                    CreationDate = x.CreationDate.ToString("dddd MMM hh:mm tt"),
                    LastModifiedOn = x.LastModifiedOn.ToString("dddd MMM hh:mm tt"),
                    FavoriteByUsers = x.FavoriteByUsers.Count(x => !x.Deleted),
                    LikedByUsers = x.LikedByUsers.Count(x => !x.Deleted),
                    PricingStrategies = x.PricingStrategies.
                    Select(y => y.Model)
                    .ToList(),
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList()
                }).SingleOrDefaultAsync();

            return reviewViewModel;
        }

        public async Task<ICollection<ReviewPreviewModel>> GetReviewPreviewModelBundleAsync()
        {

            var reviewPreviewModelBundle = await this.dbContext.Reviews
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new ReviewPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    Creator = x.Creator.UserName ?? "Anonymous",
                    CreationDate = x.CreationDate.ToString("dddd MMM hh:mm tt"),
                    PricingStrategies = x.PricingStrategies.
                    Select(y => y.Model)
                    .ToList(),
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList()
                }).ToListAsync();

            return reviewPreviewModelBundle;
        }

        public async Task<ICollection<ReviewPreviewModel>> GetTodaysReviewPreviewModelBundleAsync()
        {
            var currentDate = DateTime.UtcNow.Date;

            var reviewLatestPreviewModelBundle = await this.dbContext.Reviews
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
                    Creator = x.Creator.UserName ?? "Anonymous",
                    CreationDate = x.CreationDate.ToString("dddd MMM hh:mm tt"),
                    PricingStrategies = x.PricingStrategies.
                    Select(y => y.Model)
                    .ToList(),
                    Tags = x.Tags
                    .Select(y => y.Value)
                    .ToList()
                }).ToListAsync();

            return reviewLatestPreviewModelBundle;
        }

        public async Task<ReviewEditViewModel> GetReviewEditViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var reviewEditViewModel = await this.dbContext.Reviews
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
                }).SingleOrDefaultAsync();

            return reviewEditViewModel;
        }

        public async Task<ReviewDeleteViewModel> GetReviewDeleteViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var reviewDeleteViewModel = await this.dbContext.Reviews
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
                }).SingleOrDefaultAsync();

            return reviewDeleteViewModel;
        }
    }
}
