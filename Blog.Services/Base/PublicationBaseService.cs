namespace Blog.Services.Base
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Models.ResponseModels.Article;
    using Constants;
    using Repository;
    using Handlers.Exceptions;
    using Repository.Interfaces;

    public abstract class PublicationBaseService : Repository<Article>
    {
        protected readonly IFinder finder;

        protected PublicationBaseService(IFinder finder, ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this.finder = finder;
        }

        public async Task<bool> AnyByTagAsync(string tag)
        {
            var any = await this.finder.AnyByStringAsync<Article>(tag)
            || await this.finder.AnyByIdAsync<Article>(tag);

            return any;
        }

        public async Task<bool> AnyByTitleAsync(string title)
        {
            var any = await this.finder.AnyByStringAsync<Article>(title);

            return any;
        }

        public async Task<bool> AnyByIdAsync(string id)
        {
            var any = await this.finder.AnyByIdAsync<Article>(id);

            return any;
        }

        public async Task<ArticlePreviewModel> GetArticlePreviewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);    

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articlePreviewModel = await this.dbContext.Articles
                .Where(x => x.Id == id)
                .Select(x => new ArticlePreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description + "...",
                    UpVotes = x.UpVotes.Count(x => !x.Deleted),
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

            return articlePreviewModel;
        }

        public async Task<ArticleCompleteModel> GetArticleCompleteModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articleCompleteModel = await this.dbContext.Articles
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ArticleCompleteModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    UpVotes = x.UpVotes.Count(x => !x.Deleted),
                    DownVotes = x.DownVotes.Count(x => !x.Deleted),
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

            return articleCompleteModel;
        }

        public async Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync()
        {
            
            var articlePreviewModelBundle = await this.dbContext.Articles
                .Select(x => new ArticlePreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.UpVotes.Count(x => !x.Deleted),
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

            return articlePreviewModelBundle;
        }

        protected async Task<Article> GetByIdAsync(string articleId)
        {
            var article = await this.finder.GetByIdAsync<Article>(articleId);

            return article;
        }

    }
}
