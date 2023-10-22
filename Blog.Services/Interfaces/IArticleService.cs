namespace Blog.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Models.RequestModels.Article;
    using Data.Models.ResponseModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(ArticleCreateModel articleModel, string userId);
        Task EditAsync(ArticleEditModel articleModel, string articleId, string modifierId);
        Task DeleteAsync(string articleId, string modifierId);
        Task<bool> AnyByIdAsync(string id);
        Task<ArticlePreviewModel> GetArticlePreviewModelByIdAsync(string tag);
        Task<ArticleCompleteModel> GetArticleCompleteModelByIdAsync(string tag);
        Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync();
    }
}
