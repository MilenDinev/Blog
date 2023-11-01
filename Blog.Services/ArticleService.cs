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
            await ValidateCreateInputAsync(articleModel);

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

        private async Task ValidateCreateInputAsync(ArticleCreateModel articleModel)
        {
            var isAnyArticle = await AnyByStringAsync(articleModel.Url);
            if (isAnyArticle)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Article).Name, articleModel.Url));
        }

        public async Task AssignTagAsync(string articleId, string tagId, string modifierId)
        {
            var article = await GetByIdAsync(articleId);

            var isTagAlreadyAssigned = article.Tags.Any(x => x.Id == tagId);
            if (isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagAlreadyAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);
            article.Tags.Add(tag);

            await SaveModificationAsync(article, modifierId);
        }

        public async Task RemoveTag(string articleId, string tagId, string modifierId)
        {
            var article = await GetByIdAsync(articleId);

            var isTagAlreadyAssigned = article.Tags.Any(x => x.Id == tagId);
            if (!isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagNotAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);

            article.Tags.Remove(tag);

            await SaveModificationAsync(article, modifierId);
        }

        public async Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync()
        {
            var articlePreviewModelBundle = await _dbContext.Articles
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
            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articleEditViewModel = await _dbContext.Articles
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
            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Article).Name));

            var articleDeleteViewModel = await _dbContext.Articles
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
