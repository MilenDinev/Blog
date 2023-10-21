namespace Blog.Services.Base
{
    using Blog.Data.Models.ResponseModels.Article;
    using Blog.Services.Constants;
    using Blog.Services.Handlers.Exceptions;
    using Data;
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Interfaces;

    internal abstract class PublicationBaseService : Repository<Article>
    {
        protected readonly IFinder finder;

        protected PublicationBaseService(IFinder finder, ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this.finder = finder;
        }

        protected async Task<bool> AnyByTagAsync(string tag)
        {
            var any = await this.finder.AnyByStringAsync<Article>(tag)
            || await this.finder.AnyByIdAsync<Article>(tag);

            return any;
        }

        protected async Task<bool> AnyByTitleAsync(string title)
        {
            var any = await this.finder.AnyByStringAsync<Article>(title);

            return any;
        }

        protected async Task<bool> AnyByIdAsync(string id)
        {
            var any = await this.finder.AnyByIdAsync<Article>(id);

            return any;
        }

        protected async Task<ArticlePreviewModel> GetPreviewModelAsync(string id)
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
                    UpVotes = x.UpVotes,
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

        protected async Task<ArticleCompleteModel> GetCompleteModelAsync(string id)
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
                    UpVotes = x.UpVotes,
                    DownVotes = x.DownVotes,
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





        protected async Task<Article> FindByStringOrDefaultAsync(string tag)
        {
            var article = await this.finder.FindByStringOrDefaultAsync<Article>(tag)
            ??
            await this.finder.FindByIdOrDefaultAsync<Article>(tag);

            return article;
        }

        protected async Task<Article> FindByTitleOrDefaultAsync(string title)
        {
            var article = await this.finder.FindByStringOrDefaultAsync<Article>(title);

            return article;
        }

        protected async Task<Article> FindByIdOrDefaultAsync(string id)
        {
            var article = await this.finder.FindByIdOrDefaultAsync<Article>(id);

            return article;
        }

        protected async Task<Article> GetByIdAsync(string id)
        {
            var article = await this.finder.FindByIdOrDefaultAsync<Article>(id);

            return article;
        }

        protected async Task<Article> GetByTitleAsync(string title)
        {
            var article = await this.finder.FindByStringOrDefaultAsync<Article>(title);

            return article;
        }

    }
}
