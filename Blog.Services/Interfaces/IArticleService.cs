namespace Blog.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Models.RequestModels.Article;
    using Data.Models.ResponseModels.Article;

    public interface IArticleService
    {
        Task<CreatedArticleModel> CreateAsync(ArticleCreateModel articleModel, string userId);
        Task<EditedArticleModel> EditAsync(ArticleEditModel articleModel, string articleId, string modifierId);
        Task<DeletedArticleModel> DeleteAsync(string articleId, string modifierId);
        Task<ArticleListModel> GetByIdAsync(string articleId);
        Task<ICollection<ArticleListModel>> GetAllAsync();
    }
}
