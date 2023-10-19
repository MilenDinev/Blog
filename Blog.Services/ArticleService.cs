namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.RequestModels.Article;
    using Data.Models.ResponseModels.Article;
    using Constants;
    using Repository;
    using Interfaces;
    using Handlers.Exceptions;
    using Repository.Interfaces;

    public class ArticleService : RepositoryService<Article>, IArticleService
    {
        private readonly IFinder finder;
        private readonly IMapper mapper;

        public ArticleService(ApplicationDbContext dbContext,
            IFinder finder,
            IMapper mapper) : base(dbContext)
        {
            this.finder = finder;
            this.mapper = mapper;
        }

        public async Task<CreatedArticleModel> CreateAsync(ArticleCreateModel articleModel, string userId)
        {
            await this.ValidateCreateInputAsync(articleModel);

            var article = mapper.Map<Article>(articleModel);

            await CreateEntityAsync(article, userId);

            return mapper.Map<CreatedArticleModel>(article);
        }

        public async Task<EditedArticleModel> EditAsync(ArticleEditModel articleModel, string articleId, string modifierId)
        {
            var article = await this.finder.FindByIdOrDefaultAsync<Article>(articleId);

            article.Title = articleModel.Title ?? article.Title;

            await SaveModificationAsync(article, modifierId);

            return mapper.Map<EditedArticleModel>(article);
        }

        public async Task<DeletedArticleModel> DeleteAsync(string articleId, string modifierId)
        {
            var article = await this.finder.FindByIdOrDefaultAsync<Article>(articleId);

            await DeleteEntityAsync(article, modifierId);

            return mapper.Map<DeletedArticleModel>(article);
        }

        public async Task<ArticleListModel> GetByIdAsync(string articleId)
        {
            var article = await this.finder.FindByIdOrDefaultAsync<Article>(articleId);

            return mapper.Map<ArticleListModel>(article);
        }

        private async Task ValidateCreateInputAsync(ArticleCreateModel articleModel)
        {
            var isAnyArticle = await this.finder.AnyByStringAsync<Article>(articleModel.Title);
            if (isAnyArticle)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Article).Name, articleModel.Title));
        }

        public async Task<ICollection<ArticleListModel>> GetAllAsync()
        {
            var articles = await this.finder.GetAllAsync<Article>();

            return mapper.Map<ICollection<ArticleListModel>>(articles);
        }
    }
}
