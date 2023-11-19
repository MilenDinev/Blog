namespace Blog.Services.Interfaces
{
    using Data.Models.ViewModels.Article;
    using Data.Models.RequestModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(ArticleCreateModel articleModel, string userId);
        Task EditAsync(ArticleEditModel articleModel, string articleId, string modifierId);
        Task DeleteAsync(string articleId, string modifierId);
        Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync();
        Task<ArticleEditModel> GetArticleEditViewModelByIdAsync(string id);
        Task<ArticleDeleteViewModel> GetArticleDeleteViewModelByIdAsync(string id);
    }
}
