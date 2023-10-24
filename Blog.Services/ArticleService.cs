namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Article;
    using Data.Models.RequestModels.Article;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

    public class ArticleService : Repository<Article>, IArticleService
    {
        private readonly IMapper mapper;

        public ArticleService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task CreateAsync(ArticleCreateModel articleModel, string userId)
        {
            await this.ValidateCreateInputAsync(articleModel);

            var article = mapper.Map<Article>(articleModel);

            await CreateEntityAsync(article, userId);
        }

        public async Task EditAsync(ArticleEditModel articleModel, string articleId, string modifierId)
        {
            var article = await this.GetByIdAsync(articleId);

            article.Title = articleModel.Title ?? article.Title;
            article.ProviderName = articleModel.ProviderName ?? article.ProviderName;
            article.Url = articleModel.Url ?? article.Url;
            article.ImageUrl = articleModel.ImageUrl ?? article.ImageUrl;

            await SaveModificationAsync(article, modifierId);
        }

        public async Task DeleteAsync(string articleId, string modifierId)
        {
            var article = await this.GetByIdAsync(articleId);

            await DeleteEntityAsync(article, modifierId);
        }

        private async Task ValidateCreateInputAsync(ArticleCreateModel articleModel)
        {
            var isAnyArticle = await this.AnyByStringAsync(articleModel.Url);
            if (isAnyArticle)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Article).Name, articleModel.Url));
        }

        public async Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync()
        {
            var articlePreviewModelBundle = await this.dbContext.Articles
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new ArticlePreviewModel
                {
                    Id= x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProviderName = x.ProviderName,
                    UploadDate = x.CreationDate.ToString("dd/MM/yyyy"),
                    Url = x.Url
                }).ToListAsync();

            return articlePreviewModelBundle;
        }

        public async Task<ArticleEditViewModel> GetArticleEditViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articleEditViewModel = await this.dbContext.Articles
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ArticleEditViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProviderName = x.ProviderName,
                    Url = x.Url
                }).SingleOrDefaultAsync();

            return articleEditViewModel;

        }

        public async Task<ArticleDeleteViewModel> GetArticleDeleteViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articleDeleteViewModel = await this.dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new ArticleDeleteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ProviderName = x.ProviderName,
                UploadDate = x.CreationDate.ToString("dd/MM/yyyy"),
                Url = x.Url
            }).SingleOrDefaultAsync();

            return articleDeleteViewModel;
        }
    }
}
