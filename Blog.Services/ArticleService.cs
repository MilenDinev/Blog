namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.RequestModels.Article;
    using Base;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;
    using Repository.Interfaces;

    public class ArticleService : PublicationBaseService,  IArticleService
    {
        private readonly IMapper mapper;    

        public ArticleService(
            IMapper mapper,
            IFinder finder,
            ApplicationDbContext dbContext
            ) 
            : base(finder, dbContext)
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
            article.Description = articleModel.Description ?? article.Description;
            article.Content = articleModel.Content ?? article.Content;
            article.ImageUrl = articleModel.ImageUrl ?? article.ImageUrl;
            article.VideoUrl = articleModel.VideoUrl ?? article.VideoUrl;
            article.ExternalArticleUrl = articleModel.ExternalArticleUrl ?? article.ExternalArticleUrl;
            article.TopPick = articleModel.TopPick ?? article.TopPick;
            article.SpecialOffer = articleModel.TopPick ?? article.SpecialOffer;
                      
            await SaveModificationAsync(article, modifierId);
        }

        public async Task DeleteAsync(string articleId, string modifierId)
        {
            var article = await this.GetByIdAsync(articleId);

            await DeleteEntityAsync(article, modifierId);
        }

        public async Task UpVote(int vote, string articleId, string modifierId)
        {
            var article = await this.finder.GetByIdAsync<Article>(articleId);

            var downVote = article.DownVotes.FirstOrDefault(x => x.UserId == modifierId);

            if (downVote != null)
            {
                this.dbContext.Remove(downVote);

            }

            var upVote = new UpVote
            {
                Id = Guid.NewGuid().ToString(),
                ArticleId = article.Id,
                UserId = modifierId
            };

            await this.dbContext.AddAsync(upVote);
            await SaveModificationAsync(article, modifierId);
        }

        public async Task DownVote(int vote, string articleId, string modifierId)
        {
            var article = await this.finder.GetByIdAsync<Article>(articleId);

            var upVote = article.UpVotes.FirstOrDefault(x => x.UserId == modifierId);

            if (upVote != null)
            {
                this.dbContext.Remove(upVote);
                
            }

            var downVote = new DownVote
            {
                Id = Guid.NewGuid().ToString(),
                ArticleId = article.Id,
                UserId = modifierId
            };

            await this.dbContext.AddAsync(downVote);
            await SaveModificationAsync(article, modifierId);
        }

        private async Task ValidateCreateInputAsync(ArticleCreateModel articleModel)
        {
            var isAnyArticle = await this.AnyByTitleAsync(articleModel.Title);
            if (isAnyArticle)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Article).Name, articleModel.Title));
        }
    }
}
