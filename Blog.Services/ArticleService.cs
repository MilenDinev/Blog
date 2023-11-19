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
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public ArticleService(
            ITagService tagService,
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
           _tagService = tagService;
            _mapper = mapper;
        }

        public async Task CreateAsync(ArticleCreateModel articleModel, string userId)
        {
            await ValidateCreateInputAsync(articleModel.Title);

            var article = _mapper.Map<Article>(articleModel);

            await CreateEntityAsync(article, userId);
        }

        public async Task EditAsync(ArticleEditModel articleModel, string articleId, string modifierId)
        {
            var article = await GetByIdAsync(articleId);

            article.Title = articleModel.Title ?? article.Title;
            article.ProviderName = articleModel.ProviderName ?? article.ProviderName;
            article.Url = articleModel.Url ?? article.Url;
            article.ImageUrl = articleModel.ImageUrl ?? article.ImageUrl;

            await SaveModificationAsync(article, modifierId);
        }

        public async Task DeleteAsync(string articleId, string modifierId)
        {
            var article = await GetByIdAsync(articleId);

            await DeleteEntityAsync(article, modifierId);
        }

        public async Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync()
        {
            var articlePreviewModelBundle = await _dbContext.Articles
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ArticlePreviewModel
                {
                    Id= x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProviderName = x.ProviderName,
                    UploadDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Url = x.Url
                }).ToListAsync();

            return articlePreviewModelBundle;
        }

        public async Task<ArticleEditModel> GetArticleEditViewModelByIdAsync(string id)
        {

            var articleEditViewModel = await _dbContext.Articles
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ArticleEditModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProviderName = x.ProviderName,
                    Url = x.Url
                }).SingleOrDefaultAsync();

            return articleEditViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

        }

        public async Task<ArticleDeleteViewModel> GetArticleDeleteViewModelByIdAsync(string id)
        {

            var articleDeleteViewModel = await _dbContext.Articles
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new ArticleDeleteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ProviderName = x.ProviderName,
                UploadDate = x.CreationDate.ToString(StringFormats.CreationDate),
                Url = x.Url
            }).SingleOrDefaultAsync();

            return articleDeleteViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));
        }
    }
}
